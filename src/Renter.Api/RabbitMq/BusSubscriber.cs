using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using RawRabbit;
using RawRabbit.Common;
using RawRabbit.Enrichers.MessageContext;
using Renter.Core.Domain;
using Renter.Infrastructure.Commands;

namespace Renter.Api.RabbitMq
{
    public class BusSubscriber : IBusSubscriber
    {
        private readonly IBusClient _busClient;
        private readonly IServiceProvider _serviceProvider;
        private readonly int _retries;
        private readonly int _retryInterval;

        public BusSubscriber(IApplicationBuilder app)
        {
            _serviceProvider = app.ApplicationServices.GetService<IServiceProvider>();
            _busClient = _serviceProvider.GetService<IBusClient>();
            var options = _serviceProvider.GetService<RabbitMqOptions>();
            _retries = options.Retries >= 0 ? options.Retries : 3;
            _retryInterval = options.RetryInterval > 0 ? options.RetryInterval : 2;
        }

        public IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, RenterException, IRejectedEvent> onError = null)
            where TCommand : ICommand
        {
            _busClient.SubscribeAsync<TCommand, CorrelationContext>(async (command, correlationContext) =>
            {
                var commandHandler = _serviceProvider.GetService<ICommandHandler<TCommand>>();

                return await TryHandleAsync(command, correlationContext,
                    () => commandHandler.HandleAsync(command, correlationContext), onError);
            });

            return this;
        }

        // Internal retry for services that subscribe to the multiple events of the same types.
        // It does not interfere with the routing keys and wildcards (see TryHandleWithRequeuingAsync() below).
        private async Task<Acknowledgement> TryHandleAsync<TMessage>(TMessage message,
            CorrelationContext correlationContext,
            Func<Task> handle, Func<TMessage, RenterException, IRejectedEvent> onError = null)
        {
            var currentRetry = 0;
            var retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(_retries, i => TimeSpan.FromSeconds(_retryInterval));

            var messageName = message.GetType().Name;

            return await retryPolicy.ExecuteAsync<Acknowledgement>(async () =>
            {
                try
                    {
                        await handle();
                        return new Ack();
                    }
                    catch (Exception exception)
                    {
                        currentRetry++;
                       // _logger.LogError(exception, exception.Message);
                       // span.SetTag(Tags.Error, true);

                        if (exception is RenterException renterException && onError != null)
                        {
                            var rejectedEvent = onError(message, renterException);
                            await _busClient.PublishAsync(rejectedEvent, ctx => ctx.UseMessageContext(correlationContext));
//                            _logger.LogInformation($"Published a rejected event: '{rejectedEvent.GetType().Name}' " +
//                                                   $"for the message: '{messageName}' with correlation id: '{correlationContext.Id}'.");
                            return new Ack();
                        }

                        throw new Exception($"Unable to handle a message: '{messageName}' " +
                                            $"with correlation id: '{correlationContext.Id}', " +
                                            $"retry {currentRetry - 1}/{_retries}...");
                    }
            });
        }
    }

    public interface IRejectedEvent
    {
    }
}
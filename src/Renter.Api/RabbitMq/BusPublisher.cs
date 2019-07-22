using System;
using System.Threading.Tasks;
using RawRabbit;
using RawRabbit.Enrichers.MessageContext;
using Renter.Infrastructure.Commands;

namespace Renter.Api.RabbitMq
{
    public class BusPublisher : IBusPublisher
    {
        private readonly IBusClient _busClient;

        public BusPublisher(IBusClient busClient)
        {
            _busClient = busClient;
        }

        public async Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand
            => await _busClient.PublishAsync(command, ctx => ctx.UseMessageContext(context));
    }
}
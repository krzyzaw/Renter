using System;
using Renter.Core.Domain;
using Renter.Infrastructure.Commands;

namespace Renter.Api.RabbitMq
{
    public interface IBusSubscriber
    {
        IBusSubscriber SubscribeCommand<TCommand>(string @namespace = null, string queueName = null,
            Func<TCommand, RenterException, IRejectedEvent> onError = null)
            where TCommand : ICommand;
    }
}
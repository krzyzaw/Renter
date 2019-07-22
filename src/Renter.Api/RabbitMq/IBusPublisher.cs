using System.Threading.Tasks;
using Renter.Infrastructure.Commands;

namespace Renter.Api.RabbitMq
{
    public interface IBusPublisher
    {
        Task SendAsync<TCommand>(TCommand command, ICorrelationContext context)
            where TCommand : ICommand;
    }
}
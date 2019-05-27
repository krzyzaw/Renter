using System.Threading.Tasks;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Commands.Users;

namespace Renter.Infrastructure.Handlers.Users
{
    public class ChangeUserPasswordHandler : ICommandHandler<ChangeUserPassword>
    {
        public async Task HandleAsync(ChangeUserPassword command)
        {
            await Task.CompletedTask;
        }
    }
}
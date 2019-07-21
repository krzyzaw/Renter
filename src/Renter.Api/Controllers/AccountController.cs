using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Commands.Users;

namespace Renter.Api.Controllers
{
    public class AccountController : ApiControllerBase
    {
        public AccountController(ICommandDispatcher commandDispatcher) : base(commandDispatcher)
        {
        }

        [HttpPut]
        [Route("password")]
        public async Task<IActionResult> Put([FromBody] ChangeUserPassword command)
        {
            await DispatchAsync(command);

            return NoContent();
        }
    }
}
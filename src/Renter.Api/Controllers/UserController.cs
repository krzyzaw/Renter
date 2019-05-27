using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Commands.Users;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;

        public UserController(ICommandDispatcher commandDispatcher,
            IUserService userService) : base(commandDispatcher)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<IActionResult> Get(string email)
        {
            var user = await _userService.GetAsync(email);
            if (user == null)
            {
                return NotFound();
            }

            return Json(user);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUser command)
        {
            await CommandDispatcher.DispatchAsync(command);

            return Created($"user/{command.Email}", new object());
        }
    }
}
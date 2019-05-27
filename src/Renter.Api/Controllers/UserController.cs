using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Commands.Users;
using Renter.Infrastructure.Services.Interfaces;
using Renter.Infrastructure.Settings;

namespace Renter.Api.Controllers
{
    public class UserController : ApiControllerBase
    {
        private readonly IUserService _userService;
        private readonly GeneralSettings _generalSettings;

        public UserController(ICommandDispatcher commandDispatcher, IUserService userService, GeneralSettings generalSettings) : base(commandDispatcher)
        {
            _userService = userService;
            _generalSettings = generalSettings;
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
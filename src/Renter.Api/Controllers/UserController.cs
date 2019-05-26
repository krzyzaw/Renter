using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.Commands.Users;
using Renter.Infrastructure.DTO;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Api.Controllers
{
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public async Task<UserDto> Get(string email)
            =>await _userService.GetAsync(email);

        [HttpPost]
        public async Task Post([FromBody] CreateUser request)
        {
           await _userService.RegisterAsync(request.Email, request.Username, request.Password);
        }
    }
}
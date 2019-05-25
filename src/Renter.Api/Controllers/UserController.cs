using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
        public UserDto Get(string email)
            => _userService.Get(email);
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.DTO;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("{email}")]
        public UserDto Get(string email)
            => _userService.Get(email);
    }
}
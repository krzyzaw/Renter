using System;
using Microsoft.AspNetCore.Mvc;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Api.Controllers
{
    public class SecurityController : ApiControllerBase
    {
        private readonly IJwtService _jwtService;

        public SecurityController(ICommandDispatcher commandDispatcher, IJwtService jwtService) : base(commandDispatcher)
        {
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("token")]
        public IActionResult Get()
        {
            var token = _jwtService.CreateToken(Guid.NewGuid(), "user");
            return Json(token);
        }
    }
}
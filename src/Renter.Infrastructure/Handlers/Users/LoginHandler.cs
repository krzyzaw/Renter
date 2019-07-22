using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Commands.Users;
using Renter.Infrastructure.Extensions;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Infrastructure.Handlers.Users
{
    public class LoginHandler : ICommandHandler<Login>
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMemoryCache _cache;
        private readonly IHandler _handler;

        public LoginHandler(IUserService userService, IJwtService jwtService,
            IMemoryCache cache, IHandler handler)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cache = cache;
            _handler = handler;
        }

        //Handler
        public async Task HandleAsync(Login command)
            => await _handler
                .Run(async () => await _userService.LoginAsync(command.Email, command.Password))
                .Next()
                .Run(async () =>
                {
                    var user = await _userService.GetAsync(command.Email);
                    var jwt = _jwtService.CreateToken(user.Id, user.Role);
                    _cache.SetJwt(command.TokenId, jwt);
                })
                .ExecuteAsync();

//        Standard
//        public async Task HandleAsync(Login command)
//        {
//            await _userService.LoginAsync(command.Email, command.Password);
//            var user = await _userService.GetAsync(command.Email);
//            var jwt = _jwtService.CreateToken(user.Id, user.Role);
//            _cache.SetJwt(command.TokenId, jwt);
//        }
    }
}
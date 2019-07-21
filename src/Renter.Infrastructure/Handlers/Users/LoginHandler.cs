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

        public LoginHandler(IUserService userService, IJwtService jwtService, IMemoryCache cache)
        {
            _userService = userService;
            _jwtService = jwtService;
            _cache = cache;
        }

        public async Task HandleAsync(Login command)
        {
            await _userService.LoginAsync(command.Email, command.Password);
            var user = await _userService.GetAsync(command.Email);
            var jwt = _jwtService.CreateToken(user.Id, user.Role);
            _cache.SetJwt(command.TokenId, jwt);
        }
    }
}
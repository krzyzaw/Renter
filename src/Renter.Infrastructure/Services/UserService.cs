using System;
using Renter.Core.Domain;
using Renter.Core.Repositories;
using Renter.Infrastructure.DTO;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Infrastructure.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserDto Get(string email)
        {
            var user = _userRepository.Get(email);

            return new UserDto()
            {
                Id = user.Id,
                Email = user.Email,
                FullName = user.FullName,
                Username = user.Username
            };
        }

        public void Register(string email, string username, string password)
        {
            var user = _userRepository.Get(email);
            if (user != null)
            {
                throw new Exception($"User with email: '{email}' already exist.");
            }

            var salt = Guid.NewGuid().ToString("N");
            user = new User(email, username, password, salt);
            _userRepository.Add(user);
        }
    }
}
using System;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IUserService
    {
        UserDto Get(string email);

        void Register(string email, string username, string password);
    }
}
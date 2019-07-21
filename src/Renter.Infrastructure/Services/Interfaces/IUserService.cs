using System;
using System.Threading.Tasks;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);

        Task RegisterAsync(Guid userId, string email, string username, string password, string role);

        Task LoginAsync(string email, string password);
    }
}
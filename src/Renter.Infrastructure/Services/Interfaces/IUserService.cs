using System.Threading.Tasks;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IUserService : IService
    {
        Task<UserDto> GetAsync(string email);

        Task RegisterAsync(string email, string username, string password);
    }
}
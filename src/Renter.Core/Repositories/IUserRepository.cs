using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Renter.Core.Domain;

namespace Renter.Core.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetAsync(Guid id);

        Task<User> GetAsync(string email);

        Task<IEnumerable<User>> GetAllAsync();

        Task AddAsync(User user);

        Task UpdateAsync(User user);

        Task RemoveAsync(Guid id);
    }
}
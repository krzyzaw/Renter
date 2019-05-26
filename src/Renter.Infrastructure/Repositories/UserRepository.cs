using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Renter.Core.Domain;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private static ISet<User> _users = new HashSet<User>()
        {
            new User("user1@email.com", "user1", "password", "salt")
        };

        public async Task<User> GetAsync(Guid id)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Id == id));

        public async Task<User> GetAsync(string email)
            => await Task.FromResult(_users.SingleOrDefault(x => x.Email == email.ToLowerInvariant()));

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Task.FromResult(_users);

        public async Task AddAsync(User user)
        {
            _users.Add(user);
            await Task.CompletedTask;
        }       

        public async Task UpdateAsync(User user)
        {
            await Task.CompletedTask;
        }

        public async Task RemoveAsync(Guid id)
        {
            var user = await GetAsync(id);
            _users.Remove(user);
        }
    }
}
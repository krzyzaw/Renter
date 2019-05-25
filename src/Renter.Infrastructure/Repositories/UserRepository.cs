using System;
using System.Collections.Generic;
using System.Linq;
using Renter.Core.Domain;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private ISet<User> _users = new HashSet<User>()
        {
            new User("user1@email.com", "user1", "password", "salt")
        };

        public User Get(Guid id)
            => _users.SingleOrDefault(x => x.Id == id);

        public User Get(string email)
            => _users.SingleOrDefault(x => x.Email == email.ToLowerInvariant());

        public IEnumerable<User> GetAll()
            => _users;

        public void Add(User user)
        {
            _users.Add(user);
        }       

        public void Update(User user)
        {
        }

        public void Remove(Guid id)
        {
            var user = Get(id);
            _users.Remove(user);
        }
    }
}
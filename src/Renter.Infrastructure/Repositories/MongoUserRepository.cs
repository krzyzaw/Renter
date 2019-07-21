using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Renter.Core.Domain;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.Repositories
{
    public class MongoUserRepository : IUserRepository, IMongoRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public MongoUserRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        public async Task<User> GetAsync(Guid id)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Id == id);

        public async Task<User> GetAsync(string email)
            => await Users.AsQueryable().FirstOrDefaultAsync(x => x.Email == email);

        public async Task<IEnumerable<User>> GetAllAsync()
            => await Users.AsQueryable().ToListAsync();

        public async Task AddAsync(User user)
            => await Users.InsertOneAsync(user);

        public async Task UpdateAsync(User user)
            => await Users.ReplaceOneAsync(x => x.Id == user.Id, user);

        public async Task RemoveAsync(Guid id)
            => await Users.DeleteOneAsync(x => x.Id == id);

        private IMongoCollection<User> Users => _mongoDatabase.GetCollection<User>("Users");
    }
}
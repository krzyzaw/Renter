using System.Threading.Tasks;
using MongoDB.Driver;
using Renter.Core.Domain;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.Repositories
{
    public class DiscountsRepository : IDiscountsRepository
    {
        private readonly IMongoDatabase _mongoDatabase;

        public DiscountsRepository(IMongoDatabase mongoDatabase)
        {
            _mongoDatabase = mongoDatabase;
        }

        private IMongoCollection<Discount> Discounts => _mongoDatabase.GetCollection<Discount>("Discounts");

        public async Task AddAsync(Discount discount)
            => await Discounts.InsertOneAsync(discount);

        public async Task UpdateAsync(Discount discount)
            => await Discounts.ReplaceOneAsync(x => x.Id == discount.Id, discount);
    }
}
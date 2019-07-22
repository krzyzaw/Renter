using System.Threading.Tasks;
using Renter.Core.Domain;

namespace Renter.Core.Repositories
{
    public interface IDiscountsRepository : IRepository
    {
        Task AddAsync(Discount discount);
        Task UpdateAsync(Discount discount);
    }
}
using System.Threading.Tasks;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IDataInitializer : IService
    {
        Task SeedAsync();
    }
}
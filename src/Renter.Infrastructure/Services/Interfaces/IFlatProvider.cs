using System.Collections.Generic;
using Autofac.Core;
using System.Threading.Tasks;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IFlatProvider : IService
    {
        Task<IEnumerable<FlatDto>> BrowseAsync();
        Task<FlatDto> GetAsync(string type, string name);
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Renter.Infrastructure.DTO;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Infrastructure.Services
{
    public class FlatProvider : IFlatProvider
    {
        private readonly IMemoryCache _cache;
        private static readonly string CacheKey = "flats";

        public FlatProvider(IMemoryCache cache)
        {
            _cache = cache;
        }

        public async Task<IEnumerable<FlatDto>> BrowseAsync()
        {
            var flats = _cache.Get<IEnumerable<FlatDto>>(CacheKey);
            if (flats == null)
            {
                flats = null; //await GetAllAsync();
                _cache.Set(CacheKey, flats, TimeSpan.FromMinutes(15));
            }

            return flats;
        }

        public Task<FlatDto> GetAsync(string type, string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
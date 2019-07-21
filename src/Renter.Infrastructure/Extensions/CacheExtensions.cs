using System;
using Microsoft.Extensions.Caching.Memory;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Extensions
{
    public static class CacheExtensions
    {
        public static void SetJwt(this IMemoryCache cache, Guid tokenId, JwtDto jwt)
        {
            try
            {
                cache.Set(GetJwtKey(tokenId), jwt, TimeSpan.FromSeconds(30));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    


        public static JwtDto GetJwt(this IMemoryCache cache, Guid tokenId)
            => cache.Get<JwtDto>(GetJwtKey(tokenId));

        private static string GetJwtKey(Guid tokenId)
            => $"key-{tokenId}";
    }
}
using System;
using System.Threading.Tasks;
using Renter.Core.Domain;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.Extensions
{
    public static class RepositoryExtensions
    {
        public static async Task<User> GetOrFailAsync(this IUserRepository repository, Guid userId)
        {
            var user = await repository.GetAsync(userId);
            if (user == null)
            {
                throw new Exception( $"User with id: '{userId}' was not found.");
            }

            return user;
        }
    }
}
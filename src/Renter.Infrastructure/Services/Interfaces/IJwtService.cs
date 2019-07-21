using System;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Services.Interfaces
{
    public interface IJwtService : IService
    {
        JwtDto CreateToken(Guid userId, string role);
    }
}
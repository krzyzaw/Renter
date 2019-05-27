using AutoMapper;
using Renter.Core.Domain;
using Renter.Infrastructure.DTO;

namespace Renter.Infrastructure.Mappers
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            MapperConfiguration configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<User, UserDto>();
            });

            return configuration.CreateMapper();
        }
    }
}
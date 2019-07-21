using Autofac;
using Microsoft.Extensions.Configuration;
using Renter.Infrastructure.Extensions;
using Renter.Infrastructure.IoC.Modules;
using Renter.Infrastructure.Mappers;
using Renter.Infrastructure.Settings;

namespace Renter.Infrastructure.IoC
{
    public class ContainerModule : Autofac.Module
    {
        private readonly IConfiguration _configuration;

        public ContainerModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterModule<CommandModule>();
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<MongoModule>();
            builder.RegisterModule(new SettingsModule(_configuration));
            builder.RegisterInstance(AutoMapperConfig.Initialize()).SingleInstance();
        }
    }
}
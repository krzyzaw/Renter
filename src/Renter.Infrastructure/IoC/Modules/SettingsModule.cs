using Autofac;
using Microsoft.Extensions.Configuration;
using Renter.Infrastructure.Extensions;
using Renter.Infrastructure.Settings;

namespace Renter.Infrastructure.IoC.Modules
{
    public class SettingsModule : Module
    {
        private readonly IConfiguration _configuration;

        public SettingsModule(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_configuration.GetSettings<GeneralSettings>())
                .SingleInstance();
        }
    }
}
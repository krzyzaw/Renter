using Autofac;
using System.Reflection;
using Renter.Infrastructure.Commands;
using Renter.Infrastructure.Services.Interfaces;

namespace Renter.Infrastructure.IoC.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(ServiceModule)
                .GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo(typeof(IService))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
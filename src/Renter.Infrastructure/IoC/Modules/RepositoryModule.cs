using Autofac;
using System.Reflection;
using Renter.Core.Repositories;

namespace Renter.Infrastructure.IoC.Modules
{
    public class RepositoryModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(RepositoryModule)
                .GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo(typeof(IRepository))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
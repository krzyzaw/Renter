using Autofac;
using Renter.Core.Repositories;
using System.Reflection;
using MongoDB.Driver;
using Renter.Infrastructure.Mongo;

namespace Renter.Infrastructure.IoC.Modules
{
    public class MongoModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register((context, parameters) =>
            {
                var settings = context.Resolve<MongoSettings>();

                return new MongoClient(settings.ConnectionString);
            }).SingleInstance();

            builder.Register((context, parameters) =>
            {
                var client = context.Resolve<MongoClient>();
                var settings = context.Resolve<MongoSettings>();
                var database = client.GetDatabase(settings.Database);

                return database;
            }).As<IMongoDatabase>();

            Assembly assembly = typeof(RepositoryModule)
                .GetTypeInfo().Assembly;

            builder.RegisterAssemblyTypes(assembly)
                .AssignableTo(typeof(IMongoRepository))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();
        }
    }
}
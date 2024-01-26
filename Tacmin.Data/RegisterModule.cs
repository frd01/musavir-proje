using Autofac;
using Tacmin.Core;
using Tacmin.Data.Repositories;

namespace Tacmin.Data
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MainContext>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterAssemblyTypes(Engine.Instance.Assemblies).AsClosedTypesOf(typeof(MainRepository<>)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(MainRepository<>)).AsSelf().InstancePerLifetimeScope();
        }
    }
}

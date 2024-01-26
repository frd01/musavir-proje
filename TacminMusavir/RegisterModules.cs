using Autofac;
using Tacmin.Core.Interface;

namespace TacminMusavir
{
    public class RegisterModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TacminVirtualContext>().AsSelf().As<IVirtualContext>().InstancePerLifetimeScope();
        }
    }
}
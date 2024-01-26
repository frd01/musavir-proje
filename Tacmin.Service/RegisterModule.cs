using Autofac;
using Tacmin.Core.Interface;
using Tacmin.Service.Authentication;
using Tacmin.Service.Maliye;
using Tacmin.Service.Tebligat;

namespace Tacmin.Service
{
    public class RegisterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AuthService>().AsSelf().As<IAuthService>().InstancePerLifetimeScope();
            builder.RegisterType<EncryptionService>().AsSelf().As<IEncryptionService>().InstancePerLifetimeScope();
            builder.RegisterType<BeyannameService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<GibAuthService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MukellefService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<HesapService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BildirgeService>().AsSelf().InstancePerLifetimeScope();            
            builder.RegisterType<VergiLevhaService>().AsSelf().InstancePerLifetimeScope();            
            builder.RegisterType<MainService>().AsSelf().InstancePerLifetimeScope();           
            builder.RegisterType<GidenFaturaService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<GelirTebService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<IcisleriService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TestService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<RaporService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TicaretBakService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<VergiDenetimService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<GelenFaturaService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<DosyaService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<KasaService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BankaService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<CariService>().AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<GonderimService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<KontrolIslemService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MusavirService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<IslemService>().AsSelf().InstancePerLifetimeScope();

        }
    }
}

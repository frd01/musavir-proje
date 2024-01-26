using Autofac;
using Autofac.Core;
using Autofac.Events;
using Autofac.Extras.AttributeMetadata;
using Autofac.Features.Variance;
using Autofac.Integration.Mvc;
using AutoMapper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Hosting;
using System.Web.Mvc;
using Tacmin.Core.Attributes;
using Tacmin.Core.Infrastructure;
using Tacmin.Core.Interface;
using Tacmin.Core.Model;
using Tacmin.Core.Repository;

namespace Tacmin.Core
{
    public class Engine : Singleton<Engine>
    {
        private Engine()
        {
        }

        static Engine()
        {
            var file = HostingEnvironment.MapPath("~/settings.json");
            var override_file = HostingEnvironment.MapPath("~/settings.override.json");

            if (File.Exists(file))
                Config = JsonConfig.FromFile(file, override_file);
        }

        public ContainerManager ContainerManager { get; private set; }

        public Assembly[] Assemblies => BuildManager.GetReferencedAssemblies().Cast<Assembly>().Where(a => a.FullName.StartsWith("Tacmin") || a.FullName.StartsWith("FinWeb")).ToArray();

        public Mapper AutoMap { get; set; }

        public static dynamic Settings => Config.Json;
        public static JsonConfig Config { get; private set; }
        public static readonly ConcurrentDictionary<string, BaseUserModel> LoggedUsers = new ConcurrentDictionary<string, BaseUserModel>();

        public void RunStartupTasks()
        {
            var startupTasks = new List<Type>();
            var classes = Assemblies.Where(assembly => assembly.GetTypes().Any(type => typeof(IStartupTask).IsAssignableFrom(type) && type.IsClass));
            foreach (var cls in classes)
            {
                var tasks = cls.GetTypes().Where(type => typeof(IStartupTask).IsAssignableFrom(type) && type.IsClass);
                startupTasks.AddRange(tasks);
            }

            foreach (var task in startupTasks)
            {
                var instance = (IStartupTask)Activator.CreateInstance(task);
                instance.Execute();
            }
        }

        public void RegisterAutofac()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule<AutofacWebTypesModule>();
            builder.RegisterControllers(Assemblies).OnActivated(args =>
            {
                //if(args.Instance is BaseController bc)

            });

            /*builder.Register(c => new CustomFilterAttribute(c.Resolve<IProperty>()))
                .AsActionFilterFor<Controller>().InstancePerHttpRequest();*/
            builder.Register(x => new TacminAuthFilter()).AsActionFilterFor<BaseController>().InstancePerLifetimeScope();
            builder.Register(x => new AllowJsonGetAttribute()).AsActionFilterFor<BaseController>().InstancePerLifetimeScope();

            builder.RegisterFilterProvider();
            builder.RegisterType<WebAppTypeFinder>().As<ITypeFinder>().SingleInstance();
            builder.RegisterAssemblyTypes(Assemblies).AsClosedTypesOf(typeof(GenericRepository<>)).AsSelf().InstancePerLifetimeScope();
            builder.RegisterSource(new ContravariantRegistrationSource());
            builder.RegisterAssemblyTypes(Assemblies).AsClosedTypesOf(typeof(IHandleEvent<>)).InstancePerLifetimeScope();
            builder.RegisterEventing();

            builder.RegisterAssemblyModules(Assemblies);
            builder.RegisterModule<AttributedMetadataModule>();

            var container = builder.Build();
            var mvcAutofacResolver = new AutofacDependencyResolver(container);
            DependencyResolver.SetResolver(mvcAutofacResolver);
            ContainerManager = new ContainerManager(container);
        }

        public T Resolve<T>() where T : class
        {
            return ContainerManager.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return ContainerManager.Resolve(type);
        }

        public T[] ResolveAll<T>()
        {
            return ContainerManager.ResolveAll<T>();
        }

        public bool TryResolve<T>(out T instance) where T : class
        {
            instance = default;
            try
            {
                return ContainerManager.TryResolve<T>(out instance);
            }
            catch (DependencyResolutionException)
            {
                return false;
            }
        }
    }
}

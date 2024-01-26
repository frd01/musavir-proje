using AutoMapper;
using Tacmin.Core;
using Tacmin.Core.Interface;

namespace TacminMusavir.StartupTasks
{
    public class AutoMapperStartupTask : IStartupTask
    {
        public void Execute()
        {
            var mapperConfig = new MapperConfiguration(conf =>
            {
                conf.AddMaps(Engine.Instance.Assemblies);
            });

            mapperConfig.AssertConfigurationIsValid();
            Engine.Instance.AutoMap = new Mapper(mapperConfig);
        }
    }
}
using System.Reflection;
using Abp.Modules;

namespace EduTesting
{
    [DependsOn(typeof(EduTestingCoreModule))]
    public class EduTestingApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

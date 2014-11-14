using System.Reflection;
using Abp.Modules;

namespace EduTesting
{
    public class EduTestingCoreModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}

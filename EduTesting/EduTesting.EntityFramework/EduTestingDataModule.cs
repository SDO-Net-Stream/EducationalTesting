using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using EduTesting.EntityFramework;

namespace EduTesting
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(EduTestingCoreModule))]
    public class EduTestingDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            Database.SetInitializer<EduTestingDbContext>(null);
        }
    }
}

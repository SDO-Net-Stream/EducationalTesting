using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using EduTesting.EntityFramework;
using EduTesting.DataProvider;
using Abp.Domain.Uow;

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
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IUserRepository, FakeUserProvider>();
            IocManager.Register<IUnitOfWork, FakeUnitOfWork>();
            Database.SetInitializer<EduTestingDbContext>(null);
        }
    }
}

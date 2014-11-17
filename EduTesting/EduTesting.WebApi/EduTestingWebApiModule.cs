using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using EduTesting.Service;
using EduTesting.Security;
using Abp.Dependency;
using Castle.MicroKernel.Registration;
namespace EduTesting
{
    [DependsOn(typeof(AbpWebApiModule), typeof(EduTestingApplicationModule))]
    public class EduTestingWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.IocContainer.Register(
                Component.For<IWebUserManager>().ImplementedBy<WebUserManager>().LifestylePerWebRequest()
            );
            /*
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EduTestingApplicationModule).Assembly, "app")
                .Build();
             */
            DynamicApiControllerBuilder.For<ITestService>("app/test")
                .WithFilters(new AuthenticationFilter(IocManager))
                .Build();

        }
    }
}

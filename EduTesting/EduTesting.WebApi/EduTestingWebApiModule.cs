using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;
using EduTesting.Service;
using EduTesting.Security;
using Abp.Dependency;
using Castle.MicroKernel.Registration;
using EduTesting.WebRequestParameters;
namespace EduTesting
{
    [DependsOn(typeof(AbpWebApiModule), typeof(EduTestingApplicationModule))]
    public class EduTestingWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IWebUserManager, WebUserManager>();
            IocManager.Register<ISessionManager, SessionManager>();
            IocManager.Register<IHttpContextProvider, HttpContextProvider>();
            IocManager.Register<IAccountService, AccountService>();
            /*
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EduTestingApplicationModule).Assembly, "app")
                .Build();
             */
            DynamicApiControllerBuilder.For<ITestService>("app/test")
                .WithFilters(new AuthenticationFilter(IocManager))
                .Build();
            DynamicApiControllerBuilder.For<IAccountService>("app/account").Build();
        }
    }
}

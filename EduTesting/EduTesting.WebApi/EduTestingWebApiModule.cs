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
using Abp.Events.Bus;
using Abp.Events.Bus.Exceptions;
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
            IocManager.Register<ILoginService, LoginService>();
            /*
            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EduTestingApplicationModule).Assembly, "app")
                .Build();
             */
            var authenticationFilter = new AuthenticationFilter(IocManager);
            DynamicApiControllerBuilder.For<ITestService>("app/test")
                .WithFilters(authenticationFilter)
                .Build();
            DynamicApiControllerBuilder.For<ILoginService>("app/login")
                .WithFilters(authenticationFilter)
                .Build();
            DynamicApiControllerBuilder.For<IAccountService>("app/account")
                .Build();
            DynamicApiControllerBuilder.For<ITestResultService>("app/testResult")
                .WithFilters(authenticationFilter)
                .Build();
            DynamicApiControllerBuilder.For<IUserGroupService>("app/group")
                .WithFilters(authenticationFilter)
                .Build();
            DynamicApiControllerBuilder.For<IUserService>("app/user")
                .WithFilters(authenticationFilter)
                .Build();

#if DEBUG
            EventBus.Default.Register<AbpHandledExceptionData>(eventData =>
            { 
                // The point for catching exceptions
            });
#endif
        }
    }
}

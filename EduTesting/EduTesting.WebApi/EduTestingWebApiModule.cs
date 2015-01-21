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
using EduTesting.Model;
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
            var authenticate = new AuthenticationFilter(IocManager);
            var authorizeUser = new AuthorizationFilter(UserRole.User, IocManager);
            var authorizeTeacher = new AuthorizationFilter(UserRole.Teacher, IocManager);
            var authorizeAdministrator = new AuthorizationFilter(UserRole.Administrator, IocManager);

            DynamicApiControllerBuilder.For<ITestService>("app/test")
                .WithFilters(authenticate, authorizeTeacher)
                .Build();

            DynamicApiControllerBuilder.For<ILoginService>("app/login")
                .WithFilters(authenticate)
                .Build();
            DynamicApiControllerBuilder.For<IAccountService>("app/account")
                .Build();
            DynamicApiControllerBuilder.For<ITestResultService>("app/result")
                .WithFilters(authenticate, authorizeTeacher)
                .Build();
            DynamicApiControllerBuilder.For<IUserGroupService>("app/group")
                .WithFilters(authenticate, authorizeTeacher)
                .Build();
            DynamicApiControllerBuilder.For<IUserService>("app/user")
                .WithFilters(authenticate, authorizeAdministrator)
                .Build();
            DynamicApiControllerBuilder.For<IExamService>("app/exam")
                .WithFilters(authenticate, authorizeAdministrator)
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

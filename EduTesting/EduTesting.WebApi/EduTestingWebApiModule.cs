using System.Reflection;
using Abp.Application.Services;
using Abp.Modules;
using Abp.WebApi;
using Abp.WebApi.Controllers.Dynamic.Builders;

namespace EduTesting
{
    [DependsOn(typeof(AbpWebApiModule), typeof(EduTestingApplicationModule))]
    public class EduTestingWebApiModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            DynamicApiControllerBuilder
                .ForAll<IApplicationService>(typeof(EduTestingApplicationModule).Assembly, "app")
                .Build();
        }
    }
}

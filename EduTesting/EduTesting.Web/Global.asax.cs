using System;
using System.Threading;
using System.Web.Http;
using System.Web.Mvc;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using EducationalProject.Models;

namespace EduTesting.Web
{
    public class MvcApplication : AbpWebApplication
    {
        protected override void Application_Start(object sender, EventArgs e)
        {
            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);

            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            IocManager.Instance.IocContainer.Register(Classes.FromAssemblyNamed("Elmah.Mvc").BasedOn<IController>().LifestyleTransient());

            base.Application_Start(sender, e);
        }

    }
}

using System;
using System.Threading;
using Abp.Dependency;
using Abp.Web;
using Castle.Facilities.Logging;
using EducationalProject.Models;
using WebMatrix.WebData;

namespace EduTesting.Web
{
    public class MvcApplication : AbpWebApplication
    {
        private static SimpleMembershipInitializer _initializer;
        private static object _initializerLock = new object();
        private static bool _isInitialized;

        protected override void Application_Start(object sender, EventArgs e)
        {
            IocManager.Instance.IocContainer.AddFacility<LoggingFacility>(f => f.UseLog4Net().WithConfig("log4net.config"));
            base.Application_Start(sender, e);
            LazyInitializer.EnsureInitialized(ref _initializer, ref _isInitialized, ref _initializerLock);
        }

        public class SimpleMembershipInitializer
        {
          public SimpleMembershipInitializer()
          {
            using (var context = new UsersContext())
              context.UserProfiles.Find(1);

            if (!WebSecurity.Initialized)
              WebSecurity.InitializeDatabaseConnection("DefaultConnection", "UserProfile", "UserId", "UserName", autoCreateTables: true);
          }
        }
    }
}

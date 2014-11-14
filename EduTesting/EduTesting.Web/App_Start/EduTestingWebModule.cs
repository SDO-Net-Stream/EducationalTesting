using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Abp.Localization;
using Abp.Localization.Sources.Xml;
using Abp.Modules;

namespace EduTesting.Web
{
    [DependsOn(typeof(EduTestingDataModule), typeof(EduTestingApplicationModule), typeof(EduTestingWebApiModule))]
    public class EduTestingWebModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Add/remove languages for your application
            Configuration.Localization.Languages.Add(new LanguageInfo("en", "English", "famfamfam-flag-us", true));
            Configuration.Localization.Languages.Add(new LanguageInfo("uk", "Ukrainian", "famfamfam-flag-ua"));
            Configuration.Localization.Languages.Add(new LanguageInfo("tr", "Türkçe", "famfamfam-flag-tr"));

            //Add/remove localization sources here
            Configuration.Localization.Sources.Add(
                new XmlLocalizationSource(
                    EduTestingConsts.LocalizationSourceName,
                    HttpContext.Current.Server.MapPath("~/Localization/EduTesting")
                    )
                );

            //Configure navigation/menu
            Configuration.Navigation.Providers.Add<EduTestingNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());

            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}

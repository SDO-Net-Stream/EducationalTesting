﻿using System.Web.Optimization;

namespace EduTesting.Web
{
    public static class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            //VENDOR RESOURCES

            //~/Bundles/App/vendor/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/vendor/css")
                    .Include(
                        "~/Content/themes/base/all.css",
                        "~/Content/bootstrap-cosmo.min.css",
                        "~/Content/toastr.min.css",
                        "~/Content/flags/famfamfam-flags.css",
                        "~/Content/font-awesome.min.css"
                    )
                );

            //~/Bundles/App/vendor/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/vendor/js")
                    .Include(
                        "~/Abp/Framework/scripts/utils/ie10fix.js",
                        "~/Scripts/json2.min.js",

                        "~/Scripts/modernizr-2.8.3.js",
                        
                        "~/Scripts/jquery/jquery-2.1.1.min.js",
                        "~/Scripts/jquery/jquery-ui.min-1.11.1.js",

                        "~/Scripts/bootstrap.min.js",

                        "~/Scripts/moment-with-locales.min.js",
                        "~/Scripts/toastr.min.js",
                        "~/Scripts/others/spinjs/spin.js",
                        "~/Scripts/others/spinjs/jquery.spin.js",

                        "~/Scripts/angular/angular.min.js",
                        "~/Scripts/angular/angular-animate.min.js",
                        "~/Scripts/angular/angular-sanitize.min.js",
                        "~/Scripts/angular/angular-ui-router.min.js",
                        //"~/Scripts/angular/angular-ui/ui-bootstrap.min.js",
                        "~/Scripts/angular/angular-ui/ui-bootstrap-tpls.min.js",
                        "~/Scripts/angular/angular-ui/ui-utils.min.js",
                        "~/Scripts/angular/angular-ui/ui-sortable.js",

                        "~/Abp/Framework/scripts/abp.js",
                        "~/Abp/Framework/scripts/libs/abp.jquery.js",
                        "~/Abp/Framework/scripts/libs/abp.toastr.js",
                        "~/Abp/Framework/scripts/libs/abp.blockUI.js",
                        "~/Abp/Framework/scripts/libs/abp.spin.js",
                        "~/Abp/Framework/scripts/libs/angularjs/abp.ng.js"
                    )
                );

            //APPLICATION RESOURCES

            //~/Bundles/App/Main/css
            bundles.Add(
                new StyleBundle("~/Bundles/App/Main/css")
                    .IncludeDirectory("~/App/Main", "*.css", true)
                );

            //~/Bundles/App/Main/js
            bundles.Add(
                new ScriptBundle("~/Bundles/App/Main/js")
                    .IncludeDirectory("~/App/Main", "*.js", true)
                );
        }
    }
}
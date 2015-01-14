﻿using Abp.Application.Navigation;
using Abp.Localization;

namespace EduTesting.Web
{
    /// <summary>
    /// This class defines menus for the application.
    /// It uses ABP's menu system.
    /// When you add menu items here, they are automatically appear in angular application.
    /// See .cshtml and .js files under App/Main/views/layout/header to know how to render menu.
    /// </summary>
    public class EduTestingNavigationProvider : NavigationProvider
    {
        public override void SetNavigation(INavigationProviderContext context)
        {
            context.Manager.MainMenu
                .AddItem(
                    new MenuItemDefinition(
                        "Home",
                        new LocalizableString("HomePage", EduTestingConsts.LocalizationSourceName),
                        url: "#/",
                        icon: "fa fa-home"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "About",
                        new LocalizableString("About", EduTestingConsts.LocalizationSourceName),
                        url: "#/about",
                        icon: "fa fa-info"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Test",
                        new LocalizableString("Test", EduTestingConsts.LocalizationSourceName),
                        url: "#/test/list",
                        icon: "fa fa-briefcase"
                        )
                ).AddItem(
                    new MenuItemDefinition(
                        "Exam",
                        new LocalizableString("PassingTest", EduTestingConsts.LocalizationSourceName),
                        url: "#/test/list",
                        icon: "fa fa-briefcase"
                        )
                );
        }
    }
}

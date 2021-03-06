﻿using System.Reflection;
using Abp.Modules;
using EduTesting.Service;

namespace EduTesting
{
    [DependsOn(typeof(EduTestingCoreModule))]
    public class EduTestingApplicationModule : AbpModule
    {
        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<ITestService, TestService>();
            IocManager.Register<IAccountService, AccountService>();
        }
    }
}

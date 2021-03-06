﻿using System.Data.Entity;
using System.Reflection;
using Abp.EntityFramework;
using Abp.Modules;
using EduTesting.EntityFramework;
using EduTesting.DataProvider;
using Abp.Domain.Uow;
using EduTesting.Controllers;
using EduTesting.Repositories;

namespace EduTesting
{
    [DependsOn(typeof(AbpEntityFrameworkModule), typeof(EduTestingCoreModule))]
    public class EduTestingDataModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = "Default";
        }

        public override void Initialize()
        {
            //IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
            IocManager.Register<IUserRepository, FakeUserProvider>();
            IocManager.Register<IUnitOfWork, FakeUnitOfWork>();
            IocManager.Register<ITestRepository, TestRepository>();
            Database.SetInitializer<EduTestingDbContext>(null);
        }
    }
}

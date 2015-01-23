using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Abp.EntityFramework;
using EduTesting.Model;

namespace EduTesting.EntityFramework
{
    public class EduTestingDbContext : AbpDbContext
    {
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public EduTestingDbContext()
            : base("name=EduTestingDbContext")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in EduTestingDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of EduTestingDbContext since ABP automatically handles it.
         */
        public EduTestingDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public virtual IDbSet<Answer> Answers { get; set; }
        public virtual IDbSet<CustomAttribute> Attributes { get; set; }
        public virtual IDbSet<TestAttribute> TestAttributes { get; set; }
        public virtual IDbSet<QuestionAttribute> QuestionAttributes { get; set; }
        public virtual IDbSet<AnswerAttribute> AnswerAttributes { get; set; }
        public virtual IDbSet<Question> Questions { get; set; }
        public virtual IDbSet<Role> Roles { get; set; }
        public virtual IDbSet<Test> Tests { get; set; }
        public virtual IDbSet<TestResult> TestResults { get; set; }
        public virtual IDbSet<UserGroup> UserGroups { get; set; }
        public virtual IDbSet<User> Users { get; set; }
        public virtual IDbSet<UserAnswer> UsersAnswers { get; set; }
        public virtual IDbSet<TestResultRating> TestResultRatings { get; set; }
    }

    //Example:
    //public class User : Entity
    //{
    //    public string Name { get; set; }

    //    public string Password { get; set; }
    //}
}

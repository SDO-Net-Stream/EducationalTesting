using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace EduTesting.Model
{
    public partial class EduTestingDB : DbContext
    {
        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<CustomAttribute> Attributes { get; set; }
        public virtual DbSet<QuestionAttribute> QuestionAttributes { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Test> Tests { get; set; }
        public virtual DbSet<TestsResult> TestsResults { get; set; }
        public virtual DbSet<UserGroup> UserGroups { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersAnswer> UsersAnswers { get; set; }
    }
}

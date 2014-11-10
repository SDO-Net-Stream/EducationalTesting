namespace EducationalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailLikeLogin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.UserProfile", "FirstName", c => c.String());
            AddColumn("dbo.UserProfile", "LastName", c => c.String());
            DropColumn("dbo.UserProfile", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.UserProfile", "Email", c => c.String());
            DropColumn("dbo.UserProfile", "LastName");
            DropColumn("dbo.UserProfile", "FirstName");
        }
    }
}

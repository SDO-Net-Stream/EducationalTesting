namespace EducationalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNames : DbMigration
    {
      public override void Up()
      {
        AddColumn("dbo.UserProfile", "FirstName", c => c.String());
        AddColumn("dbo.UserProfile", "LastName", c => c.String());
      }

      public override void Down()
      {
        DropColumn("dbo.UserProfile", "LastName");
        DropColumn("dbo.UserProfile", "FirstName");
      }
    }
}

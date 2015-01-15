namespace EduTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserFieldsRename : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "UserFirstName", c => c.String());
            AddColumn("dbo.Users", "UserLastName", c => c.String());
            AddColumn("dbo.Users", "UserDateCreated", c => c.DateTime());
            AddColumn("dbo.Users", "UserActivated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UserDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "UserDomainName", c => c.String());
            AddColumn("dbo.Users", "UserPassword", c => c.String());
            AddColumn("dbo.Users", "UserPasswordSalt", c => c.String());
            AddColumn("dbo.Users", "UserPasswordVerificationToken", c => c.String());
            DropColumn("dbo.Users", "FirstName");
            DropColumn("dbo.Users", "LastName");
            DropColumn("dbo.Users", "DateCreated");
            DropColumn("dbo.Users", "Activated");
            DropColumn("dbo.Users", "Deleted");
            DropColumn("dbo.Users", "DomainName");
            DropColumn("dbo.Users", "Password");
            DropColumn("dbo.Users", "PasswordSalt");
            DropColumn("dbo.Users", "PasswordVerificationToken");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "PasswordVerificationToken", c => c.String());
            AddColumn("dbo.Users", "PasswordSalt", c => c.String());
            AddColumn("dbo.Users", "Password", c => c.String());
            AddColumn("dbo.Users", "DomainName", c => c.String());
            AddColumn("dbo.Users", "Deleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "Activated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Users", "DateCreated", c => c.DateTime());
            AddColumn("dbo.Users", "LastName", c => c.String());
            AddColumn("dbo.Users", "FirstName", c => c.String());
            DropColumn("dbo.Users", "UserPasswordVerificationToken");
            DropColumn("dbo.Users", "UserPasswordSalt");
            DropColumn("dbo.Users", "UserPassword");
            DropColumn("dbo.Users", "UserDomainName");
            DropColumn("dbo.Users", "UserDeleted");
            DropColumn("dbo.Users", "UserActivated");
            DropColumn("dbo.Users", "UserDateCreated");
            DropColumn("dbo.Users", "UserLastName");
            DropColumn("dbo.Users", "UserFirstName");
        }
    }
}

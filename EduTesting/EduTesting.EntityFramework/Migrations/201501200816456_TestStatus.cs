namespace EduTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TestStatus : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tests", "GroupId", "dbo.UserGroups");
            DropForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles");
            DropIndex("dbo.Tests", new[] { "GroupId" });
            DropPrimaryKey("dbo.Roles");
            CreateTable(
                "dbo.TestResultRatings",
                c => new
                    {
                        RatingId = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        RatingLowerBound = c.Int(nullable: false),
                        RatingTitle = c.String(),
                    })
                .PrimaryKey(t => t.RatingId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.UserGroupTests",
                c => new
                    {
                        UserGroup_GroupID = c.Int(nullable: false),
                        Test_TestId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_GroupID, t.Test_TestId })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.Test_TestId, cascadeDelete: true)
                .Index(t => t.UserGroup_GroupID)
                .Index(t => t.Test_TestId);
            
            AddColumn("dbo.Answers", "AnswerOrder", c => c.Int(nullable: false));
            AddColumn("dbo.Answers", "AnswerScore", c => c.Int(nullable: false));
            AddColumn("dbo.Questions", "QuestionType", c => c.Int(nullable: false));
            AddColumn("dbo.Tests", "TestStatus", c => c.Int(nullable: false));
            AddColumn("dbo.TestResults", "TestResultBeginTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.TestResults", "TestResultEndTime", c => c.DateTime());
            AddColumn("dbo.TestResults", "TestResultStatus", c => c.Int(nullable: false));
            AddColumn("dbo.TestResults", "TestResultScore", c => c.Int(nullable: false));
            AddColumn("dbo.TestResults", "TestResultRating_RatingId", c => c.Int());

            AddColumn("dbo.Roles", "Role_ID", c => c.Int(nullable: false, identity: false));
            Sql("update dbo.Roles set Role_ID = RoleID");
            DropColumn("dbo.Roles", "RoleID");
            RenameColumn("dbo.Roles", "Role_ID", "RoleID");

            AddPrimaryKey("dbo.Roles", "RoleID");
            CreateIndex("dbo.TestResults", "TestResultRating_RatingId");
            AddForeignKey("dbo.TestResults", "TestResultRating_RatingId", "dbo.TestResultRatings", "RatingId");
            AddForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            DropColumn("dbo.Tests", "NumberOfQuestions");
            DropColumn("dbo.Tests", "GroupId");
            DropColumn("dbo.TestResults", "IsCompleted");
            DropColumn("dbo.TestResults", "Timestamp");
        }
        
        public override void Down()
        {
            AddColumn("dbo.TestResults", "Timestamp", c => c.DateTime(nullable: false));
            AddColumn("dbo.TestResults", "IsCompleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Tests", "GroupId", c => c.Int());
            AddColumn("dbo.Tests", "NumberOfQuestions", c => c.Int(nullable: false));
            DropForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.UserGroupTests", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.UserGroupTests", "UserGroup_GroupID", "dbo.UserGroups");
            DropForeignKey("dbo.TestResults", "TestResultRating_RatingId", "dbo.TestResultRatings");
            DropForeignKey("dbo.TestResultRatings", "TestId", "dbo.Tests");
            DropIndex("dbo.UserGroupTests", new[] { "Test_TestId" });
            DropIndex("dbo.UserGroupTests", new[] { "UserGroup_GroupID" });
            DropIndex("dbo.TestResults", new[] { "TestResultRating_RatingId" });
            DropIndex("dbo.TestResultRatings", new[] { "TestId" });
            DropPrimaryKey("dbo.Roles");

            AddColumn("dbo.Roles", "Role_ID", c => c.Int(nullable: false, identity: true));
            Sql("update dbo.Roles set Role_ID = RoleID");
            DropColumn("dbo.Roles", "RoleID");
            RenameColumn("dbo.Roles", "Role_ID", "RoleID");

            DropColumn("dbo.TestResults", "TestResultRating_RatingId");
            DropColumn("dbo.TestResults", "TestResultScore");
            DropColumn("dbo.TestResults", "TestResultStatus");
            DropColumn("dbo.TestResults", "TestResultEndTime");
            DropColumn("dbo.TestResults", "TestResultBeginTime");
            DropColumn("dbo.Tests", "TestStatus");
            DropColumn("dbo.Questions", "QuestionType");
            DropColumn("dbo.Answers", "AnswerScore");
            DropColumn("dbo.Answers", "AnswerOrder");
            DropTable("dbo.UserGroupTests");
            DropTable("dbo.TestResultRatings");
            AddPrimaryKey("dbo.Roles", "RoleID");
            CreateIndex("dbo.Tests", "GroupId");
            AddForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles", "RoleID", cascadeDelete: true);
            AddForeignKey("dbo.Tests", "GroupId", "dbo.UserGroups", "GroupID");
        }
    }
}

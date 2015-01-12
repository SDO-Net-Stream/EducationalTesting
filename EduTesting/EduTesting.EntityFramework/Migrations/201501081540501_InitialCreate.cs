namespace EduTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        AnswerId = c.Int(nullable: false, identity: true),
                        QuestionId = c.Int(nullable: false),
                        AnswerText = c.String(),
                    })
                .PrimaryKey(t => t.AnswerId)
                .ForeignKey("dbo.Questions", t => t.QuestionId, cascadeDelete: true)
                .Index(t => t.QuestionId);
            
            CreateTable(
                "dbo.CustomAttributes",
                c => new
                    {
                        AttributeID = c.Int(nullable: false, identity: true),
                        AttributeName = c.String(),
                    })
                .PrimaryKey(t => t.AttributeID);
            
            CreateTable(
                "dbo.QuestionAttributes",
                c => new
                    {
                        QuestionID = c.Int(nullable: false),
                        AttributeID = c.Int(nullable: false),
                        Value = c.String(),
                    })
                .PrimaryKey(t => new { t.QuestionID, t.AttributeID })
                .ForeignKey("dbo.CustomAttributes", t => t.AttributeID, cascadeDelete: true)
                .ForeignKey("dbo.Questions", t => t.QuestionID, cascadeDelete: true)
                .Index(t => t.QuestionID)
                .Index(t => t.AttributeID);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        QuestionId = c.Int(nullable: false, identity: true),
                        TestId = c.Int(nullable: false),
                        QuestionText = c.String(),
                        QuestionDescription = c.String(),
                    })
                .PrimaryKey(t => t.QuestionId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        TestId = c.Int(nullable: false, identity: true),
                        TestName = c.String(),
                        TestDescription = c.String(),
                        NumberOfQuestions = c.Int(nullable: false),
                        GroupId = c.Int(),
                    })
                .PrimaryKey(t => t.TestId)
                .ForeignKey("dbo.UserGroups", t => t.GroupId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "dbo.TestResults",
                c => new
                    {
                        TestResultId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TestId = c.Int(nullable: false),
                        IsCompleted = c.Boolean(nullable: false),
                        Timestamp = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.TestResultId)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.TestId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        UserEmail = c.String(),
                        DateCreated = c.DateTime(),
                        Activated = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        DomainName = c.String(),
                        Password = c.String(),
                        PasswordSalt = c.String(),
                        PasswordVerificationToken = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        GroupID = c.Int(nullable: false, identity: true),
                        GroupName = c.String(),
                    })
                .PrimaryKey(t => t.GroupID);
            
            CreateTable(
                "dbo.UserAnswers",
                c => new
                    {
                        UserAnswerId = c.Int(nullable: false, identity: true),
                        TestResultId = c.Int(nullable: false),
                        CustomAnswerText = c.String(),
                        Answer_AnswerId = c.Int(),
                        Question_QuestionId = c.Int(),
                    })
                .PrimaryKey(t => t.UserAnswerId)
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerId)
                .ForeignKey("dbo.Questions", t => t.Question_QuestionId)
                .ForeignKey("dbo.TestResults", t => t.TestResultId, cascadeDelete: true)
                .Index(t => t.TestResultId)
                .Index(t => t.Answer_AnswerId)
                .Index(t => t.Question_QuestionId);
            
            CreateTable(
                "dbo.CustomAttributeAnswers",
                c => new
                    {
                        CustomAttribute_AttributeID = c.Int(nullable: false),
                        Answer_AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomAttribute_AttributeID, t.Answer_AnswerId })
                .ForeignKey("dbo.CustomAttributes", t => t.CustomAttribute_AttributeID, cascadeDelete: true)
                .ForeignKey("dbo.Answers", t => t.Answer_AnswerId, cascadeDelete: true)
                .Index(t => t.CustomAttribute_AttributeID)
                .Index(t => t.Answer_AnswerId);
            
            CreateTable(
                "dbo.TestCustomAttributes",
                c => new
                    {
                        Test_TestId = c.Int(nullable: false),
                        CustomAttribute_AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_TestId, t.CustomAttribute_AttributeID })
                .ForeignKey("dbo.Tests", t => t.Test_TestId, cascadeDelete: true)
                .ForeignKey("dbo.CustomAttributes", t => t.CustomAttribute_AttributeID, cascadeDelete: true)
                .Index(t => t.Test_TestId)
                .Index(t => t.CustomAttribute_AttributeID);
            
            CreateTable(
                "dbo.RoleUsers",
                c => new
                    {
                        Role_RoleID = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Role_RoleID, t.User_UserId })
                .ForeignKey("dbo.Roles", t => t.Role_RoleID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.Role_RoleID)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.UserGroupUsers",
                c => new
                    {
                        UserGroup_GroupID = c.Int(nullable: false),
                        User_UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserGroup_GroupID, t.User_UserId })
                .ForeignKey("dbo.UserGroups", t => t.UserGroup_GroupID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId, cascadeDelete: true)
                .Index(t => t.UserGroup_GroupID)
                .Index(t => t.User_UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "TestResultId", "dbo.TestResults");
            DropForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions");
            DropForeignKey("dbo.UserAnswers", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.UserGroupUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.UserGroupUsers", "UserGroup_GroupID", "dbo.UserGroups");
            DropForeignKey("dbo.Tests", "GroupId", "dbo.UserGroups");
            DropForeignKey("dbo.TestResults", "UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.RoleUsers", "Role_RoleID", "dbo.Roles");
            DropForeignKey("dbo.TestResults", "TestId", "dbo.Tests");
            DropForeignKey("dbo.Questions", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestCustomAttributes", "CustomAttribute_AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.TestCustomAttributes", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.QuestionAttributes", "QuestionID", "dbo.Questions");
            DropForeignKey("dbo.Answers", "QuestionId", "dbo.Questions");
            DropForeignKey("dbo.QuestionAttributes", "AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.CustomAttributeAnswers", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.CustomAttributeAnswers", "CustomAttribute_AttributeID", "dbo.CustomAttributes");
            DropIndex("dbo.UserGroupUsers", new[] { "User_UserId" });
            DropIndex("dbo.UserGroupUsers", new[] { "UserGroup_GroupID" });
            DropIndex("dbo.RoleUsers", new[] { "User_UserId" });
            DropIndex("dbo.RoleUsers", new[] { "Role_RoleID" });
            DropIndex("dbo.TestCustomAttributes", new[] { "CustomAttribute_AttributeID" });
            DropIndex("dbo.TestCustomAttributes", new[] { "Test_TestId" });
            DropIndex("dbo.CustomAttributeAnswers", new[] { "Answer_AnswerId" });
            DropIndex("dbo.CustomAttributeAnswers", new[] { "CustomAttribute_AttributeID" });
            DropIndex("dbo.UserAnswers", new[] { "Question_QuestionId" });
            DropIndex("dbo.UserAnswers", new[] { "Answer_AnswerId" });
            DropIndex("dbo.UserAnswers", new[] { "TestResultId" });
            DropIndex("dbo.TestResults", new[] { "TestId" });
            DropIndex("dbo.TestResults", new[] { "UserId" });
            DropIndex("dbo.Tests", new[] { "GroupId" });
            DropIndex("dbo.Questions", new[] { "TestId" });
            DropIndex("dbo.QuestionAttributes", new[] { "AttributeID" });
            DropIndex("dbo.QuestionAttributes", new[] { "QuestionID" });
            DropIndex("dbo.Answers", new[] { "QuestionId" });
            DropTable("dbo.UserGroupUsers");
            DropTable("dbo.RoleUsers");
            DropTable("dbo.TestCustomAttributes");
            DropTable("dbo.CustomAttributeAnswers");
            DropTable("dbo.UserAnswers");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.TestResults");
            DropTable("dbo.Tests");
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionAttributes");
            DropTable("dbo.CustomAttributes");
            DropTable("dbo.Answers");
        }
    }
}

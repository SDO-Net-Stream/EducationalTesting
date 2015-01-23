namespace EduTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAnswer_QuestionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "Question_QuestionId" });
            RenameColumn(table: "dbo.UserAnswers", name: "Question_QuestionId", newName: "QuestionId");
            DropColumn("dbo.Answers", "AnswerScore");
            AddColumn("dbo.Answers", "AnswerScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TestResultRatings", "RatingLowerBound");
            AddColumn("dbo.TestResultRatings", "RatingLowerBound", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.TestResults", "TestResultScore");
            AddColumn("dbo.TestResults", "TestResultScore", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int(nullable: false));
            CreateIndex("dbo.UserAnswers", "QuestionId");
            AddForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions", "QuestionId", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserAnswers", "QuestionId", "dbo.Questions");
            DropIndex("dbo.UserAnswers", new[] { "QuestionId" });
            AlterColumn("dbo.UserAnswers", "QuestionId", c => c.Int());
            AlterColumn("dbo.TestResults", "TestResultScore", c => c.Int(nullable: false));
            AlterColumn("dbo.TestResultRatings", "RatingLowerBound", c => c.Int(nullable: false));
            AlterColumn("dbo.Answers", "AnswerScore", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.UserAnswers", name: "QuestionId", newName: "Question_QuestionId");
            CreateIndex("dbo.UserAnswers", "Question_QuestionId");
            AddForeignKey("dbo.UserAnswers", "Question_QuestionId", "dbo.Questions", "QuestionId");
        }
    }
}

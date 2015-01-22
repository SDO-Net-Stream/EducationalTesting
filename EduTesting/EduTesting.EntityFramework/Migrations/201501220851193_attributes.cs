namespace EduTesting.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class attributes : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomAttributeAnswers", "CustomAttribute_AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.CustomAttributeAnswers", "Answer_AnswerId", "dbo.Answers");
            DropForeignKey("dbo.TestCustomAttributes", "Test_TestId", "dbo.Tests");
            DropForeignKey("dbo.TestCustomAttributes", "CustomAttribute_AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.AnswerAttributes", "AttributeId", "dbo.CustomAttributes");
            DropForeignKey("dbo.QuestionAttributes", "AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.TestAttributes", "AttributeId", "dbo.CustomAttributes");
            DropIndex("dbo.CustomAttributeAnswers", new[] { "CustomAttribute_AttributeID" });
            DropIndex("dbo.CustomAttributeAnswers", new[] { "Answer_AnswerId" });
            DropIndex("dbo.TestCustomAttributes", new[] { "Test_TestId" });
            DropIndex("dbo.TestCustomAttributes", new[] { "CustomAttribute_AttributeID" });
            DropPrimaryKey("dbo.CustomAttributes");
            CreateTable(
                "dbo.AnswerAttributes",
                c => new
                    {
                        AnswerId = c.Int(nullable: false),
                        AttributeId = c.Int(nullable: false),
                        AttributeValue = c.String(),
                    })
                .PrimaryKey(t => new { t.AnswerId, t.AttributeId })
                .ForeignKey("dbo.Answers", t => t.AnswerId, cascadeDelete: true)
                .ForeignKey("dbo.CustomAttributes", t => t.AttributeId, cascadeDelete: true)
                .Index(t => t.AnswerId)
                .Index(t => t.AttributeId);
            
            CreateTable(
                "dbo.TestAttributes",
                c => new
                    {
                        TestId = c.Int(nullable: false),
                        AttributeId = c.Int(nullable: false),
                        AttributeValue = c.String(),
                    })
                .PrimaryKey(t => new { t.TestId, t.AttributeId })
                .ForeignKey("dbo.CustomAttributes", t => t.AttributeId, cascadeDelete: true)
                .ForeignKey("dbo.Tests", t => t.TestId, cascadeDelete: true)
                .Index(t => t.TestId)
                .Index(t => t.AttributeId);
            
            AddColumn("dbo.Questions", "QuestionOrder", c => c.Int(nullable: false));

            //AlterColumn("dbo.CustomAttributes", "AttributeID", c => c.Int(nullable: false));
            AddColumn("dbo.CustomAttributes", "Attribute_ID", c => c.Int(nullable: false, identity: false));
            Sql("update dbo.CustomAttributes set Attribute_ID = AttributeID");
            DropColumn("dbo.CustomAttributes", "AttributeID");
            RenameColumn("dbo.CustomAttributes", "Attribute_ID", "AttributeID");

            AddPrimaryKey("dbo.CustomAttributes", "AttributeID");
            AddForeignKey("dbo.QuestionAttributes", "AttributeID", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
            DropTable("dbo.CustomAttributeAnswers");
            DropTable("dbo.TestCustomAttributes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TestCustomAttributes",
                c => new
                    {
                        Test_TestId = c.Int(nullable: false),
                        CustomAttribute_AttributeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Test_TestId, t.CustomAttribute_AttributeID });
            
            CreateTable(
                "dbo.CustomAttributeAnswers",
                c => new
                    {
                        CustomAttribute_AttributeID = c.Int(nullable: false),
                        Answer_AnswerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.CustomAttribute_AttributeID, t.Answer_AnswerId });
            
            DropForeignKey("dbo.QuestionAttributes", "AttributeID", "dbo.CustomAttributes");
            DropForeignKey("dbo.TestAttributes", "TestId", "dbo.Tests");
            DropForeignKey("dbo.TestAttributes", "AttributeId", "dbo.CustomAttributes");
            DropForeignKey("dbo.AnswerAttributes", "AttributeId", "dbo.CustomAttributes");
            DropForeignKey("dbo.AnswerAttributes", "AnswerId", "dbo.Answers");
            DropIndex("dbo.TestAttributes", new[] { "AttributeId" });
            DropIndex("dbo.TestAttributes", new[] { "TestId" });
            DropIndex("dbo.AnswerAttributes", new[] { "AttributeId" });
            DropIndex("dbo.AnswerAttributes", new[] { "AnswerId" });
            DropPrimaryKey("dbo.CustomAttributes");

            //AlterColumn("dbo.CustomAttributes", "AttributeID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CustomAttributes", "Attribute_ID", c => c.Int(nullable: false, identity: true));
            Sql("update dbo.CustomAttributes set Attribute_ID = AttributeID");
            DropColumn("dbo.CustomAttributes", "AttributeID");
            RenameColumn("dbo.CustomAttributes", "Attribute_ID", "AttributeID");

            DropColumn("dbo.Questions", "QuestionOrder");
            DropTable("dbo.TestAttributes");
            DropTable("dbo.AnswerAttributes");
            AddPrimaryKey("dbo.CustomAttributes", "AttributeID");
            CreateIndex("dbo.TestCustomAttributes", "CustomAttribute_AttributeID");
            CreateIndex("dbo.TestCustomAttributes", "Test_TestId");
            CreateIndex("dbo.CustomAttributeAnswers", "Answer_AnswerId");
            CreateIndex("dbo.CustomAttributeAnswers", "CustomAttribute_AttributeID");
            AddForeignKey("dbo.TestAttributes", "AttributeId", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
            AddForeignKey("dbo.QuestionAttributes", "AttributeID", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
            AddForeignKey("dbo.AnswerAttributes", "AttributeId", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
            AddForeignKey("dbo.TestCustomAttributes", "CustomAttribute_AttributeID", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
            AddForeignKey("dbo.TestCustomAttributes", "Test_TestId", "dbo.Tests", "TestId", cascadeDelete: true);
            AddForeignKey("dbo.CustomAttributeAnswers", "Answer_AnswerId", "dbo.Answers", "AnswerId", cascadeDelete: true);
            AddForeignKey("dbo.CustomAttributeAnswers", "CustomAttribute_AttributeID", "dbo.CustomAttributes", "AttributeID", cascadeDelete: true);
        }
    }
}

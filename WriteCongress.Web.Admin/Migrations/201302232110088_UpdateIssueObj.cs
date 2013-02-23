namespace WriteCongress.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateIssueObj : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Letters", "Issue_IssueID", "dbo.Issues");
            DropIndex("dbo.Letters", new[] { "Issue_IssueID" });
            CreateTable(
                "dbo.UserProfile",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            DropTable("dbo.Letters");
            DropTable("dbo.Issues");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Issues",
                c => new
                    {
                        IssueID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.IssueID);
            
            CreateTable(
                "dbo.Letters",
                c => new
                    {
                        LetterId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                        ImagePath = c.String(),
                        Body = c.String(),
                        Against = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Issue_IssueID = c.Int(),
                    })
                .PrimaryKey(t => t.LetterId);
            
            DropTable("dbo.UserProfile");
            CreateIndex("dbo.Letters", "Issue_IssueID");
            AddForeignKey("dbo.Letters", "Issue_IssueID", "dbo.Issues", "IssueID");
        }
    }
}

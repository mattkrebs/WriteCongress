namespace WriteCongress.Web.Admin.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
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
                .PrimaryKey(t => t.LetterId)
                .ForeignKey("dbo.Issues", t => t.Issue_IssueID)
                .Index(t => t.Issue_IssueID);
            
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
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Letters", new[] { "Issue_IssueID" });
            DropForeignKey("dbo.Letters", "Issue_IssueID", "dbo.Issues");
            DropTable("dbo.Issues");
            DropTable("dbo.Letters");
        }
    }
}

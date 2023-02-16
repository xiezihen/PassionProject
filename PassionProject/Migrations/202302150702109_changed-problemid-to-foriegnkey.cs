namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedproblemidtoforiegnkey : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Holds", "ProblemID");
            AddForeignKey("dbo.Holds", "ProblemID", "dbo.Problems", "ProblemID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Holds", "ProblemID", "dbo.Problems");
            DropIndex("dbo.Holds", new[] { "ProblemID" });
        }
    }
}

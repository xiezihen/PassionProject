namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class problems : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Problems",
                c => new
                    {
                        ProblemID = c.Int(nullable: false, identity: true),
                        ProblemName = c.String(),
                        ProblemGrade = c.String(),
                    })
                .PrimaryKey(t => t.ProblemID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Problems");
        }
    }
}

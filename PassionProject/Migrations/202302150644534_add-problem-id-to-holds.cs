namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addproblemidtoholds : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Holds", "ProblemID", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Holds", "ProblemID");
        }
    }
}

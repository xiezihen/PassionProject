namespace PassionProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class holds : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Holds",
                c => new
                    {
                        HoldID = c.Int(nullable: false, identity: true),
                        PositionX = c.Int(nullable: false),
                        PositionY = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.HoldID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Holds");
        }
    }
}

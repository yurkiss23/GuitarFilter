namespace GuitarFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblFilterValues : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterValues",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 250),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.tblFilterValues");
        }
    }
}

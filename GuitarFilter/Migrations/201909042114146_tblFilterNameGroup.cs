namespace GuitarFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblFilterNameGroup : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilterNameGroups",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId })
                .ForeignKey("dbo.tblFilterName", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValueId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilterNameGroups", "FilterValueId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilterNameGroups", "FilterNameId", "dbo.tblFilterName");
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilterNameGroups", new[] { "FilterNameId" });
            DropTable("dbo.tblFilterNameGroups");
        }
    }
}

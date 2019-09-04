namespace GuitarFilter.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblFilters : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblFilters",
                c => new
                    {
                        FilterNameId = c.Int(nullable: false),
                        FilterValueId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.FilterNameId, t.FilterValueId, t.ProductId })
                .ForeignKey("dbo.tblFilterName", t => t.FilterNameId, cascadeDelete: true)
                .ForeignKey("dbo.tblFilterValues", t => t.FilterValueId, cascadeDelete: true)
                .ForeignKey("dbo.tblProducts", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.FilterNameId)
                .Index(t => t.FilterValueId)
                .Index(t => t.ProductId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tblFilters", "ProductId", "dbo.tblProducts");
            DropForeignKey("dbo.tblFilters", "FilterValueId", "dbo.tblFilterValues");
            DropForeignKey("dbo.tblFilters", "FilterNameId", "dbo.tblFilterName");
            DropIndex("dbo.tblFilters", new[] { "ProductId" });
            DropIndex("dbo.tblFilters", new[] { "FilterValueId" });
            DropIndex("dbo.tblFilters", new[] { "FilterNameId" });
            DropTable("dbo.tblFilters");
        }
    }
}

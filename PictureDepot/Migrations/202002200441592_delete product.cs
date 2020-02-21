namespace PictureDepot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteproduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "ProductId", "dbo.Products");
            DropIndex("dbo.Artists", new[] { "ProductId" });
            DropColumn("dbo.Artists", "ProductId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.Artists", "ProductId");
            AddForeignKey("dbo.Artists", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}

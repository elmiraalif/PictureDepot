namespace PictureDepot.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtheforeignkeytoproduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Artists", "ProductId", "dbo.Products");
            AddColumn("dbo.Artists", "Product_ProductId", c => c.Int());
            AddColumn("dbo.Products", "ArtistId", c => c.Int(nullable: false));
            CreateIndex("dbo.Artists", "Product_ProductId");
            CreateIndex("dbo.Products", "ArtistId");
            AddForeignKey("dbo.Products", "ArtistId", "dbo.Artists", "ArtistId", cascadeDelete: true);
            AddForeignKey("dbo.Artists", "Product_ProductId", "dbo.Products", "ProductId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Artists", "Product_ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "ArtistId", "dbo.Artists");
            DropIndex("dbo.Products", new[] { "ArtistId" });
            DropIndex("dbo.Artists", new[] { "Product_ProductId" });
            DropColumn("dbo.Products", "ArtistId");
            DropColumn("dbo.Artists", "Product_ProductId");
            AddForeignKey("dbo.Artists", "ProductId", "dbo.Products", "ProductId", cascadeDelete: true);
        }
    }
}

namespace ArtShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Artist",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        LifeSpan = c.String(maxLength: 10),
                        Country = c.String(maxLength: 50),
                        Description = c.String(maxLength: 2000),
                        TotalProducts = c.Int(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Product",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        Description = c.String(nullable: false, maxLength: 2000),
                        Image = c.String(nullable: false),
                        Price = c.Double(nullable: false),
                        QuantitySold = c.Int(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                        AvgStars = c.Double(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artist", t => t.ArtistId, cascadeDelete: true)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.CartItem",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        CartId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Cart", t => t.CartId, cascadeDelete: true)
                .Index(t => t.CartId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.OrderDetail",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Double(nullable: false),
                        Quantity = c.Int(nullable: false),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Order", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Rating",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Stars = c.Int(nullable: false),
                        UserName = c.String(),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Product", t => t.Product_Id)
                .Index(t => t.Product_Id);
            
            CreateTable(
                "dbo.Cart",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Cookie = c.String(nullable: false, maxLength: 50),
                        CartDate = c.DateTime(nullable: false),
                        ItemCount = c.Int(nullable: false),
                        Disabled = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Error",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 256),
                        ErrorDate = c.DateTime(),
                        IpAddress = c.String(maxLength: 50),
                        UserAgent = c.String(),
                        Exception = c.String(),
                        Message = c.String(),
                        Everything = c.String(),
                        HttpReferer = c.String(maxLength: 500),
                        PathAndQuery = c.String(maxLength: 500),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                        OrderNumber = c.Int(nullable: false),
                        ItemCount = c.Int(nullable: false),
                        ShippingId = c.Int(nullable: false),
                        Email = c.String(nullable: false, maxLength: 100),
                        Disabled = c.Boolean(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Shipping", t => t.ShippingId, cascadeDelete: true)
                .Index(t => t.ShippingId);
            
            CreateTable(
                "dbo.Shipping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 30),
                        LastName = c.String(nullable: false, maxLength: 30),
                        Email = c.String(nullable: false, maxLength: 100),
                        Address = c.String(nullable: false, maxLength: 300),
                        City = c.String(nullable: false, maxLength: 30),
                        Country = c.String(nullable: false, maxLength: 30),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderNumber",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Number = c.Int(nullable: false),
                        CreatedOn = c.DateTime(nullable: false),
                        CreatedBy = c.String(nullable: false),
                        ChangedOn = c.DateTime(nullable: false),
                        ChangedBy = c.String(maxLength: 256),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Order", "ShippingId", "dbo.Shipping");
            DropForeignKey("dbo.OrderDetail", "OrderId", "dbo.Order");
            DropForeignKey("dbo.CartItem", "CartId", "dbo.Cart");
            DropForeignKey("dbo.Rating", "Product_Id", "dbo.Product");
            DropForeignKey("dbo.OrderDetail", "ProductId", "dbo.Product");
            DropForeignKey("dbo.CartItem", "ProductId", "dbo.Product");
            DropForeignKey("dbo.Product", "ArtistId", "dbo.Artist");
            DropIndex("dbo.Order", new[] { "ShippingId" });
            DropIndex("dbo.Rating", new[] { "Product_Id" });
            DropIndex("dbo.OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.OrderDetail", new[] { "OrderId" });
            DropIndex("dbo.CartItem", new[] { "ProductId" });
            DropIndex("dbo.CartItem", new[] { "CartId" });
            DropIndex("dbo.Product", new[] { "ArtistId" });
            DropTable("dbo.OrderNumber");
            DropTable("dbo.Shipping");
            DropTable("dbo.Order");
            DropTable("dbo.Error");
            DropTable("dbo.Cart");
            DropTable("dbo.Rating");
            DropTable("dbo.OrderDetail");
            DropTable("dbo.CartItem");
            DropTable("dbo.Product");
            DropTable("dbo.Artist");
        }
    }
}

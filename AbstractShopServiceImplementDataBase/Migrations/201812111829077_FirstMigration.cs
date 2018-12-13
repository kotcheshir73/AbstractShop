namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClientId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Status = c.Int(nullable: false),
                        DateCreate = c.DateTime(nullable: false),
                        DateImplement = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ClientId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductName = c.String(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        ComponentId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.ComponentId);
            
            CreateTable(
                "dbo.Components",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComponentName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StockComponents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockId = c.Int(nullable: false),
                        ComponentId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Components", t => t.ComponentId, cascadeDelete: true)
                .ForeignKey("dbo.Stocks", t => t.StockId, cascadeDelete: true)
                .Index(t => t.StockId)
                .Index(t => t.ComponentId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StockName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductComponents", "ProductId", "dbo.Products");
            DropForeignKey("dbo.StockComponents", "StockId", "dbo.Stocks");
            DropForeignKey("dbo.StockComponents", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.ProductComponents", "ComponentId", "dbo.Components");
            DropForeignKey("dbo.Orders", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Orders", "ClientId", "dbo.Clients");
            DropIndex("dbo.StockComponents", new[] { "ComponentId" });
            DropIndex("dbo.StockComponents", new[] { "StockId" });
            DropIndex("dbo.ProductComponents", new[] { "ComponentId" });
            DropIndex("dbo.ProductComponents", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "ProductId" });
            DropIndex("dbo.Orders", new[] { "ClientId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.StockComponents");
            DropTable("dbo.Components");
            DropTable("dbo.ProductComponents");
            DropTable("dbo.Products");
            DropTable("dbo.Orders");
            DropTable("dbo.Clients");
        }
    }
}

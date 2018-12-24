namespace AbstractShopServiceImplementDataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImplementer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Implementers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ImplementerFIO = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "ImplementerId", c => c.Int());
            CreateIndex("dbo.Orders", "ImplementerId");
            AddForeignKey("dbo.Orders", "ImplementerId", "dbo.Implementers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ImplementerId", "dbo.Implementers");
            DropIndex("dbo.Orders", new[] { "ImplementerId" });
            DropColumn("dbo.Orders", "ImplementerId");
            DropTable("dbo.Implementers");
        }
    }
}

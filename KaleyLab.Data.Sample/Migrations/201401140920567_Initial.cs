namespace KaleyLab.Data.Sample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Order",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        OrderNo = c.String(),
                        CreateUser = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        Comment = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.OrderItem",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        GoodName = c.String(),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Comment = c.String(),
                        Order_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Order", t => t.Order_Id, cascadeDelete: true)
                .Index(t => t.Order_Id);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.OrderItem", new[] { "Order_Id" });
            DropForeignKey("dbo.OrderItem", "Order_Id", "dbo.Order");
            DropTable("dbo.OrderItem");
            DropTable("dbo.Order");
        }
    }
}

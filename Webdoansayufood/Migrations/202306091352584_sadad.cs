namespace Webdoansayufood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.tb_OrderDetail", "Order_Id", "dbo.tb_Order");
            DropForeignKey("dbo.tb_OrderDetail", "Product_Id", "dbo.tb_Product");
            DropIndex("dbo.tb_OrderDetail", new[] { "Order_Id" });
            DropIndex("dbo.tb_OrderDetail", new[] { "Product_Id" });
            RenameColumn(table: "dbo.tb_OrderDetail", name: "Order_Id", newName: "OrderId");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.tb_OrderDetail", "OrderId", c => c.Int(nullable: false));
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.tb_OrderDetail", "OrderId");
            CreateIndex("dbo.tb_OrderDetail", "ProductId");
            AddForeignKey("dbo.tb_OrderDetail", "OrderId", "dbo.tb_Order", "Id", cascadeDelete: true);
            AddForeignKey("dbo.tb_OrderDetail", "ProductId", "dbo.tb_Product", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tb_OrderDetail", "ProductId", "dbo.tb_Product");
            DropForeignKey("dbo.tb_OrderDetail", "OrderId", "dbo.tb_Order");
            DropIndex("dbo.tb_OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.tb_OrderDetail", new[] { "OrderId" });
            AlterColumn("dbo.tb_OrderDetail", "ProductId", c => c.Int());
            AlterColumn("dbo.tb_OrderDetail", "OrderId", c => c.Int());
            RenameColumn(table: "dbo.tb_OrderDetail", name: "ProductId", newName: "Product_Id");
            RenameColumn(table: "dbo.tb_OrderDetail", name: "OrderId", newName: "Order_Id");
            CreateIndex("dbo.tb_OrderDetail", "Product_Id");
            CreateIndex("dbo.tb_OrderDetail", "Order_Id");
            AddForeignKey("dbo.tb_OrderDetail", "Product_Id", "dbo.tb_Product", "Id");
            AddForeignKey("dbo.tb_OrderDetail", "Order_Id", "dbo.tb_Order", "Id");
        }
    }
}

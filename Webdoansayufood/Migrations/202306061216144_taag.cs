namespace Webdoansayufood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class taag : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_OrderDetail", "Total", c => c.Double(nullable: false));
            DropColumn("dbo.tb_Order", "TotalAmout");
            DropColumn("dbo.tb_Order", "Quantity");
            DropColumn("dbo.tb_Order", "TypePayment");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "TypePayment", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "Quantity", c => c.Int(nullable: false));
            AddColumn("dbo.tb_Order", "TotalAmout", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.tb_OrderDetail", "Total");
        }
    }
}

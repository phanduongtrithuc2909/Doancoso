namespace Webdoansayufood.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tb_Order", "Email", c => c.String(nullable: false));
            DropColumn("dbo.tb_Order", "Code");
        }
        
        public override void Down()
        {
            AddColumn("dbo.tb_Order", "Code", c => c.String(nullable: false));
            DropColumn("dbo.tb_Order", "Email");
        }
    }
}

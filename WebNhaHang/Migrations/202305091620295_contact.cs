namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class contact : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Contact", "Phone", c => c.String());
            AddColumn("dbo.Tb_Contact", "CreateBy", c => c.String());
            AddColumn("dbo.Tb_Contact", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Contact", "ModifieDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Contact", "ModifieBy", c => c.String());
            DropColumn("dbo.Tb_Contact", "Website");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Contact", "Website", c => c.String());
            DropColumn("dbo.Tb_Contact", "ModifieBy");
            DropColumn("dbo.Tb_Contact", "ModifieDate");
            DropColumn("dbo.Tb_Contact", "CreateDate");
            DropColumn("dbo.Tb_Contact", "CreateBy");
            DropColumn("dbo.Tb_Contact", "Phone");
        }
    }
}

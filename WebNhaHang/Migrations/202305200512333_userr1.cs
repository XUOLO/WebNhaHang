namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userr1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_User", "FirstName", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Tb_User", "LastName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Tb_User", "FullName");
            DropColumn("dbo.Tb_User", "Phone");
            DropColumn("dbo.Tb_User", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_User", "Address", c => c.String());
            AddColumn("dbo.Tb_User", "Phone", c => c.String());
            AddColumn("dbo.Tb_User", "FullName", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Tb_User", "LastName");
            DropColumn("dbo.Tb_User", "FirstName");
        }
    }
}

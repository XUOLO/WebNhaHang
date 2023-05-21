namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userr11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_User", "Address", c => c.String());
            AddColumn("dbo.Tb_User", "Phone", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_User", "Phone");
            DropColumn("dbo.Tb_User", "Address");
        }
    }
}

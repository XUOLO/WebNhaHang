namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class usermailactiva : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ActivationCode", c => c.Guid(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "ActivationCode");
        }
    }
}

namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statusressv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Reservation", "Status");
        }
    }
}

namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phoneress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "Address", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Reservation", "Address");
        }
    }
}

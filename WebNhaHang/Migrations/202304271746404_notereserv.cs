namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notereserv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "Note", c => c.String());
            DropColumn("dbo.Tb_Reservation", "Address");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Reservation", "Address", c => c.String());
            DropColumn("dbo.Tb_Reservation", "Note");
        }
    }
}

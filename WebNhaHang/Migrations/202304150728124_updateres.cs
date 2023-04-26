namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "ArrivalDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "DepartureDateTime", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Reservation", "DepartureDateTime");
            DropColumn("dbo.Tb_Reservation", "ArrivalDateTime");
        }
    }
}

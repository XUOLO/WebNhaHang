namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class a : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tb_Reservation", "ArrivalDateTime");
            DropColumn("dbo.Tb_Reservation", "DepartureDateTime");
            DropColumn("dbo.Tb_Reservation", "CreateBy");
            DropColumn("dbo.Tb_Reservation", "CreateDate");
            DropColumn("dbo.Tb_Reservation", "ModifieDate");
            DropColumn("dbo.Tb_Reservation", "ModifieBy");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Reservation", "ModifieBy", c => c.String());
            AddColumn("dbo.Tb_Reservation", "ModifieDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "CreateBy", c => c.String());
            AddColumn("dbo.Tb_Reservation", "DepartureDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "ArrivalDateTime", c => c.DateTime(nullable: false));
        }
    }
}

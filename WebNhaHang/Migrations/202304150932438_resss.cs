namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class resss : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "DateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tb_Reservation", "NumberOfPeople", c => c.String());
            DropColumn("dbo.Tb_Reservation", "ArrivalDateTime");
            DropColumn("dbo.Tb_Reservation", "DepartureDateTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Reservation", "DepartureDateTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "ArrivalDateTime", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Tb_Reservation", "NumberOfPeople", c => c.Int(nullable: false));
            DropColumn("dbo.Tb_Reservation", "DateTime");
        }
    }
}

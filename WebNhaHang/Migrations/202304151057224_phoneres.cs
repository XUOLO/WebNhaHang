namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class phoneres : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "Phone", c => c.String());
            AddColumn("dbo.Tb_Reservation", "CreateBy", c => c.String());
            AddColumn("dbo.Tb_Reservation", "CreateDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "ModifieDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tb_Reservation", "ModifieBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Reservation", "ModifieBy");
            DropColumn("dbo.Tb_Reservation", "ModifieDate");
            DropColumn("dbo.Tb_Reservation", "CreateDate");
            DropColumn("dbo.Tb_Reservation", "CreateBy");
            DropColumn("dbo.Tb_Reservation", "Phone");
        }
    }
}

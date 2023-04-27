namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class codereserv : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Reservation", "Code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Reservation", "Code");
        }
    }
}

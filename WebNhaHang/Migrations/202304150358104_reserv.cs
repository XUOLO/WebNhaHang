namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class reserv : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Reservation",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(maxLength: 100),
                        ArrivalDateTime = c.DateTime(nullable: false),
                        DepartureDateTime = c.DateTime(nullable: false),
                        Room = c.String(maxLength: 250),
                        NumberOfPeople = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_Reservation");
        }
    }
}

namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class userr111 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Tb_User");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Tb_User",
                c => new
                    {
                        idUser = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Address = c.String(),
                        Phone = c.String(),
                    })
                .PrimaryKey(t => t.idUser);
            
        }
    }
}

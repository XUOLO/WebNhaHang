namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class application2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Aplication",
                c => new
                    {
                        ApplicantID = c.Int(nullable: false, identity: true),
                        FullName = c.String(maxLength: 250),
                        Email = c.String(nullable: false, maxLength: 250),
                        Phone = c.String(),
                        Address = c.String(nullable: false, maxLength: 500),
                        Position = c.String(),
                        Experience = c.String(),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.ApplicantID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tb_Aplication");
        }
    }
}

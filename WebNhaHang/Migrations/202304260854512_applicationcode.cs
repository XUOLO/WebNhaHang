namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class applicationcode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tb_Aplication", "Code", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tb_Aplication", "Code");
        }
    }
}

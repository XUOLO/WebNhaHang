namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteisfeature : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Tb_Productt", "IsFeature");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tb_Productt", "IsFeature", c => c.Boolean(nullable: false));
        }
    }
}

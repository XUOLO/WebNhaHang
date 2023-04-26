namespace WebNhaHang.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tb_Category",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(),
                        Description = c.String(),
                        SeoTitle = c.String(maxLength: 150),
                        SeoDescription = c.String(maxLength: 250),
                        SeoKeywords = c.String(maxLength: 150),
                        Position = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tb_Contact",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 150),
                        Email = c.String(),
                        Website = c.String(),
                        Message = c.String(maxLength: 4000),
                        Isread = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tb_OrderDetail",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        OrderId = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tb_Order", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Tb_Productt", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Tb_Order",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(nullable: false),
                        CustomerName = c.String(nullable: false),
                        Phone = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        Mail = c.String(),
                        TotalAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        TypePayment = c.Int(nullable: false),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tb_Productt",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 250),
                        Alias = c.String(maxLength: 250),
                        ProductCode = c.String(maxLength: 50),
                        ProductCategoryID = c.Int(nullable: false),
                        Description = c.String(),
                        Detail = c.String(),
                        Image = c.String(maxLength: 250),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        PriceSale = c.Decimal(precision: 18, scale: 2),
                        IsHome = c.Boolean(nullable: false),
                        IsSale = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        IsHot = c.Boolean(nullable: false),
                        IsFeature = c.Boolean(nullable: false),
                        Quantity = c.Int(nullable: false),
                        SeoTitle = c.String(maxLength: 250),
                        SeoDescription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tb_ProductCategory", t => t.ProductCategoryID, cascadeDelete: true)
                .Index(t => t.ProductCategoryID);
            
            CreateTable(
                "dbo.Tb_ProductCategory",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 150),
                        Alias = c.String(nullable: false, maxLength: 150),
                        Description = c.String(),
                        Icon = c.String(maxLength: 250),
                        IsActive = c.Boolean(nullable: false),
                        SeoTitle = c.String(maxLength: 250),
                        SeoDescription = c.String(maxLength: 500),
                        SeoKeywords = c.String(maxLength: 250),
                        CreateBy = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                        ModifieDate = c.DateTime(nullable: false),
                        ModifieBy = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tb_ProductImage",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Image = c.String(),
                        IsDefault = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Tb_Productt", t => t.ProductId, cascadeDelete: true)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Tb_ProductImage", "ProductId", "dbo.Tb_Productt");
            DropForeignKey("dbo.Tb_Productt", "ProductCategoryID", "dbo.Tb_ProductCategory");
            DropForeignKey("dbo.Tb_OrderDetail", "ProductId", "dbo.Tb_Productt");
            DropForeignKey("dbo.Tb_OrderDetail", "OrderId", "dbo.Tb_Order");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Tb_ProductImage", new[] { "ProductId" });
            DropIndex("dbo.Tb_Productt", new[] { "ProductCategoryID" });
            DropIndex("dbo.Tb_OrderDetail", new[] { "ProductId" });
            DropIndex("dbo.Tb_OrderDetail", new[] { "OrderId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Tb_ProductImage");
            DropTable("dbo.Tb_ProductCategory");
            DropTable("dbo.Tb_Productt");
            DropTable("dbo.Tb_Order");
            DropTable("dbo.Tb_OrderDetail");
            DropTable("dbo.Tb_Contact");
            DropTable("dbo.Tb_Category");
        }
    }
}

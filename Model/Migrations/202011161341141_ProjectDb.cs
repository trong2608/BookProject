namespace Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProjectDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Admins",
                c => new
                    {
                        id = c.String(nullable: false, maxLength: 128),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        author_id = c.String(nullable: false, maxLength: 128),
                        author_name = c.String(nullable: false),
                        image = c.String(),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.author_id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        pro_id = c.String(nullable: false, maxLength: 128),
                        pro_name = c.String(nullable: false),
                        pro_image = c.String(),
                        pro_description = c.String(nullable: false),
                        create_date = c.String(),
                        pro_price = c.Double(nullable: false),
                        pro_sale_price = c.Double(nullable: false),
                        cat_id = c.String(maxLength: 128),
                        pub_id = c.String(maxLength: 128),
                        author_id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.pro_id)
                .ForeignKey("dbo.Authors", t => t.author_id)
                .ForeignKey("dbo.Categories", t => t.cat_id)
                .ForeignKey("dbo.Publishers", t => t.pub_id)
                .Index(t => t.cat_id)
                .Index(t => t.pub_id)
                .Index(t => t.author_id);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        cat_id = c.String(nullable: false, maxLength: 128),
                        cat_name = c.String(nullable: false),
                        status = c.Boolean(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.cat_id);
            
            CreateTable(
                "dbo.Oders_Detail",
                c => new
                    {
                        Order_Detail = c.Int(nullable: false, identity: true),
                        Oders_id = c.String(maxLength: 128),
                        pro_id = c.String(maxLength: 128),
                        quantity = c.Int(nullable: false),
                        detail_price = c.Double(nullable: false),
                        detail_sale_price = c.Double(nullable: false),
                        total_price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Order_Detail)
                .ForeignKey("dbo.Oders", t => t.Oders_id)
                .ForeignKey("dbo.Products", t => t.pro_id)
                .Index(t => t.Oders_id)
                .Index(t => t.pro_id);
            
            CreateTable(
                "dbo.Oders",
                c => new
                    {
                        Oders_id = c.String(nullable: false, maxLength: 128),
                        cus_id = c.String(maxLength: 128),
                        total_amount = c.Double(nullable: false),
                        payment_method = c.String(),
                        Description = c.String(),
                        fullname = c.String(),
                        address = c.String(),
                        phone = c.String(),
                        create_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Oders_id)
                .ForeignKey("dbo.Customers", t => t.cus_id)
                .Index(t => t.cus_id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        cus_id = c.String(nullable: false, maxLength: 128),
                        cus_name = c.String(nullable: false),
                        email = c.String(nullable: false),
                        password = c.String(nullable: false),
                        phone = c.String(nullable: false),
                        address = c.String(nullable: false),
                        status = c.Boolean(nullable: false),
                        create_date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.cus_id);
            
            CreateTable(
                "dbo.Publishers",
                c => new
                    {
                        pub_id = c.String(nullable: false, maxLength: 128),
                        pub_name = c.String(nullable: false),
                        Description = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.pub_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "pub_id", "dbo.Publishers");
            DropForeignKey("dbo.Oders_Detail", "pro_id", "dbo.Products");
            DropForeignKey("dbo.Oders_Detail", "Oders_id", "dbo.Oders");
            DropForeignKey("dbo.Oders", "cus_id", "dbo.Customers");
            DropForeignKey("dbo.Products", "cat_id", "dbo.Categories");
            DropForeignKey("dbo.Products", "author_id", "dbo.Authors");
            DropIndex("dbo.Oders", new[] { "cus_id" });
            DropIndex("dbo.Oders_Detail", new[] { "pro_id" });
            DropIndex("dbo.Oders_Detail", new[] { "Oders_id" });
            DropIndex("dbo.Products", new[] { "author_id" });
            DropIndex("dbo.Products", new[] { "pub_id" });
            DropIndex("dbo.Products", new[] { "cat_id" });
            DropTable("dbo.Publishers");
            DropTable("dbo.Customers");
            DropTable("dbo.Oders");
            DropTable("dbo.Oders_Detail");
            DropTable("dbo.Categories");
            DropTable("dbo.Products");
            DropTable("dbo.Authors");
            DropTable("dbo.Admins");
        }
    }
}

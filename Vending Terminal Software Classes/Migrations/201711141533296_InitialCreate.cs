namespace Vending_Terminal_Software_Classes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CurrentStates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Location = c.String(),
                        Money = c.Int(nullable: false),
                        Profit = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Banknotes_n_Coins",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Cost = c.Int(nullable: false),
                        CanBeChange = c.Boolean(nullable: false),
                        Amount = c.Int(nullable: false),
                        CurrentState_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CurrentStates", t => t.CurrentState_ID)
                .Index(t => t.CurrentState_ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(maxLength: 4),
                        Amount = c.Int(nullable: false),
                        CurrentState_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Infoes", t => t.Code)
                .ForeignKey("dbo.CurrentStates", t => t.CurrentState_ID)
                .Index(t => t.Code)
                .Index(t => t.CurrentState_ID);
            
            CreateTable(
                "dbo.Infoes",
                c => new
                    {
                        Code = c.String(nullable: false, maxLength: 4),
                        Name = c.String(maxLength: 100),
                        SellingPrice = c.Int(nullable: false),
                        BuyingPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Sales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Date = c.DateTime(nullable: false),
                        CurrentState_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CurrentStates", t => t.CurrentState_ID)
                .Index(t => t.CurrentState_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sales", "CurrentState_ID", "dbo.CurrentStates");
            DropForeignKey("dbo.Products", "CurrentState_ID", "dbo.CurrentStates");
            DropForeignKey("dbo.Products", "Code", "dbo.Infoes");
            DropForeignKey("dbo.Banknotes_n_Coins", "CurrentState_ID", "dbo.CurrentStates");
            DropIndex("dbo.Sales", new[] { "CurrentState_ID" });
            DropIndex("dbo.Products", new[] { "CurrentState_ID" });
            DropIndex("dbo.Products", new[] { "Code" });
            DropIndex("dbo.Banknotes_n_Coins", new[] { "CurrentState_ID" });
            DropTable("dbo.Sales");
            DropTable("dbo.Infoes");
            DropTable("dbo.Products");
            DropTable("dbo.Banknotes_n_Coins");
            DropTable("dbo.CurrentStates");
        }
    }
}

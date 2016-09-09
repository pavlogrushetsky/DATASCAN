namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedEstimatorsGroups : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("General.Estimators", "CustomerId", "General.Customers");
            DropIndex("General.Estimators", new[] { "CustomerId" });
            CreateTable(
                "dbo.EstimatorsGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerId = c.Int(),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            AddColumn("General.Estimators", "GroupId", c => c.Int());
            AlterColumn("General.Estimators", "CustomerId", c => c.Int());
            CreateIndex("General.Estimators", "CustomerId");
            CreateIndex("General.Estimators", "GroupId");
            AddForeignKey("General.Estimators", "GroupId", "dbo.EstimatorsGroups", "Id");
            AddForeignKey("General.Estimators", "CustomerId", "General.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("General.Estimators", "CustomerId", "General.Customers");
            DropForeignKey("General.Estimators", "GroupId", "dbo.EstimatorsGroups");
            DropForeignKey("dbo.EstimatorsGroups", "CustomerId", "General.Customers");
            DropIndex("dbo.EstimatorsGroups", new[] { "CustomerId" });
            DropIndex("General.Estimators", new[] { "GroupId" });
            DropIndex("General.Estimators", new[] { "CustomerId" });
            AlterColumn("General.Estimators", "CustomerId", c => c.Int(nullable: false));
            DropColumn("General.Estimators", "GroupId");
            DropTable("dbo.EstimatorsGroups");
            CreateIndex("General.Estimators", "CustomerId");
            AddForeignKey("General.Estimators", "CustomerId", "General.Customers", "Id", cascadeDelete: true);
        }
    }
}

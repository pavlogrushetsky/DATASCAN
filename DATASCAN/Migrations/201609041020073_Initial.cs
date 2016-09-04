namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ScanPeriods", "ScanId", "dbo.Scans");
            DropIndex("dbo.ScanPeriods", new[] { "ScanId" });
            CreateTable(
                "dbo.SheduledScans",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scans", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.ScanPeriods", "ScanId", c => c.Int(nullable: false));
            CreateIndex("dbo.ScanPeriods", "ScanId");
            AddForeignKey("dbo.ScanPeriods", "ScanId", "dbo.Scans", "Id", cascadeDelete: true);
            DropColumn("dbo.PeriodicScans", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PeriodicScans", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ScanPeriods", "ScanId", "dbo.Scans");
            DropForeignKey("dbo.SheduledScans", "Id", "dbo.Scans");
            DropIndex("dbo.SheduledScans", new[] { "Id" });
            DropIndex("dbo.ScanPeriods", new[] { "ScanId" });
            AlterColumn("dbo.ScanPeriods", "ScanId", c => c.Int());
            DropTable("dbo.SheduledScans");
            CreateIndex("dbo.ScanPeriods", "ScanId");
            AddForeignKey("dbo.ScanPeriods", "ScanId", "dbo.Scans", "Id");
        }
    }
}

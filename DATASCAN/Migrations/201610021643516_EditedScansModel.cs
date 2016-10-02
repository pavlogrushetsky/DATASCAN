namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditedScansModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Scan.PeriodicScans", "ScanPeriodId", "Scan.Periods");
            DropForeignKey("Scan.Periods", "ScanId", "Scan.Scans");
            DropIndex("Scan.Periods", new[] { "ScanId" });
            DropIndex("Scan.Periods", new[] { "ScheduledScan_Id" });
            DropIndex("Scan.PeriodicScans", new[] { "ScanPeriodId" });
            DropColumn("Scan.Periods", "ScanId");
            RenameColumn(table: "Scan.Periods", name: "ScheduledScan_Id", newName: "ScanId");
            AddColumn("Scan.PeriodicScans", "Period", c => c.Int(nullable: false));
            AddColumn("Scan.PeriodicScans", "PeriodType", c => c.Boolean(nullable: false));
            AddColumn("Scan.PeriodicScans", "DateLastScanned", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AlterColumn("Scan.Periods", "ScanId", c => c.Int(nullable: false));
            CreateIndex("Scan.Periods", "ScanId");
            AddForeignKey("Scan.Periods", "ScanId", "Scan.SheduledScans", "Id");
            DropColumn("Scan.PeriodicScans", "ScanPeriodId");
        }
        
        public override void Down()
        {
            AddColumn("Scan.PeriodicScans", "ScanPeriodId", c => c.Int(nullable: false));
            DropForeignKey("Scan.Periods", "ScanId", "Scan.SheduledScans");
            DropIndex("Scan.Periods", new[] { "ScanId" });
            AlterColumn("Scan.Periods", "ScanId", c => c.Int());
            DropColumn("Scan.PeriodicScans", "DateLastScanned");
            DropColumn("Scan.PeriodicScans", "PeriodType");
            DropColumn("Scan.PeriodicScans", "Period");
            RenameColumn(table: "Scan.Periods", name: "ScanId", newName: "ScheduledScan_Id");
            AddColumn("Scan.Periods", "ScanId", c => c.Int(nullable: false));
            CreateIndex("Scan.PeriodicScans", "ScanPeriodId");
            CreateIndex("Scan.Periods", "ScheduledScan_Id");
            CreateIndex("Scan.Periods", "ScanId");
            AddForeignKey("Scan.Periods", "ScanId", "Scan.Scans", "Id", cascadeDelete: true);
            AddForeignKey("Scan.PeriodicScans", "ScanPeriodId", "Scan.Periods", "Id", cascadeDelete: true);
        }
    }
}

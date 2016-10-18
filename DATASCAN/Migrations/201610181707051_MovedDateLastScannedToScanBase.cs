namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovedDateLastScannedToScanBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("Scan.Scans", "DateLastScanned", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("Scan.PeriodicScans", "DateLastScanned");
        }
        
        public override void Down()
        {
            AddColumn("Scan.PeriodicScans", "DateLastScanned", c => c.DateTime(precision: 7, storeType: "datetime2"));
            DropColumn("Scan.Scans", "DateLastScanned");
        }
    }
}

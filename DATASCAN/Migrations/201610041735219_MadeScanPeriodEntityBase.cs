namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MadeScanPeriodEntityBase : DbMigration
    {
        public override void Up()
        {
            AddColumn("Scan.Periods", "DateCreated", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("Scan.Periods", "DateModified", c => c.DateTime(nullable: false, precision: 7, storeType: "datetime2"));
            AddColumn("Scan.Periods", "IsActive", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("Scan.Periods", "IsActive");
            DropColumn("Scan.Periods", "DateModified");
            DropColumn("Scan.Periods", "DateCreated");
        }
    }
}

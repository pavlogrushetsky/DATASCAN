namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScanViaGPRSToEstimators : DbMigration
    {
        public override void Up()
        {
            AddColumn("General.Estimators", "IsScannedViaGPRS", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("General.Estimators", "IsScannedViaGPRS");
        }
    }
}

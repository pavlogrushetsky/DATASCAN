namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredForRocAddress : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Roc809.Estimators", "Address", c => c.String(maxLength: 15));
        }
        
        public override void Down()
        {
            AlterColumn("Roc809.Estimators", "Address", c => c.String(nullable: false, maxLength: 15));
        }
    }
}

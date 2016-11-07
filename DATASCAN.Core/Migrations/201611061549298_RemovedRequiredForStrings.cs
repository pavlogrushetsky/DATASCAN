namespace DATASCAN.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredForStrings : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Roc809.AlarmData", "Value", c => c.String(maxLength: 20));
            AlterColumn("Roc809.AlarmData", "Description", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("Roc809.AlarmData", "Description", c => c.String(nullable: false, maxLength: 20));
            AlterColumn("Roc809.AlarmData", "Value", c => c.String(nullable: false, maxLength: 20));
        }
    }
}

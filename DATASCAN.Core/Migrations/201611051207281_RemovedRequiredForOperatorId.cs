namespace DATASCAN.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedRequiredForOperatorId : DbMigration
    {
        public override void Up()
        {
            AlterColumn("Roc809.EventData", "OperatorId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("Roc809.EventData", "OperatorId", c => c.String(nullable: false));
        }
    }
}

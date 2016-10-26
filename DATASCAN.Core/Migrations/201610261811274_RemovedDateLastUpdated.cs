namespace DATASCAN.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedDateLastUpdated : DbMigration
    {
        public override void Up()
        {
            DropColumn("Roc809.Estimators", "DateAlarmDataLastUpdated");
            DropColumn("Roc809.Estimators", "DateEventDataLastUpdated");
            DropColumn("Floutec.MeasureLines", "DateHourlyDataLastUpdated");
            DropColumn("Floutec.MeasureLines", "DateInstantDataLastUpdated");
            DropColumn("Floutec.MeasureLines", "DateIdentDataLastUpdated");
            DropColumn("Floutec.MeasureLines", "DateInterDataLastUpdated");
            DropColumn("Floutec.MeasureLines", "DateAlarmDataLastUpdated");
            DropColumn("Roc809.MeasurePoints", "DateMinuteDataLastUpdated");
            DropColumn("Roc809.MeasurePoints", "DatePeriodicDataLastUpdated");
            DropColumn("Roc809.MeasurePoints", "DateDailyDataLastUpdated");
        }
        
        public override void Down()
        {
            AddColumn("Roc809.MeasurePoints", "DateDailyDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Roc809.MeasurePoints", "DatePeriodicDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Roc809.MeasurePoints", "DateMinuteDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Floutec.MeasureLines", "DateAlarmDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Floutec.MeasureLines", "DateInterDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Floutec.MeasureLines", "DateIdentDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Floutec.MeasureLines", "DateInstantDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Floutec.MeasureLines", "DateHourlyDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Roc809.Estimators", "DateEventDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
            AddColumn("Roc809.Estimators", "DateAlarmDataLastUpdated", c => c.DateTime(precision: 7, storeType: "datetime2"));
        }
    }
}

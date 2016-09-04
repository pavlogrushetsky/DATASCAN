namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSchema : DbMigration
    {
        public override void Up()
        {
            MoveTable(name: "dbo.Customers", newSchema: "General");
            MoveTable(name: "dbo.Estimators", newSchema: "General");
            MoveTable(name: "dbo.Floutecs", newSchema: "Floutec");
            MoveTable(name: "dbo.Roc809s", newSchema: "Roc");
            MoveTable(name: "dbo.MeasurePoints", newSchema: "General");
            MoveTable(name: "dbo.FloutecMeasureLines", newSchema: "Floutec");
            MoveTable(name: "dbo.Roc809MeasurePoints", newSchema: "Roc");
            MoveTable(name: "dbo.ScanMembers", newSchema: "Scan");
            MoveTable(name: "dbo.Scans", newSchema: "Scan");
            MoveTable(name: "dbo.PeriodicScans", newSchema: "Scan");
            MoveTable(name: "dbo.SheduledScans", newSchema: "Scan");
            MoveTable(name: "dbo.ScanPeriods", newSchema: "Scan");
            MoveTable(name: "dbo.FloutecAlarmData", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecHourlyData", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecIdentData", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecInstantData", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecInterData", newSchema: "Floutec");
            MoveTable(name: "dbo.Roc809DailyData", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809MinuteData", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809PeriodicData", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809AlarmData", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809EventData", newSchema: "Roc");
            MoveTable(name: "dbo.FloutecAlarmsTypes", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecIntersTypes", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecParamsTypes", newSchema: "Floutec");
            MoveTable(name: "dbo.FloutecSensorsTypes", newSchema: "Floutec");
            MoveTable(name: "dbo.Roc809AlarmCodes", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809AlarmTypes", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809EventCodes", newSchema: "Roc");
            MoveTable(name: "dbo.Roc809EventTypes", newSchema: "Roc");
        }
        
        public override void Down()
        {
            MoveTable(name: "Roc.Roc809EventTypes", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809EventCodes", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809AlarmTypes", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809AlarmCodes", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecSensorsTypes", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecParamsTypes", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecIntersTypes", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecAlarmsTypes", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809EventData", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809AlarmData", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809PeriodicData", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809MinuteData", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809DailyData", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecInterData", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecInstantData", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecIdentData", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecHourlyData", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecAlarmData", newSchema: "dbo");
            MoveTable(name: "Scan.ScanPeriods", newSchema: "dbo");
            MoveTable(name: "Scan.SheduledScans", newSchema: "dbo");
            MoveTable(name: "Scan.PeriodicScans", newSchema: "dbo");
            MoveTable(name: "Scan.Scans", newSchema: "dbo");
            MoveTable(name: "Scan.ScanMembers", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809MeasurePoints", newSchema: "dbo");
            MoveTable(name: "Floutec.FloutecMeasureLines", newSchema: "dbo");
            MoveTable(name: "General.MeasurePoints", newSchema: "dbo");
            MoveTable(name: "Roc.Roc809s", newSchema: "dbo");
            MoveTable(name: "Floutec.Floutecs", newSchema: "dbo");
            MoveTable(name: "General.Estimators", newSchema: "dbo");
            MoveTable(name: "General.Customers", newSchema: "dbo");
        }
    }
}

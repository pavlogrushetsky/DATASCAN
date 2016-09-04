namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedNames : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "Floutec.Floutecs", newName: "Estimators");
            RenameTable(name: "Roc.Roc809s", newName: "Estimators");
            RenameTable(name: "Floutec.FloutecMeasureLines", newName: "MeasureLines");
            RenameTable(name: "Roc.Roc809MeasurePoints", newName: "MeasurePoints");
            RenameTable(name: "Scan.ScanMembers", newName: "Members");
            RenameTable(name: "Scan.ScanPeriods", newName: "Periods");
            RenameTable(name: "Floutec.FloutecAlarmData", newName: "AlarmData");
            RenameTable(name: "Floutec.FloutecHourlyData", newName: "HourlyData");
            RenameTable(name: "Floutec.FloutecIdentData", newName: "IdentData");
            RenameTable(name: "Floutec.FloutecInstantData", newName: "InstantData");
            RenameTable(name: "Floutec.FloutecInterData", newName: "InterData");
            RenameTable(name: "Roc.Roc809DailyData", newName: "DailyData");
            RenameTable(name: "Roc.Roc809MinuteData", newName: "MinuteData");
            RenameTable(name: "Roc.Roc809PeriodicData", newName: "PeriodicData");
            RenameTable(name: "Roc.Roc809AlarmData", newName: "AlarmData");
            RenameTable(name: "Roc.Roc809EventData", newName: "EventData");
            RenameTable(name: "Floutec.FloutecAlarmsTypes", newName: "AlarmTypes");
            RenameTable(name: "Floutec.FloutecIntersTypes", newName: "InterTypes");
            RenameTable(name: "Floutec.FloutecParamsTypes", newName: "ParamTypes");
            RenameTable(name: "Floutec.FloutecSensorsTypes", newName: "SensorTypes");
            RenameTable(name: "Roc.Roc809AlarmCodes", newName: "AlarmCodes");
            RenameTable(name: "Roc.Roc809AlarmTypes", newName: "AlarmTypes");
            RenameTable(name: "Roc.Roc809EventCodes", newName: "EventCodes");
            RenameTable(name: "Roc.Roc809EventTypes", newName: "EventTypes");
            MoveTable(name: "Roc.Estimators", newSchema: "Roc809");
            MoveTable(name: "Roc.MeasurePoints", newSchema: "Roc809");
            MoveTable(name: "Roc.DailyData", newSchema: "Roc809");
            MoveTable(name: "Roc.MinuteData", newSchema: "Roc809");
            MoveTable(name: "Roc.PeriodicData", newSchema: "Roc809");
            MoveTable(name: "Roc.AlarmData", newSchema: "Roc809");
            MoveTable(name: "Roc.EventData", newSchema: "Roc809");
            MoveTable(name: "Roc.AlarmCodes", newSchema: "Roc809");
            MoveTable(name: "Roc.AlarmTypes", newSchema: "Roc809");
            MoveTable(name: "Roc.EventCodes", newSchema: "Roc809");
            MoveTable(name: "Roc.EventTypes", newSchema: "Roc809");
        }
        
        public override void Down()
        {
            MoveTable(name: "Roc809.EventTypes", newSchema: "Roc");
            MoveTable(name: "Roc809.EventCodes", newSchema: "Roc");
            MoveTable(name: "Roc809.AlarmTypes", newSchema: "Roc");
            MoveTable(name: "Roc809.AlarmCodes", newSchema: "Roc");
            MoveTable(name: "Roc809.EventData", newSchema: "Roc");
            MoveTable(name: "Roc809.AlarmData", newSchema: "Roc");
            MoveTable(name: "Roc809.PeriodicData", newSchema: "Roc");
            MoveTable(name: "Roc809.MinuteData", newSchema: "Roc");
            MoveTable(name: "Roc809.DailyData", newSchema: "Roc");
            MoveTable(name: "Roc809.MeasurePoints", newSchema: "Roc");
            MoveTable(name: "Roc809.Estimators", newSchema: "Roc");
            RenameTable(name: "Roc.EventTypes", newName: "Roc809EventTypes");
            RenameTable(name: "Roc.EventCodes", newName: "Roc809EventCodes");
            RenameTable(name: "Roc.AlarmTypes", newName: "Roc809AlarmTypes");
            RenameTable(name: "Roc.AlarmCodes", newName: "Roc809AlarmCodes");
            RenameTable(name: "Floutec.SensorTypes", newName: "FloutecSensorsTypes");
            RenameTable(name: "Floutec.ParamTypes", newName: "FloutecParamsTypes");
            RenameTable(name: "Floutec.InterTypes", newName: "FloutecIntersTypes");
            RenameTable(name: "Floutec.AlarmTypes", newName: "FloutecAlarmsTypes");
            RenameTable(name: "Roc.EventData", newName: "Roc809EventData");
            RenameTable(name: "Roc.AlarmData", newName: "Roc809AlarmData");
            RenameTable(name: "Roc.PeriodicData", newName: "Roc809PeriodicData");
            RenameTable(name: "Roc.MinuteData", newName: "Roc809MinuteData");
            RenameTable(name: "Roc.DailyData", newName: "Roc809DailyData");
            RenameTable(name: "Floutec.InterData", newName: "FloutecInterData");
            RenameTable(name: "Floutec.InstantData", newName: "FloutecInstantData");
            RenameTable(name: "Floutec.IdentData", newName: "FloutecIdentData");
            RenameTable(name: "Floutec.HourlyData", newName: "FloutecHourlyData");
            RenameTable(name: "Floutec.AlarmData", newName: "FloutecAlarmData");
            RenameTable(name: "Scan.Periods", newName: "ScanPeriods");
            RenameTable(name: "Scan.Members", newName: "ScanMembers");
            RenameTable(name: "Roc.MeasurePoints", newName: "Roc809MeasurePoints");
            RenameTable(name: "Floutec.MeasureLines", newName: "FloutecMeasureLines");
            RenameTable(name: "Roc.Estimators", newName: "Roc809s");
            RenameTable(name: "Floutec.Estimators", newName: "Floutecs");
        }
    }
}

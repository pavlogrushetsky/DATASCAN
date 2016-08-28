using System.Data.Entity;
using DATASCAN.Model;
using DATASCAN.Model.Floutecs;
using DATASCAN.Model.Floutecs.Catalogs;
using DATASCAN.Model.Rocs;
using DATASCAN.Model.Rocs.Catalogs;
using DATASCAN.Model.Scanning;

namespace DATASCAN.Context
{
    public class DataContext : DbContext
    {
        public DataContext() : base("DATASCAN")
        {
            
        }

        public DbSet<Customer> Customers { get; set; }
        
        public DbSet<EstimatorBase> Estimators { get; set; }
        
        public DbSet<MeasurePointBase> MeasurePoints { get; set; }  
        
        public DbSet<Floutec> Floutecs { get; set; }
        
        public DbSet<Roc809> Roc809s { get; set; }
        
        public DbSet<FloutecMeasureLine> FloutecMeasureLines { get; set; }
        
        public DbSet<Roc809MeasurePoint> Roc809MeasurePoints { get; set; }
        
        public DbSet<FloutecHourlyData> FloutecHourlyData { get; set; }
        
        public DbSet<FloutecInstantData> FloutecInstantData { get; set; }
        
        public DbSet<FloutecIdentData> FloutecIdentData { get; set; }
        
        public DbSet<FloutecInterData> FloutecInterData { get; set; }
        
        public DbSet<FloutecAlarmData> FloutecAlarmData { get; set; }   
        
        public DbSet<Roc809MinuteData> Roc809MinuteData { get; set; }
        
        public DbSet<Roc809PeriodicData> Roc809PeriodicData { get; set; }
        
        public DbSet<Roc809DailyData> Roc809DailyData { get; set; }
        
        public DbSet<Roc809EventData> Roc809EventData { get; set; }
        
        public DbSet<Roc809AlarmData> Roc809AlarmData { get; set; }  

        public DbSet<FloutecSensorsTypes> FloutecSensorsTypes { get; set; }
        
        public DbSet<FloutecParamsTypes> FloutecParamsTypes { get; set; }  

        public DbSet<FloutecAlarmsTypes> FloutecAlarmsTypes { get; set; } 

        public DbSet<FloutecIntersTypes> FloutecIntersTypes { get; set; } 

        public DbSet<Roc809EventTypes> Roc809EventTypes { get; set; }
        
        public DbSet<Roc809EventCodes> Roc809EventCodes { get; set; }
        
        public DbSet<Roc809AlarmTypes> Roc809AlarmTypes { get; set; }
        
        public DbSet<Roc809AlarmCodes> Roc809AlarmCodes { get; set; }   
        
        public DbSet<ScanBase> Scans { get; set; }
        
        public DbSet<PeriodicScan> PeriodicScans { get; set; }
        
        public DbSet<ScheduledScan> ScheduledScans { get; set; }
        
        public DbSet<ScanMember> ScanMembers { get; set; }     
    }
}

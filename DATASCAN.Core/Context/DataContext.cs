using System.Data.Common;
using System.Data.Entity;
using DATASCAN.Core.Migrations;
using DATASCAN.Core.Model;
using DATASCAN.Core.Model.Floutecs;
using DATASCAN.Core.Model.Floutecs.Catalogs;
using DATASCAN.Core.Model.Rocs;
using DATASCAN.Core.Model.Rocs.Catalogs;
using DATASCAN.Core.Model.Scanning;

namespace DATASCAN.Core.Context
{
    /// <summary>
    /// Контекст данных
    /// </summary>
    public class DataContext : DbContext
    {
        /// <summary>
        /// Инициализирует контекст данных на основе строки подключения в App.config
        /// </summary>
        public DataContext() : base("DATASCAN")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>("DATASCAN"));
        }

        /// <summary>
        /// Инициализирует контекст данных на основе указанного соединения
        /// </summary>
        /// <param name="connection"><see cref="DbConnection"/>Соединение</param>
        /// <param name="initialize"><see cref="DbConnection"/>Инициализировать базу данных</param>
        public DataContext(DbConnection connection, bool initialize) : base(connection, true)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DataContext, Configuration>(true));
            Database.Initialize(initialize);
        }

        /// <summary>
        /// Заказчики
        /// </summary>
        public DbSet<Customer> Customers { get; set; }
        
        /// <summary>
        /// Вычислители
        /// </summary>
        public DbSet<EstimatorBase> Estimators { get; set; }

        /// <summary>
        /// Группы вычислителей
        /// </summary>
        public DbSet<EstimatorsGroup> Groups { get; set; } 
        
        /// <summary>
        /// Точки измерения
        /// </summary>
        public DbSet<MeasurePointBase> MeasurePoints { get; set; }  
        
        /// <summary>
        /// Вычислители ФЛОУТЭК
        /// </summary>
        public DbSet<Floutec> Floutecs { get; set; }
        
        /// <summary>
        /// Вычислители ROC809
        /// </summary>
        public DbSet<Roc809> Roc809s { get; set; }
        
        /// <summary>
        /// Нитки измерения вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecMeasureLine> FloutecMeasureLines { get; set; }

        /// <summary>
        /// Точки измерения вычислителей ROC809
        /// </summary>
        public DbSet<Roc809MeasurePoint> Roc809MeasurePoints { get; set; }
        
        /// <summary>
        /// Часовые данные вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecHourlyData> FloutecHourlyData { get; set; }
        
        /// <summary>
        /// Мгновенные данные вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecInstantData> FloutecInstantData { get; set; }
        
        /// <summary>
        /// Данные идентификации вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecIdentData> FloutecIdentData { get; set; }
        
        /// <summary>
        /// Данные вмешательств вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecInterData> FloutecInterData { get; set; }
        
        /// <summary>
        /// Данные аварий вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecAlarmData> FloutecAlarmData { get; set; }

        /// <summary>
        /// Минутные данные вычислителей ROC809
        /// </summary>
        public DbSet<Roc809MinuteData> Roc809MinuteData { get; set; }

        /// <summary>
        /// Периодические данные вычислителей ROC809
        /// </summary>
        public DbSet<Roc809PeriodicData> Roc809PeriodicData { get; set; }

        /// <summary>
        /// Суточные данные вычислителей ROC809
        /// </summary>
        public DbSet<Roc809DailyData> Roc809DailyData { get; set; }

        /// <summary>
        /// Данные событий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809EventData> Roc809EventData { get; set; }

        /// <summary>
        /// Данные аварий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809AlarmData> Roc809AlarmData { get; set; }  

        /// <summary>
        /// Справочные данные типов датчиков вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecSensorTypes> FloutecSensorTypes { get; set; }

        /// <summary>
        /// Справочные данные типов параметров вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecParamTypes> FloutecParamTypes { get; set; }  

        /// <summary>
        /// Справочные данные типов аварий вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecAlarmTypes> FloutecAlarmTypes { get; set; } 

        /// <summary>
        /// Справочные данные типов вмешательств вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecInterTypes> FloutecInterTypes { get; set; }

        /// <summary>
        /// Справочные данные типов событий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809EventTypes> Roc809EventTypes { get; set; }

        /// <summary>
        /// Справочные данные дополнительных кодов событий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809EventCodes> Roc809EventCodes { get; set; }

        /// <summary>
        /// Справочные данные типов аварий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809AlarmTypes> Roc809AlarmTypes { get; set; }

        /// <summary>
        /// Справочные данные дополнительных кодов аварий вычислителей ROC809
        /// </summary>
        public DbSet<Roc809AlarmCodes> Roc809AlarmCodes { get; set; }   
        
        /// <summary>
        /// Опросы данных
        /// </summary>
        public DbSet<ScanBase> Scans { get; set; }
        
        /// <summary>
        /// Периодические опросы данных
        /// </summary>
        public DbSet<PeriodicScan> PeriodicScans { get; set; }
        
        /// <summary>
        /// Опросы данных по расписанию
        /// </summary>
        public DbSet<ScheduledScan> ScheduledScans { get; set; }
        
        /// <summary>
        /// Элементы опросов данных
        /// </summary>
        public DbSet<ScanMemberBase> ScanMembers { get; set; }

        /// <summary>
        /// Элементы опросов данных вычислителей ROC809
        /// </summary>
        public DbSet<RocScanMember> RocScanMembers { get; set; }

        /// <summary>
        /// Элементы опросов данных вычислителей ФЛОУТЭК
        /// </summary>
        public DbSet<FloutecScanMember> FloutecScanMembers { get; set; }

        /// <summary>
        /// Периоды опросов данных
        /// </summary>
        public DbSet<ScanPeriod> ScanPeriods { get; set; }
    }
}

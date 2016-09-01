namespace DATASCAN.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        Person = c.String(maxLength: 100),
                        Phone = c.String(maxLength: 13),
                        Email = c.String(maxLength: 200),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Estimators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 13),
                        CustomerId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.MeasurePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        EstimatorId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estimators", t => t.EstimatorId, cascadeDelete: true)
                .Index(t => t.EstimatorId);
            
            CreateTable(
                "dbo.ScanMembers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstimatorId = c.Int(nullable: false),
                        MeasurePointId = c.Int(),
                        DataType = c.String(nullable: false, maxLength: 50),
                        ScanBaseId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estimators", t => t.EstimatorId, cascadeDelete: true)
                .ForeignKey("dbo.MeasurePoints", t => t.MeasurePointId)
                .ForeignKey("dbo.Scans", t => t.ScanBaseId, cascadeDelete: true)
                .Index(t => t.EstimatorId)
                .Index(t => t.MeasurePointId)
                .Index(t => t.ScanBaseId);
            
            CreateTable(
                "dbo.Scans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScanPeriods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Period = c.Time(nullable: false, precision: 7),
                        ScanId = c.Int(nullable: false),
                        ScheduledScan_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scans", t => t.ScanId, cascadeDelete: true)
                .ForeignKey("dbo.PeriodicScans", t => t.ScheduledScan_Id)
                .Index(t => t.ScanId)
                .Index(t => t.ScheduledScan_Id);
            
            CreateTable(
                "dbo.FloutecAlarmData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DAT = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        T_AVAR = c.Int(nullable: false),
                        T_PARAM = c.Int(nullable: false),
                        VAL = c.Double(nullable: false),
                        FloutecMeasureLineId = c.Int(nullable: false),
                        N_FLONIT = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloutecMeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "dbo.FloutecHourlyData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DAT = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DAT_END = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        RASX = c.Double(nullable: false),
                        DAVL = c.Double(nullable: false),
                        PD = c.String(maxLength: 1),
                        TEMP = c.Double(nullable: false),
                        PT = c.String(maxLength: 1),
                        PEREP = c.Double(nullable: false),
                        PP = c.String(maxLength: 1),
                        PLOTN = c.Double(nullable: false),
                        PL = c.String(maxLength: 1),
                        FloutecMeasureLineId = c.Int(nullable: false),
                        N_FLONIT = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloutecMeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "dbo.FloutecIdentData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        KONTRH = c.Time(nullable: false, precision: 7),
                        SCHET = c.Int(nullable: false),
                        NO2 = c.Double(nullable: false),
                        OTBOR = c.Int(nullable: false),
                        ACP = c.Double(nullable: false),
                        ACS = c.Double(nullable: false),
                        SHER = c.Double(nullable: false),
                        VERXP = c.Double(nullable: false),
                        PLOTN = c.Double(nullable: false),
                        DTRUB = c.Double(nullable: false),
                        OTSECH = c.Double(nullable: false),
                        BCP = c.Double(nullable: false),
                        BCS = c.Double(nullable: false),
                        VERXDP = c.Double(nullable: false),
                        NIZT = c.Double(nullable: false),
                        CO2 = c.Double(nullable: false),
                        DSU = c.Double(nullable: false),
                        CCP = c.Double(nullable: false),
                        CCS = c.Double(nullable: false),
                        NIZP = c.Double(nullable: false),
                        VERXT = c.Double(nullable: false),
                        KONDENS = c.Int(nullable: false),
                        KALIBSCH = c.Double(nullable: false),
                        MINRSCH = c.Double(nullable: false),
                        MAXRSCH = c.Double(nullable: false),
                        TYPDAN = c.Double(nullable: false),
                        FloutecMeasureLineId = c.Int(nullable: false),
                        N_FLONIT = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloutecMeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "dbo.FloutecInstantData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DAT = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        QHOUR = c.Double(nullable: false),
                        PQHOUR = c.Double(nullable: false),
                        CURRSPEND = c.Double(nullable: false),
                        DAYSPEND = c.Double(nullable: false),
                        YESTSPEND = c.Double(nullable: false),
                        MONTHSPEND = c.Double(nullable: false),
                        LASTMONTHSPEND = c.Double(nullable: false),
                        ALLSPEND = c.Double(nullable: false),
                        ALARMSY = c.Double(nullable: false),
                        ALARMRY = c.Double(nullable: false),
                        PALARMSY = c.Double(nullable: false),
                        PALARMRY = c.Double(nullable: false),
                        PERPRES = c.Double(nullable: false),
                        PP = c.String(maxLength: 1),
                        STPRES = c.Double(nullable: false),
                        PD = c.String(maxLength: 1),
                        ABSP = c.Double(nullable: false),
                        TEMP = c.Double(nullable: false),
                        PT = c.String(maxLength: 1),
                        GASVIZ = c.Double(nullable: false),
                        SQROOT = c.Double(nullable: false),
                        GAZPLOTNRU = c.Double(nullable: false),
                        GAZPLOTNNU = c.Double(nullable: false),
                        DLITAS = c.Int(nullable: false),
                        DLITBAS = c.Int(nullable: false),
                        DLITMAS = c.Int(nullable: false),
                        PDLITAS = c.Int(nullable: false),
                        PDLITBAS = c.Int(nullable: false),
                        PDLITMAS = c.Int(nullable: false),
                        FloutecMeasureLineId = c.Int(nullable: false),
                        N_FLONIT = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloutecMeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "dbo.FloutecInterData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DAT = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        CH_PAR = c.Int(nullable: false),
                        T_PARAM = c.Int(),
                        VAL_OLD = c.String(maxLength: 20),
                        VAL_NEW = c.String(maxLength: 20),
                        FloutecMeasureLineId = c.Int(nullable: false),
                        N_FLONIT = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FloutecMeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "dbo.Roc809DailyData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roc809MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "dbo.Roc809MinuteData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roc809MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "dbo.Roc809PeriodicData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roc809MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "dbo.Roc809AlarmData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        SRBX = c.Boolean(nullable: false),
                        Condition = c.Boolean(nullable: false),
                        T = c.Int(),
                        L = c.Int(),
                        P = c.Int(),
                        Value = c.String(nullable: false, maxLength: 10),
                        Description = c.String(nullable: false, maxLength: 10),
                        Code = c.Int(),
                        FST = c.Int(),
                        Roc809Id = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roc809s", t => t.Roc809Id)
                .Index(t => t.Roc809Id);
            
            CreateTable(
                "dbo.Roc809EventData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Time = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        OperatorId = c.String(nullable: false),
                        T = c.Int(),
                        L = c.Int(),
                        P = c.Int(),
                        Value = c.String(maxLength: 20),
                        NewValue = c.String(maxLength: 20),
                        OldValue = c.String(maxLength: 20),
                        RawValue = c.String(maxLength: 20),
                        CalibratedValue = c.String(maxLength: 20),
                        Description = c.String(maxLength: 20),
                        Code = c.Int(),
                        FST = c.Int(),
                        Roc809Id = c.Int(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roc809s", t => t.Roc809Id)
                .Index(t => t.Roc809Id);
            
            CreateTable(
                "dbo.FloutecAlarmsTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description_45 = c.String(nullable: false, maxLength: 400),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FloutecIntersTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description_45 = c.String(nullable: false, maxLength: 400),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FloutecParamsTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Param = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FloutecSensorsTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Roc809AlarmCodes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Roc809AlarmTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Roc809EventCodes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.Roc809EventTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "dbo.FloutecMeasureLines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        SensorType = c.Int(nullable: false),
                        DateHourlyDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateInstantDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateIdentDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateInterDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateAlarmDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeasurePoints", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Floutecs",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estimators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.PeriodicScans",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ScanPeriodId = c.Int(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scans", t => t.Id)
                .ForeignKey("dbo.ScanPeriods", t => t.ScanPeriodId, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.ScanPeriodId);
            
            CreateTable(
                "dbo.Roc809MeasurePoints",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Number = c.Int(nullable: false),
                        HistSegment = c.Int(nullable: false),
                        DateMinuteDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DatePeriodicDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateDailyDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MeasurePoints", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Roc809s",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(nullable: false, maxLength: 15),
                        Port = c.Int(nullable: false),
                        RocUnit = c.Int(nullable: false),
                        RocGroup = c.Int(nullable: false),
                        HostUnit = c.Int(nullable: false),
                        HostGroup = c.Int(nullable: false),
                        DateAlarmDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateEventDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Estimators", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Roc809s", "Id", "dbo.Estimators");
            DropForeignKey("dbo.Roc809MeasurePoints", "Id", "dbo.MeasurePoints");
            DropForeignKey("dbo.PeriodicScans", "ScanPeriodId", "dbo.ScanPeriods");
            DropForeignKey("dbo.PeriodicScans", "Id", "dbo.Scans");
            DropForeignKey("dbo.Floutecs", "Id", "dbo.Estimators");
            DropForeignKey("dbo.FloutecMeasureLines", "Id", "dbo.MeasurePoints");
            DropForeignKey("dbo.Roc809EventData", "Roc809Id", "dbo.Roc809s");
            DropForeignKey("dbo.Roc809AlarmData", "Roc809Id", "dbo.Roc809s");
            DropForeignKey("dbo.Roc809PeriodicData", "Roc809MeasurePointId", "dbo.Roc809MeasurePoints");
            DropForeignKey("dbo.Roc809MinuteData", "Roc809MeasurePointId", "dbo.Roc809MeasurePoints");
            DropForeignKey("dbo.Roc809DailyData", "Roc809MeasurePointId", "dbo.Roc809MeasurePoints");
            DropForeignKey("dbo.FloutecInterData", "FloutecMeasureLineId", "dbo.FloutecMeasureLines");
            DropForeignKey("dbo.FloutecInstantData", "FloutecMeasureLineId", "dbo.FloutecMeasureLines");
            DropForeignKey("dbo.FloutecIdentData", "FloutecMeasureLineId", "dbo.FloutecMeasureLines");
            DropForeignKey("dbo.FloutecHourlyData", "FloutecMeasureLineId", "dbo.FloutecMeasureLines");
            DropForeignKey("dbo.FloutecAlarmData", "FloutecMeasureLineId", "dbo.FloutecMeasureLines");
            DropForeignKey("dbo.ScanPeriods", "ScheduledScan_Id", "dbo.PeriodicScans");
            DropForeignKey("dbo.ScanPeriods", "ScanId", "dbo.Scans");
            DropForeignKey("dbo.ScanMembers", "ScanBaseId", "dbo.Scans");
            DropForeignKey("dbo.ScanMembers", "MeasurePointId", "dbo.MeasurePoints");
            DropForeignKey("dbo.ScanMembers", "EstimatorId", "dbo.Estimators");
            DropForeignKey("dbo.MeasurePoints", "EstimatorId", "dbo.Estimators");
            DropForeignKey("dbo.Estimators", "CustomerId", "dbo.Customers");
            DropIndex("dbo.Roc809s", new[] { "Id" });
            DropIndex("dbo.Roc809MeasurePoints", new[] { "Id" });
            DropIndex("dbo.PeriodicScans", new[] { "ScanPeriodId" });
            DropIndex("dbo.PeriodicScans", new[] { "Id" });
            DropIndex("dbo.Floutecs", new[] { "Id" });
            DropIndex("dbo.FloutecMeasureLines", new[] { "Id" });
            DropIndex("dbo.Roc809EventData", new[] { "Roc809Id" });
            DropIndex("dbo.Roc809AlarmData", new[] { "Roc809Id" });
            DropIndex("dbo.Roc809PeriodicData", new[] { "Roc809MeasurePointId" });
            DropIndex("dbo.Roc809MinuteData", new[] { "Roc809MeasurePointId" });
            DropIndex("dbo.Roc809DailyData", new[] { "Roc809MeasurePointId" });
            DropIndex("dbo.FloutecInterData", new[] { "FloutecMeasureLineId" });
            DropIndex("dbo.FloutecInstantData", new[] { "FloutecMeasureLineId" });
            DropIndex("dbo.FloutecIdentData", new[] { "FloutecMeasureLineId" });
            DropIndex("dbo.FloutecHourlyData", new[] { "FloutecMeasureLineId" });
            DropIndex("dbo.FloutecAlarmData", new[] { "FloutecMeasureLineId" });
            DropIndex("dbo.ScanPeriods", new[] { "ScheduledScan_Id" });
            DropIndex("dbo.ScanPeriods", new[] { "ScanId" });
            DropIndex("dbo.ScanMembers", new[] { "ScanBaseId" });
            DropIndex("dbo.ScanMembers", new[] { "MeasurePointId" });
            DropIndex("dbo.ScanMembers", new[] { "EstimatorId" });
            DropIndex("dbo.MeasurePoints", new[] { "EstimatorId" });
            DropIndex("dbo.Estimators", new[] { "CustomerId" });
            DropTable("dbo.Roc809s");
            DropTable("dbo.Roc809MeasurePoints");
            DropTable("dbo.PeriodicScans");
            DropTable("dbo.Floutecs");
            DropTable("dbo.FloutecMeasureLines");
            DropTable("dbo.Roc809EventTypes");
            DropTable("dbo.Roc809EventCodes");
            DropTable("dbo.Roc809AlarmTypes");
            DropTable("dbo.Roc809AlarmCodes");
            DropTable("dbo.FloutecSensorsTypes");
            DropTable("dbo.FloutecParamsTypes");
            DropTable("dbo.FloutecIntersTypes");
            DropTable("dbo.FloutecAlarmsTypes");
            DropTable("dbo.Roc809EventData");
            DropTable("dbo.Roc809AlarmData");
            DropTable("dbo.Roc809PeriodicData");
            DropTable("dbo.Roc809MinuteData");
            DropTable("dbo.Roc809DailyData");
            DropTable("dbo.FloutecInterData");
            DropTable("dbo.FloutecInstantData");
            DropTable("dbo.FloutecIdentData");
            DropTable("dbo.FloutecHourlyData");
            DropTable("dbo.FloutecAlarmData");
            DropTable("dbo.ScanPeriods");
            DropTable("dbo.Scans");
            DropTable("dbo.ScanMembers");
            DropTable("dbo.MeasurePoints");
            DropTable("dbo.Estimators");
            DropTable("dbo.Customers");
        }
    }
}

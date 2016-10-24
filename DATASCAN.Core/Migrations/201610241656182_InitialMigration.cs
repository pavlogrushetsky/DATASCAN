namespace DATASCAN.Core.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "General.Customers",
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
                "General.Estimators",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Phone = c.String(maxLength: 13),
                        IsScannedViaGPRS = c.Boolean(nullable: false),
                        CustomerId = c.Int(),
                        GroupId = c.Int(),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Customers", t => t.CustomerId)
                .ForeignKey("General.Groups", t => t.GroupId)
                .Index(t => t.CustomerId)
                .Index(t => t.GroupId);
            
            CreateTable(
                "General.Groups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        CustomerId = c.Int(),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Customers", t => t.CustomerId)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "General.MeasurePoints",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        Number = c.Int(nullable: false),
                        EstimatorId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Estimators", t => t.EstimatorId, cascadeDelete: true)
                .Index(t => t.EstimatorId);
            
            CreateTable(
                "Floutec.AlarmData",
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
                .ForeignKey("Floutec.MeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "Floutec.HourlyData",
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
                .ForeignKey("Floutec.MeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "Floutec.IdentData",
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
                .ForeignKey("Floutec.MeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "Floutec.InstantData",
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
                .ForeignKey("Floutec.MeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "Floutec.InterData",
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
                .ForeignKey("Floutec.MeasureLines", t => t.FloutecMeasureLineId)
                .Index(t => t.FloutecMeasureLineId);
            
            CreateTable(
                "Roc809.DailyData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Roc809.MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "Roc809.MinuteData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Roc809.MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "Roc809.PeriodicData",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Roc809MeasurePointId = c.Int(nullable: false),
                        Period = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Value = c.Double(nullable: false),
                        DateAdded = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Roc809.MeasurePoints", t => t.Roc809MeasurePointId)
                .Index(t => t.Roc809MeasurePointId);
            
            CreateTable(
                "Scan.Members",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EstimatorId = c.Int(nullable: false),
                        ScanBaseId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Estimators", t => t.EstimatorId, cascadeDelete: true)
                .ForeignKey("Scan.Scans", t => t.ScanBaseId, cascadeDelete: true)
                .Index(t => t.EstimatorId)
                .Index(t => t.ScanBaseId);
            
            CreateTable(
                "Scan.Scans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 200),
                        DateLastScanned = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "Scan.Periods",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Period = c.Time(nullable: false, precision: 7),
                        ScanId = c.Int(nullable: false),
                        DateCreated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        DateModified = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scan.SheduledScans", t => t.ScanId)
                .Index(t => t.ScanId);
            
            CreateTable(
                "Roc809.AlarmData",
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
                .ForeignKey("Roc809.Estimators", t => t.Roc809Id)
                .Index(t => t.Roc809Id);
            
            CreateTable(
                "Roc809.EventData",
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
                .ForeignKey("Roc809.Estimators", t => t.Roc809Id)
                .Index(t => t.Roc809Id);
            
            CreateTable(
                "Floutec.AlarmTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description_45 = c.String(nullable: false, maxLength: 400),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Floutec.InterTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description_45 = c.String(nullable: false, maxLength: 400),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Floutec.ParamTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Param = c.String(nullable: false, maxLength: 50),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Floutec.SensorTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Roc809.AlarmCodes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Roc809.AlarmTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Roc809.EventCodes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Roc809.EventTypes",
                c => new
                    {
                        Code = c.Int(nullable: false),
                        Description = c.String(nullable: false, maxLength: 400),
                    })
                .PrimaryKey(t => t.Code);
            
            CreateTable(
                "Floutec.MeasureLines",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        SensorType = c.Int(nullable: false),
                        DateHourlyDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateInstantDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateIdentDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateInterDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateAlarmDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.MeasurePoints", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Floutec.Estimators",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Estimators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Scan.FloutecMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ScanIdentData = c.Boolean(nullable: false),
                        ScanAlarmData = c.Boolean(nullable: false),
                        ScanInstantData = c.Boolean(nullable: false),
                        ScanInterData = c.Boolean(nullable: false),
                        ScanHourlyData = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scan.Members", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Scan.PeriodicScans",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        PeriodType = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scan.Scans", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Roc809.MeasurePoints",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        HistSegment = c.Int(nullable: false),
                        DateMinuteDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DatePeriodicDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateDailyDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.MeasurePoints", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Roc809.Estimators",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Address = c.String(maxLength: 15),
                        Port = c.Int(nullable: false),
                        RocUnit = c.Int(nullable: false),
                        RocGroup = c.Int(nullable: false),
                        HostUnit = c.Int(nullable: false),
                        HostGroup = c.Int(nullable: false),
                        DateAlarmDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                        DateEventDataLastUpdated = c.DateTime(precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("General.Estimators", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Scan.RocMembers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        ScanEventData = c.Boolean(nullable: false),
                        ScanAlarmData = c.Boolean(nullable: false),
                        ScanMinuteData = c.Boolean(nullable: false),
                        ScanPeriodicData = c.Boolean(nullable: false),
                        ScanDailyData = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scan.Members", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "Scan.SheduledScans",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("Scan.Scans", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("Scan.SheduledScans", "Id", "Scan.Scans");
            DropForeignKey("Scan.RocMembers", "Id", "Scan.Members");
            DropForeignKey("Roc809.Estimators", "Id", "General.Estimators");
            DropForeignKey("Roc809.MeasurePoints", "Id", "General.MeasurePoints");
            DropForeignKey("Scan.PeriodicScans", "Id", "Scan.Scans");
            DropForeignKey("Scan.FloutecMembers", "Id", "Scan.Members");
            DropForeignKey("Floutec.Estimators", "Id", "General.Estimators");
            DropForeignKey("Floutec.MeasureLines", "Id", "General.MeasurePoints");
            DropForeignKey("Roc809.EventData", "Roc809Id", "Roc809.Estimators");
            DropForeignKey("Roc809.AlarmData", "Roc809Id", "Roc809.Estimators");
            DropForeignKey("Scan.Periods", "ScanId", "Scan.SheduledScans");
            DropForeignKey("Scan.Members", "ScanBaseId", "Scan.Scans");
            DropForeignKey("Scan.Members", "EstimatorId", "General.Estimators");
            DropForeignKey("Roc809.PeriodicData", "Roc809MeasurePointId", "Roc809.MeasurePoints");
            DropForeignKey("Roc809.MinuteData", "Roc809MeasurePointId", "Roc809.MeasurePoints");
            DropForeignKey("Roc809.DailyData", "Roc809MeasurePointId", "Roc809.MeasurePoints");
            DropForeignKey("Floutec.InterData", "FloutecMeasureLineId", "Floutec.MeasureLines");
            DropForeignKey("Floutec.InstantData", "FloutecMeasureLineId", "Floutec.MeasureLines");
            DropForeignKey("Floutec.IdentData", "FloutecMeasureLineId", "Floutec.MeasureLines");
            DropForeignKey("Floutec.HourlyData", "FloutecMeasureLineId", "Floutec.MeasureLines");
            DropForeignKey("Floutec.AlarmData", "FloutecMeasureLineId", "Floutec.MeasureLines");
            DropForeignKey("General.MeasurePoints", "EstimatorId", "General.Estimators");
            DropForeignKey("General.Estimators", "GroupId", "General.Groups");
            DropForeignKey("General.Groups", "CustomerId", "General.Customers");
            DropForeignKey("General.Estimators", "CustomerId", "General.Customers");
            DropIndex("Scan.SheduledScans", new[] { "Id" });
            DropIndex("Scan.RocMembers", new[] { "Id" });
            DropIndex("Roc809.Estimators", new[] { "Id" });
            DropIndex("Roc809.MeasurePoints", new[] { "Id" });
            DropIndex("Scan.PeriodicScans", new[] { "Id" });
            DropIndex("Scan.FloutecMembers", new[] { "Id" });
            DropIndex("Floutec.Estimators", new[] { "Id" });
            DropIndex("Floutec.MeasureLines", new[] { "Id" });
            DropIndex("Roc809.EventData", new[] { "Roc809Id" });
            DropIndex("Roc809.AlarmData", new[] { "Roc809Id" });
            DropIndex("Scan.Periods", new[] { "ScanId" });
            DropIndex("Scan.Members", new[] { "ScanBaseId" });
            DropIndex("Scan.Members", new[] { "EstimatorId" });
            DropIndex("Roc809.PeriodicData", new[] { "Roc809MeasurePointId" });
            DropIndex("Roc809.MinuteData", new[] { "Roc809MeasurePointId" });
            DropIndex("Roc809.DailyData", new[] { "Roc809MeasurePointId" });
            DropIndex("Floutec.InterData", new[] { "FloutecMeasureLineId" });
            DropIndex("Floutec.InstantData", new[] { "FloutecMeasureLineId" });
            DropIndex("Floutec.IdentData", new[] { "FloutecMeasureLineId" });
            DropIndex("Floutec.HourlyData", new[] { "FloutecMeasureLineId" });
            DropIndex("Floutec.AlarmData", new[] { "FloutecMeasureLineId" });
            DropIndex("General.MeasurePoints", new[] { "EstimatorId" });
            DropIndex("General.Groups", new[] { "CustomerId" });
            DropIndex("General.Estimators", new[] { "GroupId" });
            DropIndex("General.Estimators", new[] { "CustomerId" });
            DropTable("Scan.SheduledScans");
            DropTable("Scan.RocMembers");
            DropTable("Roc809.Estimators");
            DropTable("Roc809.MeasurePoints");
            DropTable("Scan.PeriodicScans");
            DropTable("Scan.FloutecMembers");
            DropTable("Floutec.Estimators");
            DropTable("Floutec.MeasureLines");
            DropTable("Roc809.EventTypes");
            DropTable("Roc809.EventCodes");
            DropTable("Roc809.AlarmTypes");
            DropTable("Roc809.AlarmCodes");
            DropTable("Floutec.SensorTypes");
            DropTable("Floutec.ParamTypes");
            DropTable("Floutec.InterTypes");
            DropTable("Floutec.AlarmTypes");
            DropTable("Roc809.EventData");
            DropTable("Roc809.AlarmData");
            DropTable("Scan.Periods");
            DropTable("Scan.Scans");
            DropTable("Scan.Members");
            DropTable("Roc809.PeriodicData");
            DropTable("Roc809.MinuteData");
            DropTable("Roc809.DailyData");
            DropTable("Floutec.InterData");
            DropTable("Floutec.InstantData");
            DropTable("Floutec.IdentData");
            DropTable("Floutec.HourlyData");
            DropTable("Floutec.AlarmData");
            DropTable("General.MeasurePoints");
            DropTable("General.Groups");
            DropTable("General.Estimators");
            DropTable("General.Customers");
        }
    }
}

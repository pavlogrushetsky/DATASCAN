namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedScanMembers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Scan.Members", "MeasurePointId", "General.MeasurePoints");
            DropIndex("Scan.Members", new[] { "MeasurePointId" });
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
            
            DropColumn("Scan.Members", "MeasurePointId");
            DropColumn("Scan.Members", "DataType");
        }
        
        public override void Down()
        {
            AddColumn("Scan.Members", "DataType", c => c.String(nullable: false, maxLength: 50));
            AddColumn("Scan.Members", "MeasurePointId", c => c.Int());
            DropForeignKey("Scan.RocMembers", "Id", "Scan.Members");
            DropForeignKey("Scan.FloutecMembers", "Id", "Scan.Members");
            DropIndex("Scan.RocMembers", new[] { "Id" });
            DropIndex("Scan.FloutecMembers", new[] { "Id" });
            DropTable("Scan.RocMembers");
            DropTable("Scan.FloutecMembers");
            CreateIndex("Scan.Members", "MeasurePointId");
            AddForeignKey("Scan.Members", "MeasurePointId", "General.MeasurePoints", "Id");
        }
    }
}

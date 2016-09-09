namespace DATASCAN.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedNameToEstimatorsGroup : DbMigration
    {
        public override void Up()
        {
            AddColumn("General.Groups", "Name", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("General.Groups", "Name");
        }
    }
}

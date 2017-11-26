namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsdsfd1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Organisations", "OrganisationId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Organisations", "OrganisationId", c => c.Int(nullable: false));
        }
    }
}

namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AndriySergenuk_1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Organisations", "Avatar", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Organisations", "Avatar");
        }
    }
}

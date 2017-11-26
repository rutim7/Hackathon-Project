namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsdsfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "AvatarNews", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "AvatarNews");
        }
    }
}

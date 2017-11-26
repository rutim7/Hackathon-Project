namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fsdsfddff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.News", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Category");
        }
    }
}

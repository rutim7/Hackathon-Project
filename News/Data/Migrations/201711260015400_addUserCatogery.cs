namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addUserCatogery : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "UserCategories", c => c.String());
            AddColumn("dbo.News", "Category", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.News", "Category");
            DropColumn("dbo.AspNetUsers", "UserCategories");
        }
    }
}

namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewDetails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Reviews", "ReviewerEmailAddress", c => c.String(maxLength: 1000));
            AddColumn("dbo.Reviews", "Rating", c => c.Int(nullable: false));
            AlterColumn("dbo.Reviews", "Text", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Text", c => c.String(maxLength: 1000));
            DropColumn("dbo.Reviews", "Rating");
            DropColumn("dbo.Reviews", "ReviewerEmailAddress");
        }
    }
}

namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ReviewValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Reviews", "ReviewerEmailAddress", c => c.String(nullable: false));
            AlterColumn("dbo.Reviews", "Text", c => c.String(nullable: false, maxLength: 1000));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Reviews", "Text", c => c.String());
            AlterColumn("dbo.Reviews", "ReviewerEmailAddress", c => c.String(maxLength: 1000));
        }
    }
}

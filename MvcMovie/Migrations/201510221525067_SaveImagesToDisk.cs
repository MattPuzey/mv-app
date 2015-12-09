namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SaveImagesToDisk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Files", "FilePath", c => c.String());
            DropColumn("dbo.Files", "Content");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Content", c => c.Binary());
            DropColumn("dbo.Files", "FilePath");
        }
    }
}

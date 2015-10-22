namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Images : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Movie_ID = c.Int(),
                        Uploader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_ID)
                .ForeignKey("dbo.Users", t => t.Uploader_Id)
                .Index(t => t.Movie_ID)
                .Index(t => t.Uploader_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "Uploader_Id", "dbo.Users");
            DropForeignKey("dbo.Images", "Movie_ID", "dbo.Movies");
            DropIndex("dbo.Images", new[] { "Uploader_Id" });
            DropIndex("dbo.Images", new[] { "Movie_ID" });
            DropTable("dbo.Images");
        }
    }
}

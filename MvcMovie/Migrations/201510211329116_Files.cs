namespace MvcMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Files : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "Movie_ID", "dbo.Movies");
            DropForeignKey("dbo.Images", "Uploader_Id", "dbo.Users");
            DropIndex("dbo.Images", new[] { "Movie_ID" });
            DropIndex("dbo.Images", new[] { "Uploader_Id" });
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        PersonId = c.Int(nullable: false),
                        Movie_ID = c.Int(),
                        Uploader_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Movies", t => t.Movie_ID)
                .ForeignKey("dbo.Users", t => t.Uploader_Id)
                .Index(t => t.Movie_ID)
                .Index(t => t.Uploader_Id);
            
            DropTable("dbo.Images");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Files", "Uploader_Id", "dbo.Users");
            DropForeignKey("dbo.Files", "Movie_ID", "dbo.Movies");
            DropIndex("dbo.Files", new[] { "Uploader_Id" });
            DropIndex("dbo.Files", new[] { "Movie_ID" });
            DropTable("dbo.Files");
            CreateIndex("dbo.Images", "Uploader_Id");
            CreateIndex("dbo.Images", "Movie_ID");
            AddForeignKey("dbo.Images", "Uploader_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Images", "Movie_ID", "dbo.Movies", "ID");
        }
    }
}

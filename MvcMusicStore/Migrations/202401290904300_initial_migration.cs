namespace MvcMusicStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial_migration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumId = c.Int(nullable: false, identity: true),
                        GenreId = c.Int(nullable: false),
                        ArtistId = c.Int(nullable: false),
                        Title = c.String(nullable: false, maxLength: 160),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AlbumArtUrl = c.String(maxLength: 1024),
                    })
                .PrimaryKey(t => t.AlbumId)
                .ForeignKey("dbo.Artists", t => t.ArtistId, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.GenreId, cascadeDelete: true)
                .Index(t => t.GenreId)
                .Index(t => t.ArtistId);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ArtistId);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.GenreId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        ActivationCode = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserRoleMappings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsersRoles",
                c => new
                    {
                        Users_Id = c.Int(nullable: false),
                        Roles_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Users_Id, t.Roles_Id })
                .ForeignKey("dbo.User", t => t.Users_Id, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Roles_Id, cascadeDelete: true)
                .Index(t => t.Users_Id)
                .Index(t => t.Roles_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsersRoles", "Roles_Id", "dbo.Role");
            DropForeignKey("dbo.UsersRoles", "Users_Id", "dbo.User");
            DropForeignKey("dbo.Albums", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Albums", "ArtistId", "dbo.Artists");
            DropIndex("dbo.UsersRoles", new[] { "Roles_Id" });
            DropIndex("dbo.UsersRoles", new[] { "Users_Id" });
            DropIndex("dbo.Albums", new[] { "ArtistId" });
            DropIndex("dbo.Albums", new[] { "GenreId" });
            DropTable("dbo.UsersRoles");
            DropTable("dbo.UserRoleMappings");
            DropTable("dbo.User");
            DropTable("dbo.Role");
            DropTable("dbo.Genres");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
        }
    }
}

﻿namespace BiasedSocialMedia.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class inital1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Posts_PostID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Posts_PostID)
                .Index(t => t.Posts_PostID);
            
            CreateTable(
                "dbo.Followers",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CurrentUserID = c.Int(nullable: false),
                        FollowerUserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.FollowerUserId, cascadeDelete: true)
                .Index(t => t.FollowerUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Email = c.String(),
                        ProfilePhotoID = c.Int(nullable: false),
                        Password = c.String(),
                        UserName = c.String(),
                        Gender = c.String(maxLength: 1, fixedLength: true, unicode: false),
                        PhoneNumber = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.LikeUnlikeStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        LikedById = c.Int(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.LikedById, cascadeDelete: true)
                .Index(t => t.LikedById);
            
            CreateTable(
                "dbo.LoginLogs",
                c => new
                    {
                        LogID = c.Int(nullable: false, identity: true),
                        UserID = c.String(),
                        LastLogin = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        Remarks = c.String(),
                    })
                .PrimaryKey(t => t.LogID);
            
            CreateTable(
                "dbo.MediaInfo",
                c => new
                    {
                        MediaID = c.Int(nullable: false, identity: true),
                        FileName = c.String(),
                        MediaURL = c.String(),
                        Data = c.Binary(),
                    })
                .PrimaryKey(t => t.MediaID);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        PostID = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostContent = c.String(),
                        CreatedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        UpdatedAt = c.DateTime(nullable: false, defaultValueSql: "GETDATE()"),
                        isDeleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PostID)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Posts", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "Posts_PostID", "dbo.Posts");
            DropForeignKey("dbo.LikeUnlikeStatus", "LikedById", "dbo.Users");
            DropForeignKey("dbo.Followers", "FollowerUserId", "dbo.Users");
            DropIndex("dbo.Posts", new[] { "UserId" });
            DropIndex("dbo.LikeUnlikeStatus", new[] { "LikedById" });
            DropIndex("dbo.Followers", new[] { "FollowerUserId" });
            DropIndex("dbo.Comments", new[] { "Posts_PostID" });
            DropTable("dbo.Posts");
            DropTable("dbo.MediaInfo");
            DropTable("dbo.LoginLogs");
            DropTable("dbo.LikeUnlikeStatus");
            DropTable("dbo.Users");
            DropTable("dbo.Followers");
            DropTable("dbo.Comments");
        }
    }
}
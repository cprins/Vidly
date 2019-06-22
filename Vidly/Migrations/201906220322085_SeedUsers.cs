namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'597fe305-bf1b-4bbe-bc1b-2b7e5bd3a8ab', N'cesarprins@gmail.com', 0, N'ADDwsDA8Sm3FBdTdZzoeMX8pbF63/TlaC3FSncYwyMr+WHd5lz3nELOQeg60TSWpww==', N'0ce84d6c-7710-41f2-b8a3-b386d2ab291c', NULL, 0, 0, NULL, 1, 0, N'cesarprins@gmail.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'5c3d9f77-5f5f-4146-869b-66d83842a052', N'admin@vidly.com', 0, N'ACcsQeSQKWRAd8xFqDbBtUC5m0MXsVOdsr67b0I7h7DleYTqtEwIJTsmpxyBRIQtqQ==', N'86d9f831-450f-49e7-9cbc-19396c771ca2', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'0ac466c4-30ec-4612-bda9-c37c7b96ef09', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5c3d9f77-5f5f-4146-869b-66d83842a052', N'0ac466c4-30ec-4612-bda9-c37c7b96ef09')
");
        }
        
        public override void Down()
        {
        }
    }
}

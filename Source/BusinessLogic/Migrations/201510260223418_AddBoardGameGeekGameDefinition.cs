namespace BusinessLogic.Migrations
{
    using DataAccess.Security;
    using DataAccess;
    using System;
    using System.Data.Entity.Migrations;
    using Models;
    using System.Collections.Generic;
    using System.Linq;
    using Models.User;

    public partial class AddBoardGameGeekGameDefinition : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BoardGameGeekGameDefinition",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: false),
                        Name = c.String(),
                        Thumbnail = c.String(),
                    })
                .PrimaryKey(t => t.Id);

            Sql("UPDATE GameDefinition SET BoardGameGeekObjectid = NULL WHERE BoardGameGeekObjectId = 0");

            RenameColumn("dbo.GameDefinition", "BoardGameGeekObjectId", "BoardGameGeekGameDefinitionId");
            CreateIndex("dbo.GameDefinition", "BoardGameGeekGameDefinitionId");
            
            Sql(@"WITH BGG_CTE (Name, BoardGameGeekGameDefinitionId, ThumbnailImageUrl)
                    AS
                    (
                        SELECT
                              [Name]
                              ,[BoardGameGeekGameDefinitionId]
                              ,[ThumbnailImageUrl]
                          FROM (SELECT  [Name]
                              ,[BoardGameGeekGameDefinitionId]
                              ,[ThumbnailImageUrl]
                              ,row_number() over(partition by [BoardGameGeekGameDefinitionId] order by ThumbnailImageUrl desc) as rowOrder
                              FROM [NerdScorekeeper].[dbo].[GameDefinition]) temp
                          Where roworder = 1
                          AND COALESCE(BoardGameGeekGameDefinitionId, 0) <> 0
                      )
                      INSERT INTO BoardGameGeekGameDefinition (Id, Name, Thumbnail)
                      SELECT [BoardGameGeekGameDefinitionId], Name, ThumbnailImageUrl
                      FROM BGG_CTE");
            AddForeignKey("dbo.GameDefinition", "BoardGameGeekGameDefinitionId", "dbo.BoardGameGeekGameDefinition", "Id", cascadeDelete: true);
        }
        public override void Down()
        {
            RenameColumn("dbo.GameDefinition", "BoardGameGeekGameDefinitionId", "BoardGameGeekObjectId");
            DropForeignKey("dbo.GameDefinition", "BoardGameGeekGameDefinitionId", "dbo.BoardGameGeekGameDefinition");
            DropIndex("dbo.GameDefinition", new[] { "BoardGameGeekGameDefinitionId" });
            DropTable("dbo.BoardGameGeekGameDefinition");
        }
    }
}

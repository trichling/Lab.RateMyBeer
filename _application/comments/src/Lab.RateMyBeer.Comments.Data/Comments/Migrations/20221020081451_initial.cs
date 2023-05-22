using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.RateMyBeer.Comments.Data.Comments.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    CommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CheckinId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.CommentsId);
                });

            migrationBuilder.CreateTable(
                name: "CommentsCommentEntries",
                columns: table => new
                {
                    CommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CommentsDataCommentsId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsCommentEntries", x => x.CommentId);
                    table.ForeignKey(
                        name: "FK_CommentsCommentEntries_Comments_CommentsDataCommentsId",
                        column: x => x.CommentsDataCommentsId,
                        principalTable: "Comments",
                        principalColumn: "CommentsId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentsCommentEntries_CommentsDataCommentsId",
                table: "CommentsCommentEntries",
                column: "CommentsDataCommentsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommentsCommentEntries");

            migrationBuilder.DropTable(
                name: "Comments");
        }
    }
}

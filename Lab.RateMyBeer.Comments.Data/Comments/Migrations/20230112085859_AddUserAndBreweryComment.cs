using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab.RateMyBeer.Comments.Data.Comments.Migrations
{
    public partial class AddUserAndBreweryComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BreweryComment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserComment",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BreweryComment",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "UserComment",
                table: "Comments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelWebApp.Data.Migrations
{
    public partial class Addedauthor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Novel",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Novel");
        }
    }
}

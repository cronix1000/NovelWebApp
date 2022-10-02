using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelWebApp.Data.Migrations
{
    public partial class changedchaptermodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "chapterStory",
                table: "Chapter",
                newName: "Story");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Story",
                table: "Chapter",
                newName: "chapterStory");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NovelWebApp.Data.Migrations
{
    public partial class ADDEDnovelid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Novel_NovelId",
                table: "Chapter");

            migrationBuilder.RenameColumn(
                name: "Story",
                table: "Chapter",
                newName: "chapterStory");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Novel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Novel",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "NovelId",
                table: "Chapter",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Novel_NovelId",
                table: "Chapter",
                column: "NovelId",
                principalTable: "Novel",
                principalColumn: "NovelId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Chapter_Novel_NovelId",
                table: "Chapter");

            migrationBuilder.RenameColumn(
                name: "chapterStory",
                table: "Chapter",
                newName: "Story");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Novel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Novel",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NovelId",
                table: "Chapter",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Chapter_Novel_NovelId",
                table: "Chapter",
                column: "NovelId",
                principalTable: "Novel",
                principalColumn: "NovelId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeImageUrlToPath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Elections",
                newName: "ImagePath");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Candidates",
                newName: "ImagePath");

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                column: "ImagePath",
                value: "https://tr.wikipedia.org/wiki/T%C3%BCrkiye_Cumhurba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_Forsu#/media/Dosya:Flag_of_the_President_of_Turkey.svgg");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Elections",
                newName: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImagePath",
                table: "Candidates",
                newName: "ImageUrl");

            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                column: "ImageUrl",
                value: "https://tr.wikipedia.org/wiki/T%C3%BCrkiye_Cumhurba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_Forsu#/media/Dosya:Emblem_of_the_Presidency_of_Turkey.svg");
        }
    }
}

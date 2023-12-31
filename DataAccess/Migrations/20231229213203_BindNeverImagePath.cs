using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class BindNeverImagePath : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                column: "ImagePath",
                value: "C:/Users/Fatih/Desktop/fors.png");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1,
                column: "ImagePath",
                value: "https://tr.wikipedia.org/wiki/T%C3%BCrkiye_Cumhurba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_Forsu#/media/Dosya:Flag_of_the_President_of_Turkey.svgg");
        }
    }
}

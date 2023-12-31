using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddCandidateImage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Candidates",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Elections",
                keyColumn: "ElectionId",
                keyValue: 1);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Candidates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Candidates");

            migrationBuilder.InsertData(
                table: "Elections",
                columns: new[] { "ElectionId", "Description", "EndDate", "ImageUrl", "StartDate", "Title" },
                values: new object[] { 1, "2023 Cumhurbaşkanlığı Seçimi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "https://tr.wikipedia.org/wiki/T%C3%BCrkiye_Cumhurba%C5%9Fkanl%C4%B1%C4%9F%C4%B1_Forsu#/media/Dosya:Emblem_of_the_Presidency_of_Turkey.svg", new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), "2023 Cumhurbaşkanlığı Seçimi" });

            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Description", "ElectionId", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Ak Parti Genel başkanı", 1, "Recep Tayyip", "Erdoğan" },
                    { 2, "CHP Genel başkanı", 1, "Kemal", "Kılıçdaroğlu" },
                    { 3, "Zafer Partisi adayı", 1, "Sinan", "Oğan" }
                });
        }
    }
}

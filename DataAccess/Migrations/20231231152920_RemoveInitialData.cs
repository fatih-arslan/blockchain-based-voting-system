using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Elections",
                keyColumn: "Id",
                keyValue: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Elections",
                columns: new[] { "Id", "Description", "EndDate", "ImagePath", "StartDate", "Title" },
                values: new object[] { 1, "2023 Cumhurbaşkanlığı Seçimi", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "images/fors.png", new DateTime(2023, 12, 27, 0, 0, 0, 0, DateTimeKind.Utc), "2023 Cumhurbaşkanlığı Seçimi" });
        }
    }
}

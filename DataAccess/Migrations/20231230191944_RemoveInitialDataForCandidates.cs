using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class RemoveInitialDataForCandidates : Migration
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Candidates",
                columns: new[] { "Id", "Description", "ElectionId", "ImagePath", "Name", "Surname" },
                values: new object[,]
                {
                    { 1, "Ak Parti Genel başkanı", 1, "https://tr.wikipedia.org/wiki/Recep_Tayyip_Erdo%C4%9Fan#/media/Dosya:Recep_Tayyip_Erdogan_in_Ukraine.jpg", "Recep Tayyip", "Erdoğan" },
                    { 2, "CHP Genel başkanı", 1, "https://tr.wikipedia.org/wiki/Kemal_K%C4%B1l%C4%B1%C3%A7daro%C4%9Flu#/media/Dosya:Kemal_K%C4%B1l%C4%B1%C3%A7daro%C4%9Flu_in_March_2023_(cropped).png", "Kemal", "Kılıçdaroğlu" },
                    { 3, "Zafer Partisi adayı", 1, "https://tr.wikipedia.org/wiki/Sinan_O%C4%9Fan#/media/Dosya:Dr._Sinan_O%C4%9Fan_2023_(cropped).jpg", "Sinan", "Oğan" }
                });
        }
    }
}

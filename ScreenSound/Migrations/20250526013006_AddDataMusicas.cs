using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ScreenSound.Migrations
{
    /// <inheritdoc />
    public partial class AddDataMusicas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Musicas",
                columns: new[] { "Nome", "AnoLancamento" },
                values: new object[,]
                {
                    { "Oceano", 1989 },
                    { "Flor de Lis", 1976 },
                    { "Samurai", 1982 },
                    { "Se", 1992 },

                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Musicas");
        }
    }
}
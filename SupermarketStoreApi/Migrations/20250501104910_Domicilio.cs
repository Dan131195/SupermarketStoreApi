using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermarketStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class Domicilio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Indirizzo",
                table: "Clienti",
                newName: "Domicilio");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Domicilio",
                table: "Clienti",
                newName: "Indirizzo");
        }
    }
}

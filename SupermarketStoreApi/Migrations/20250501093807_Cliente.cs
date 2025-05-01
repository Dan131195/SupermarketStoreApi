using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SupermarketStoreApi.Migrations
{
    /// <inheritdoc />
    public partial class Cliente : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImmagineProfilo",
                table: "Clienti",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImmagineProfilo",
                table: "Clienti");
        }
    }
}

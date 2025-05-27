using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace minard_teste_2.Migrations
{
    /// <inheritdoc />
    public partial class inicialDesigner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Donos",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Donos",
                newName: "Name");
        }
    }
}

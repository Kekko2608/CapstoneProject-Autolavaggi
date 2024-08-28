using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProject_Autolavaggi.Migrations
{
    /// <inheritdoc />
    public partial class updateTabellaRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Role",
                newName: "Nome");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "Role",
                newName: "Name");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProject_Autolavaggi.Migrations
{
    /// <inheritdoc />
    public partial class urlMaps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleMapsUrl",
                table: "Autolavaggi",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleMapsUrl",
                table: "Autolavaggi");
        }
    }
}

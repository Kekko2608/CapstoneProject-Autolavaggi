using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CapstoneProject_Autolavaggi.Migrations
{
    /// <inheritdoc />
    public partial class addPrenotazview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PrenotazioneServizio");

            migrationBuilder.AddColumn<int>(
                name: "ServizioId",
                table: "Prenotazioni",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Prenotazioni_ServizioId",
                table: "Prenotazioni",
                column: "ServizioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Prenotazioni_Servizi_ServizioId",
                table: "Prenotazioni",
                column: "ServizioId",
                principalTable: "Servizi",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Prenotazioni_Servizi_ServizioId",
                table: "Prenotazioni");

            migrationBuilder.DropIndex(
                name: "IX_Prenotazioni_ServizioId",
                table: "Prenotazioni");

            migrationBuilder.DropColumn(
                name: "ServizioId",
                table: "Prenotazioni");

            migrationBuilder.CreateTable(
                name: "PrenotazioneServizio",
                columns: table => new
                {
                    PrenotazioniId = table.Column<int>(type: "int", nullable: false),
                    SeriziId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrenotazioneServizio", x => new { x.PrenotazioniId, x.SeriziId });
                    table.ForeignKey(
                        name: "FK_PrenotazioneServizio_Prenotazioni_PrenotazioniId",
                        column: x => x.PrenotazioniId,
                        principalTable: "Prenotazioni",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrenotazioneServizio_Servizi_SeriziId",
                        column: x => x.SeriziId,
                        principalTable: "Servizi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PrenotazioneServizio_SeriziId",
                table: "PrenotazioneServizio",
                column: "SeriziId");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aplicatieHandbal.Migrations
{
    /// <inheritdoc />
    public partial class identificator_principal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Games_GameId",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "GameId",
                table: "Tickets",
                newName: "GameID");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "Tickets",
                newName: "TicketID");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_GameId",
                table: "Tickets",
                newName: "IX_Tickets_GameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Staff",
                newName: "StaffID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Players",
                newName: "PlayerID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Games",
                newName: "GameID");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Articole",
                newName: "ArticoleID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Games_GameID",
                table: "Tickets",
                column: "GameID",
                principalTable: "Games",
                principalColumn: "GameID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_Games_GameID",
                table: "Tickets");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Tickets",
                newName: "GameId");

            migrationBuilder.RenameColumn(
                name: "TicketID",
                table: "Tickets",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_Tickets_GameID",
                table: "Tickets",
                newName: "IX_Tickets_GameId");

            migrationBuilder.RenameColumn(
                name: "StaffID",
                table: "Staff",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "PlayerID",
                table: "Players",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "GameID",
                table: "Games",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ArticoleID",
                table: "Articole",
                newName: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_Games_GameId",
                table: "Tickets",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

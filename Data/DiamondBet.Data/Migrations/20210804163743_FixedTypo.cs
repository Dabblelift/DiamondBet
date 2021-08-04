namespace DiamondBet.Data.Migrations
{
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class FixedTypo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Competitions_ComnetitionId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "ComnetitionId",
                table: "Games",
                newName: "CompetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_ComnetitionId",
                table: "Games",
                newName: "IX_Games_CompetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Competitions_CompetitionId",
                table: "Games",
                column: "CompetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Games_Competitions_CompetitionId",
                table: "Games");

            migrationBuilder.RenameColumn(
                name: "CompetitionId",
                table: "Games",
                newName: "ComnetitionId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_CompetitionId",
                table: "Games",
                newName: "IX_Games_ComnetitionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Games_Competitions_ComnetitionId",
                table: "Games",
                column: "ComnetitionId",
                principalTable: "Competitions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

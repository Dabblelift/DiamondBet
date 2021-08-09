using Microsoft.EntityFrameworkCore.Migrations;

namespace DiamondBet.Data.Migrations
{
    public partial class MakeOddsGameNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Odds_GameId",
                table: "Odds");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Odds",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Odds_GameId",
                table: "Odds",
                column: "GameId",
                unique: true,
                filter: "[GameId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Odds_GameId",
                table: "Odds");

            migrationBuilder.AlterColumn<int>(
                name: "GameId",
                table: "Odds",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Odds_GameId",
                table: "Odds",
                column: "GameId",
                unique: true);
        }
    }
}

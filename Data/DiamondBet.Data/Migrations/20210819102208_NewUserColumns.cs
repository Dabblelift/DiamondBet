using Microsoft.EntityFrameworkCore.Migrations;

namespace DiamondBet.Data.Migrations
{
    public partial class NewUserColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FirstPlaces",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SecondPlaces",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ThirdPlaces",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FirstPlaces",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "SecondPlaces",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ThirdPlaces",
                table: "AspNetUsers");
        }
    }
}

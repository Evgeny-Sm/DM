using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class ControlList : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Controls_FileUnitId",
                table: "Controls");

            migrationBuilder.AddColumn<bool>(
                name: "IsInAction",
                table: "Controls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "TimeForChecking",
                table: "Controls",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Controls_FileUnitId",
                table: "Controls",
                column: "FileUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Controls_FileUnitId",
                table: "Controls");

            migrationBuilder.DropColumn(
                name: "IsInAction",
                table: "Controls");

            migrationBuilder.DropColumn(
                name: "TimeForChecking",
                table: "Controls");

            migrationBuilder.CreateIndex(
                name: "IX_Controls_FileUnitId",
                table: "Controls",
                column: "FileUnitId",
                unique: true);
        }
    }
}

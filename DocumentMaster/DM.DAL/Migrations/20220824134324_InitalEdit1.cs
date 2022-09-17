using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class InitalEdit1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeToDev",
                table: "FileUnits");

            migrationBuilder.AddColumn<double>(
                name: "TimeForAction",
                table: "UserActions",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeForAction",
                table: "UserActions");

            migrationBuilder.AddColumn<double>(
                name: "TimeToDev",
                table: "FileUnits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class ProjectCode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProjectCode",
                table: "FileUnits",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProjectCode",
                table: "FileUnits");
        }
    }
}

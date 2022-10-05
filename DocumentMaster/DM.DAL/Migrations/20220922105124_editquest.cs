using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class editquest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PrijectId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ProjectId",
                table: "Questions",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Projects_ProjectId",
                table: "Questions",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Projects_ProjectId",
                table: "Questions");

            migrationBuilder.DropIndex(
                name: "IX_Questions_ProjectId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "PrijectId",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Questions");
        }
    }
}

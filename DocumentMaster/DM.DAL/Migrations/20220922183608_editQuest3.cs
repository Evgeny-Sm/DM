using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class editQuest3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LinkedFile",
                table: "Questions");

            migrationBuilder.CreateTable(
                name: "FileUnitQuestion",
                columns: table => new
                {
                    FileUnitsId = table.Column<int>(type: "int", nullable: false),
                    QuestionsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUnitQuestion", x => new { x.FileUnitsId, x.QuestionsId });
                    table.ForeignKey(
                        name: "FK_FileUnitQuestion_FileUnits_FileUnitsId",
                        column: x => x.FileUnitsId,
                        principalTable: "FileUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileUnitQuestion_Questions_QuestionsId",
                        column: x => x.QuestionsId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUnitQuestion_QuestionsId",
                table: "FileUnitQuestion",
                column: "QuestionsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUnitQuestion");

            migrationBuilder.AddColumn<string>(
                name: "LinkedFile",
                table: "Questions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}

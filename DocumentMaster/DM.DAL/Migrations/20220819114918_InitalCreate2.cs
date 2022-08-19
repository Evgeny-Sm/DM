using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class InitalCreate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumbersDrawings",
                table: "FileUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "FileUnits",
                type: "int",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TimeToDev",
                table: "FileUnits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Sections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sections", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUnits_SectionId",
                table: "FileUnits",
                column: "SectionId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUnits_Sections_SectionId",
                table: "FileUnits",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUnits_Sections_SectionId",
                table: "FileUnits");

            migrationBuilder.DropTable(
                name: "Sections");

            migrationBuilder.DropIndex(
                name: "IX_FileUnits_SectionId",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "NumbersDrawings",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "TimeToDev",
                table: "FileUnits");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class MakeReleases : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Releases",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectCode = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    IsLocked = table.Column<bool>(type: "bit", nullable: false),
                    IsRemoved = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Releases", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Releases_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Releases_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FileUnitRelease",
                columns: table => new
                {
                    FileUnitsId = table.Column<int>(type: "int", nullable: false),
                    ReleasesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileUnitRelease", x => new { x.FileUnitsId, x.ReleasesId });
                    table.ForeignKey(
                        name: "FK_FileUnitRelease_FileUnits_FileUnitsId",
                        column: x => x.FileUnitsId,
                        principalTable: "FileUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FileUnitRelease_Releases_ReleasesId",
                        column: x => x.ReleasesId,
                        principalTable: "Releases",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUnitRelease_ReleasesId",
                table: "FileUnitRelease",
                column: "ReleasesId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_PersonId",
                table: "Releases",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Releases_ProjectId",
                table: "Releases",
                column: "ProjectId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileUnitRelease");

            migrationBuilder.DropTable(
                name: "Releases");
        }
    }
}

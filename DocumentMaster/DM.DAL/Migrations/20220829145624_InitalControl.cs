using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class InitalControl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "FileUnits",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "FileUnits",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "TimeToCreate",
                table: "FileUnits",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Controls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileUnitId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false),
                    IsConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Controls", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Controls_FileUnits_FileUnitId",
                        column: x => x.FileUnitId,
                        principalTable: "FileUnits",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Controls_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileUnits_PersonId",
                table: "FileUnits",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Controls_FileUnitId",
                table: "Controls",
                column: "FileUnitId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Controls_PersonId",
                table: "Controls",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileUnits_Persons_PersonId",
                table: "FileUnits",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileUnits_Persons_PersonId",
                table: "FileUnits");

            migrationBuilder.DropTable(
                name: "Controls");

            migrationBuilder.DropIndex(
                name: "IX_FileUnits_PersonId",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "FileUnits");

            migrationBuilder.DropColumn(
                name: "TimeToCreate",
                table: "FileUnits");
        }
    }
}

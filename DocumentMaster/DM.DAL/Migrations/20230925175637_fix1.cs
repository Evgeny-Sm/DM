using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DM.DAL.Migrations
{
    public partial class fix1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsToDo_Questions_QuestionId",
                table: "QuestionsToDo");

            migrationBuilder.DropColumn(
                name: "QuestinId",
                table: "QuestionsToDo");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionsToDo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsToDo_Questions_QuestionId",
                table: "QuestionsToDo",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsToDo_Questions_QuestionId",
                table: "QuestionsToDo");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionId",
                table: "QuestionsToDo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "QuestinId",
                table: "QuestionsToDo",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsToDo_Questions_QuestionId",
                table: "QuestionsToDo",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }
    }
}

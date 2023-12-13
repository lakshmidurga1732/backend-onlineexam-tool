using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExam1.Migrations
{
    public partial class create9 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SubjectID",
                table: "TestStructures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TestStructures_SubjectID",
                table: "TestStructures",
                column: "SubjectID");

            migrationBuilder.AddForeignKey(
                name: "FK_TestStructures_Subjects_SubjectID",
                table: "TestStructures",
                column: "SubjectID",
                principalTable: "Subjects",
                principalColumn: "SubjectID",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TestStructures_Subjects_SubjectID",
                table: "TestStructures");

            migrationBuilder.DropIndex(
                name: "IX_TestStructures_SubjectID",
                table: "TestStructures");

            migrationBuilder.DropColumn(
                name: "SubjectID",
                table: "TestStructures");
        }
    }
}

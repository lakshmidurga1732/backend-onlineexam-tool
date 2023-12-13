using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineExam1.Migrations
{
    public partial class create10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UserResponses",
                type: "char(5)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_UserResponses_UserId",
                table: "UserResponses",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserResponses_Users_UserId",
                table: "UserResponses");

            migrationBuilder.DropIndex(
                name: "IX_UserResponses_UserId",
                table: "UserResponses");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserResponses");
        }
    }
}

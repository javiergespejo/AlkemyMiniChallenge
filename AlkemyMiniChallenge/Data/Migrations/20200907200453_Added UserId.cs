using Microsoft.EntityFrameworkCore.Migrations;

namespace AlkemyMiniChallenge.Data.Migrations
{
    public partial class AddedUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Operation",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Operation_UserId",
                table: "Operation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Operation_AspNetUsers_UserId",
                table: "Operation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Operation_AspNetUsers_UserId",
                table: "Operation");

            migrationBuilder.DropIndex(
                name: "IX_Operation_UserId",
                table: "Operation");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Operation");
        }
    }
}

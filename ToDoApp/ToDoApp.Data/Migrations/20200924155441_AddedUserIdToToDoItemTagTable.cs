using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Data.Migrations
{
    public partial class AddedUserIdToToDoItemTagTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "ToDoItemTag",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItemTag_UserId",
                table: "ToDoItemTag",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItemTag_AspNetUsers_UserId",
                table: "ToDoItemTag",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItemTag_AspNetUsers_UserId",
                table: "ToDoItemTag");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItemTag_UserId",
                table: "ToDoItemTag");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ToDoItemTag");
        }
    }
}

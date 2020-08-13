using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Migrations
{
    public partial class CreateToDoItemCategoryRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "ToDoItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ToDoItem_CategoryId",
                table: "ToDoItem",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ToDoItem_Category_CategoryId",
                table: "ToDoItem",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ToDoItem_Category_CategoryId",
                table: "ToDoItem");

            migrationBuilder.DropIndex(
                name: "IX_ToDoItem_CategoryId",
                table: "ToDoItem");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "ToDoItem");
        }
    }
}

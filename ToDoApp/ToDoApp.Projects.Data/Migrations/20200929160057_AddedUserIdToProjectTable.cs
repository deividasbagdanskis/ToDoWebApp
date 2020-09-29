using Microsoft.EntityFrameworkCore.Migrations;

namespace ToDoApp.Projects.Data.Migrations
{
    public partial class AddedUserIdToProjectTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Project",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Project");
        }
    }
}

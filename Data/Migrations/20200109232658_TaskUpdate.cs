using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class TaskUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskOwnerName",
                table: "Tasking",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaskTitle",
                table: "Tasking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskOwnerName",
                table: "Tasking");

            migrationBuilder.DropColumn(
                name: "TaskTitle",
                table: "Tasking");
        }
    }
}

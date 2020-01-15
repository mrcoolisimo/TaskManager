using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class TaskMemberOwner2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tasking");

            migrationBuilder.AddColumn<string>(
                name: "TaskOwner",
                table: "Tasking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskOwner",
                table: "Tasking");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tasking",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class TaskMemberOwner : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Tasking",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MemberID",
                table: "Tasking",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasking_MemberID",
                table: "Tasking",
                column: "MemberID");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasking_Member_MemberID",
                table: "Tasking",
                column: "MemberID",
                principalTable: "Member",
                principalColumn: "MemberID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasking_Member_MemberID",
                table: "Tasking");

            migrationBuilder.DropIndex(
                name: "IX_Tasking_MemberID",
                table: "Tasking");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Tasking");

            migrationBuilder.DropColumn(
                name: "MemberID",
                table: "Tasking");
        }
    }
}

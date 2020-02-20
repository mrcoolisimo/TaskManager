using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TaskManager.Data.Migrations
{
    public partial class calorie : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DayTotal",
                columns: table => new
                {
                    DayTotalID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TotalFats = table.Column<int>(nullable: false),
                    TotalCarbs = table.Column<int>(nullable: false),
                    TotalProtein = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    RealDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DayTotal", x => x.DayTotalID);
                });

            migrationBuilder.CreateTable(
                name: "Food",
                columns: table => new
                {
                    FoodID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Servings = table.Column<int>(nullable: false),
                    Fats = table.Column<int>(nullable: false),
                    Carbs = table.Column<int>(nullable: false),
                    Protein = table.Column<int>(nullable: false),
                    Date = table.Column<string>(nullable: true),
                    RealDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food", x => x.FoodID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DayTotal");

            migrationBuilder.DropTable(
                name: "Food");
        }
    }
}

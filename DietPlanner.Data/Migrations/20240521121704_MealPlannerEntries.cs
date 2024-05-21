using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DietPlanner.Migrations
{
    /// <inheritdoc />
    public partial class MealPlannerEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealTypes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DietEntries",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Food = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Calories = table.Column<int>(type: "INTEGER", nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValueSql: "GETDATE()"),
                    UserId = table.Column<string>(type: "TEXT", nullable: true),
                    MealTypeId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DietEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DietEntries_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DietEntries_MealTypes_MealTypeId",
                        column: x => x.MealTypeId,
                        principalTable: "MealTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "MealTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1L, "Breakfast" },
                    { 2L, "Lunch" },
                    { 3L, "Dinner" },
                    { 4L, "Snack" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DietEntries_MealTypeId",
                table: "DietEntries",
                column: "MealTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DietEntries_UserId",
                table: "DietEntries",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DietEntries");

            migrationBuilder.DropTable(
                name: "MealTypes");
        }
    }
}

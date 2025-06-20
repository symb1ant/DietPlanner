using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietPlanner.Migrations
{
    /// <inheritdoc />
    public partial class specifyidobjectsformeals : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietEntries_MealTypes_MealTypeId",
                table: "DietEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_DietEntries_MealTypes_MealTypeId",
                table: "DietEntries",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietEntries_MealTypes_MealTypeId",
                table: "DietEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_DietEntries_MealTypes_MealTypeId",
                table: "DietEntries",
                column: "MealTypeId",
                principalTable: "MealTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

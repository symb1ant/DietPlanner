﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DietPlanner.Migrations
{
    /// <inheritdoc />
    public partial class specifyidobjects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietEntries_AspNetUsers_UserId",
                table: "DietEntries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DietEntries",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_DietEntries_AspNetUsers_UserId",
                table: "DietEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DietEntries_AspNetUsers_UserId",
                table: "DietEntries");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "DietEntries",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_DietEntries_AspNetUsers_UserId",
                table: "DietEntries",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class SaleItems_UserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Users_Id",
                table: "SaleItems");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "SaleItems",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_SaleItems_UserId",
                table: "SaleItems",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Users_UserId",
                table: "SaleItems",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItems_Users_UserId",
                table: "SaleItems");

            migrationBuilder.DropIndex(
                name: "IX_SaleItems_UserId",
                table: "SaleItems");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "SaleItems");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItems_Users_Id",
                table: "SaleItems",
                column: "Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

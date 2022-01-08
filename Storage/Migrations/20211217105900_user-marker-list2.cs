using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class usermarkerlist2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "map_markers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_map_markers_UserId",
                table: "map_markers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_map_markers_AspNetUsers_UserId",
                table: "map_markers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_map_markers_AspNetUsers_UserId",
                table: "map_markers");

            migrationBuilder.DropIndex(
                name: "IX_map_markers_UserId",
                table: "map_markers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "map_markers");
        }
    }
}

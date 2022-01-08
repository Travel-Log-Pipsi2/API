using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class nolist : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_map_markers_AspNetUsers_UserId",
                table: "map_markers");

            migrationBuilder.DropIndex(
                name: "IX_map_markers_UserId",
                table: "map_markers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "map_markers",
                newName: "UserID");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserID",
                table: "map_markers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserID",
                table: "map_markers",
                newName: "UserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "map_markers",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

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
    }
}

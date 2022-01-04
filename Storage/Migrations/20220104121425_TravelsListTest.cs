using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class TravelsListTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_travels_MarkerId",
                table: "travels",
                column: "MarkerId");

            migrationBuilder.AddForeignKey(
                name: "FK_travels_map_markers_MarkerId",
                table: "travels",
                column: "MarkerId",
                principalTable: "map_markers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_travels_map_markers_MarkerId",
                table: "travels");

            migrationBuilder.DropIndex(
                name: "IX_travels_MarkerId",
                table: "travels");
        }
    }
}

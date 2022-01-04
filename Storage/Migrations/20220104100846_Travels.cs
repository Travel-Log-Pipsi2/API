using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class Travels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "map_markers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "map_markers");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "map_markers",
                newName: "Country");

            migrationBuilder.CreateTable(
                name: "travels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MarkerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_travels", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "travels");

            migrationBuilder.RenameColumn(
                name: "Country",
                table: "map_markers",
                newName: "Description");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "map_markers",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "map_markers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}

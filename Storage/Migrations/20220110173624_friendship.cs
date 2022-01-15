using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Storage.Migrations
{
    public partial class friendship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "frienships",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FromFriend = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToFriend = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Notification = table.Column<bool>(type: "bit", nullable: false),
                    IsAccepted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_frienships", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "frienships");
        }
    }
}

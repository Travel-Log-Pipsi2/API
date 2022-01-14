using Microsoft.EntityFrameworkCore.Migrations;

namespace Storage.Migrations
{
    public partial class connectionVariableRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastConnection",
                table: "connections",
                newName: "LastConnectionTime");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastConnectionTime",
                table: "connections",
                newName: "LastConnection");
        }
    }
}

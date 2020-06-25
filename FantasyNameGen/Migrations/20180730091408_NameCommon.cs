using Microsoft.EntityFrameworkCore.Migrations;

namespace FantasyNameGen.Migrations
{
    public partial class NameCommon : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Common",
                table: "Names",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Common",
                table: "Names");
        }
    }
}

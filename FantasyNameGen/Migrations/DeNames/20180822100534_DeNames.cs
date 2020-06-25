using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace FantasyNameGen.Migrations.DeNames
{
    public partial class DeNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DeNames",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    RomanName = table.Column<string>(nullable: true),
                    CyrilName = table.Column<string>(nullable: true),
                    Gender = table.Column<char>(nullable: false),
                    Variants = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Common = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeNames", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeNames");
        }
    }
}

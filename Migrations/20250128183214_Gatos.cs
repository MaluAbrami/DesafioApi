using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DesafioApi.Migrations
{
    public partial class Gatos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weights",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    imperial = table.Column<string>(nullable: true),
                    metric = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weights", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Racas",
                columns: table => new
                {
                    id = table.Column<string>(nullable: false),
                    weightid = table.Column<int>(nullable: true),
                    name = table.Column<string>(nullable: true),
                    cfa_url = table.Column<string>(nullable: true),
                    vetstreet_url = table.Column<string>(nullable: true),
                    vcahospitals_url = table.Column<string>(nullable: true),
                    temperament = table.Column<string>(nullable: true),
                    origin = table.Column<string>(nullable: true),
                    country_codes = table.Column<string>(nullable: true),
                    country_code = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    life_span = table.Column<string>(nullable: true),
                    indoor = table.Column<int>(nullable: false),
                    lap = table.Column<int>(nullable: false),
                    alt_names = table.Column<string>(nullable: true),
                    adaptability = table.Column<int>(nullable: false),
                    affection_level = table.Column<int>(nullable: false),
                    child_friendly = table.Column<int>(nullable: false),
                    dog_friendly = table.Column<int>(nullable: false),
                    energy_level = table.Column<int>(nullable: false),
                    grooming = table.Column<int>(nullable: false),
                    health_issues = table.Column<int>(nullable: false),
                    intelligence = table.Column<int>(nullable: false),
                    shedding_level = table.Column<int>(nullable: false),
                    social_needs = table.Column<int>(nullable: false),
                    stranger_friendly = table.Column<int>(nullable: false),
                    vocalisation = table.Column<int>(nullable: false),
                    experimental = table.Column<int>(nullable: false),
                    hairless = table.Column<int>(nullable: false),
                    natural = table.Column<int>(nullable: false),
                    rare = table.Column<int>(nullable: false),
                    rex = table.Column<int>(nullable: false),
                    suppressed_tail = table.Column<int>(nullable: false),
                    short_legs = table.Column<int>(nullable: false),
                    wikipedia_url = table.Column<string>(nullable: true),
                    hypoallergenic = table.Column<int>(nullable: false),
                    reference_image_id = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Racas", x => x.id);
                    table.ForeignKey(
                        name: "FK_Racas_Weights_weightid",
                        column: x => x.weightid,
                        principalTable: "Weights",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gatos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    NomeRaca = table.Column<string>(nullable: true),
                    Descricao = table.Column<string>(nullable: true),
                    UrlImagem = table.Column<string>(nullable: true),
                    racaid = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Gatos_Racas_racaid",
                        column: x => x.racaid,
                        principalTable: "Racas",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gatos_racaid",
                table: "Gatos",
                column: "racaid");

            migrationBuilder.CreateIndex(
                name: "IX_Racas_weightid",
                table: "Racas",
                column: "weightid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gatos");

            migrationBuilder.DropTable(
                name: "Racas");

            migrationBuilder.DropTable(
                name: "Weights");
        }
    }
}

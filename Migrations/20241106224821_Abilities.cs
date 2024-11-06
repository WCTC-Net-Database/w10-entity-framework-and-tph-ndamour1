using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace W9_assignment_template.Migrations
{
    public partial class Abilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Abilities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Taunt = table.Column<int>(type: "int", nullable: true),
                    Shove = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Abilities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CharacterAbilities",
                columns: table => new
                {
                    AbilitiesId = table.Column<int>(type: "int", nullable: false),
                    CharactersId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CharacterAbilities", x => new { x.AbilitiesId, x.CharactersId });
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Abilities_AbilitiesId",
                        column: x => x.AbilitiesId,
                        principalTable: "Abilities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CharacterAbilities_Characters_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "Characters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CharacterAbilities_CharactersId",
                table: "CharacterAbilities",
                column: "CharactersId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CharacterAbilities");

            migrationBuilder.DropTable(
                name: "Abilities");
        }
    }
}

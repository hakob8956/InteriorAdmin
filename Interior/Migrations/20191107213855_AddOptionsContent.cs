using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddOptionsContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OptionContents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    LanguageId = table.Column<int>(nullable: false),
                    InteriorId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionContents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OptionContents_Interiors_InteriorId",
                        column: x => x.InteriorId,
                        principalTable: "Interiors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OptionContents_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OptionContents_InteriorId",
                table: "OptionContents",
                column: "InteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_OptionContents_LanguageId",
                table: "OptionContents",
                column: "LanguageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OptionContents");
        }
    }
}

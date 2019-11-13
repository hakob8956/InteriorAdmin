using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddColumnInContent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentAttachments_Languages_LanguageId",
                table: "ContentAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ContentAttachments_LanguageId",
                table: "ContentAttachments");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "ContentAttachments");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_LanguageId",
                table: "Contents",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Languages_LanguageId",
                table: "Contents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Languages_LanguageId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_LanguageId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "ContentAttachments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_LanguageId",
                table: "ContentAttachments",
                column: "LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentAttachments_Languages_LanguageId",
                table: "ContentAttachments",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

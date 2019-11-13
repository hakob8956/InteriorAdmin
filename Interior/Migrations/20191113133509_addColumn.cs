using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class addColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "InteriorId",
                table: "ContentAttachments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_InteriorId",
                table: "ContentAttachments",
                column: "InteriorId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContentAttachments_Interiors_InteriorId",
                table: "ContentAttachments",
                column: "InteriorId",
                principalTable: "Interiors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContentAttachments_Interiors_InteriorId",
                table: "ContentAttachments");

            migrationBuilder.DropIndex(
                name: "IX_ContentAttachments_InteriorId",
                table: "ContentAttachments");

            migrationBuilder.DropColumn(
                name: "InteriorId",
                table: "ContentAttachments");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class DeleteRequired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "FilesAttachments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments",
                column: "BrandId",
                unique: true,
                filter: "[BrandId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "FilesAttachments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments",
                column: "BrandId",
                unique: true);
        }
    }
}

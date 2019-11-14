using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class addColumn1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "FilesAttachments");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "FilesAttachments",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "FileType",
                table: "Files",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments",
                column: "BrandId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments");

            migrationBuilder.DropColumn(
                name: "FileType",
                table: "Files");

            migrationBuilder.AlterColumn<int>(
                name: "BrandId",
                table: "FilesAttachments",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<byte>(
                name: "FileType",
                table: "FilesAttachments",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateIndex(
                name: "IX_FilesAttachments_BrandId",
                table: "FilesAttachments",
                column: "BrandId",
                unique: true,
                filter: "[BrandId] IS NOT NULL");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddFilesIdInInterior : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interiors_Files_FileId",
                table: "Interiors");

            migrationBuilder.DropIndex(
                name: "IX_Interiors_FileId",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "AndroidBundleHref",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "GlbHref",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "IosBundleHref",
                table: "Interiors");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Interiors",
                newName: "imageId");

            migrationBuilder.AddColumn<int>(
                name: "GlbFileId",
                table: "Interiors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fileAndroidId",
                table: "Interiors",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "fileIosId",
                table: "Interiors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interiors_GlbFileId",
                table: "Interiors",
                column: "GlbFileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interiors_Files_GlbFileId",
                table: "Interiors",
                column: "GlbFileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Interiors_Files_GlbFileId",
                table: "Interiors");

            migrationBuilder.DropIndex(
                name: "IX_Interiors_GlbFileId",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "GlbFileId",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "fileAndroidId",
                table: "Interiors");

            migrationBuilder.DropColumn(
                name: "fileIosId",
                table: "Interiors");

            migrationBuilder.RenameColumn(
                name: "imageId",
                table: "Interiors",
                newName: "FileId");

            migrationBuilder.AddColumn<string>(
                name: "AndroidBundleHref",
                table: "Interiors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlbHref",
                table: "Interiors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IosBundleHref",
                table: "Interiors",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interiors_FileId",
                table: "Interiors",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Interiors_Files_FileId",
                table: "Interiors",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}

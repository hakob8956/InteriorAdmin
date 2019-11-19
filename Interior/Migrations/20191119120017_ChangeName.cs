using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class ChangeName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeepLinkingUrl",
                table: "Interiors",
                newName: "BuyUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuyUrl",
                table: "Interiors",
                newName: "DeepLinkingUrl");
        }
    }
}

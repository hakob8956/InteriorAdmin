using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Interior.Migrations
{
    public partial class AddContentAttachment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Brands_BrandId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Languages_LanguageId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Recommendations_RecommendationId",
                table: "Contents");

            migrationBuilder.DropForeignKey(
                name: "FK_Contents_Shops_ShopId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_BrandId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_LanguageId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_RecommendationId",
                table: "Contents");

            migrationBuilder.DropIndex(
                name: "IX_Contents_ShopId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "LanguageId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "RecommendationId",
                table: "Contents");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Contents");

            migrationBuilder.AddColumn<byte>(
                name: "ContentType",
                table: "Contents",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.CreateTable(
                name: "ContentAttachments",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContentId = table.Column<int>(nullable: false),
                    LanguageId = table.Column<int>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    CategoryId = table.Column<int>(nullable: true),
                    BrandId = table.Column<int>(nullable: true),
                    ShopId = table.Column<int>(nullable: true),
                    RecommendationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContentAttachments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Brands_BrandId",
                        column: x => x.BrandId,
                        principalTable: "Brands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Contents_ContentId",
                        column: x => x.ContentId,
                        principalTable: "Contents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Recommendations_RecommendationId",
                        column: x => x.RecommendationId,
                        principalTable: "Recommendations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContentAttachments_Shops_ShopId",
                        column: x => x.ShopId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_BrandId",
                table: "ContentAttachments",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_CategoryId",
                table: "ContentAttachments",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_ContentId",
                table: "ContentAttachments",
                column: "ContentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_LanguageId",
                table: "ContentAttachments",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_RecommendationId",
                table: "ContentAttachments",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_ContentAttachments_ShopId",
                table: "ContentAttachments",
                column: "ShopId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ContentAttachments");

            migrationBuilder.DropColumn(
                name: "ContentType",
                table: "Contents");

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "LanguageId",
                table: "Contents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecommendationId",
                table: "Contents",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Contents",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Contents_BrandId",
                table: "Contents",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_CategoryId",
                table: "Contents",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_LanguageId",
                table: "Contents",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_RecommendationId",
                table: "Contents",
                column: "RecommendationId");

            migrationBuilder.CreateIndex(
                name: "IX_Contents_ShopId",
                table: "Contents",
                column: "ShopId");

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Brands_BrandId",
                table: "Contents",
                column: "BrandId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Categories_CategoryId",
                table: "Contents",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Languages_LanguageId",
                table: "Contents",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Recommendations_RecommendationId",
                table: "Contents",
                column: "RecommendationId",
                principalTable: "Recommendations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Contents_Shops_ShopId",
                table: "Contents",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

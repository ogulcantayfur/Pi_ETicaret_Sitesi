using Microsoft.EntityFrameworkCore.Migrations;

namespace Pi_ETicaret_Sitesi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kategoriler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kategoriler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Urunler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ad = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    resim = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    fiyat = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Urunler", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UrunKategoriler",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    urunId = table.Column<int>(type: "int", nullable: false),
                    kategoriId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunKategoriler", x => x.id);
                    table.ForeignKey(
                        name: "FK_UrunKategoriler_Kategoriler_kategoriId",
                        column: x => x.kategoriId,
                        principalTable: "Kategoriler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UrunKategoriler_Urunler_urunId",
                        column: x => x.urunId,
                        principalTable: "Urunler",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunKategoriler_kategoriId_urunId",
                table: "UrunKategoriler",
                columns: new[] { "kategoriId", "urunId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrunKategoriler_urunId",
                table: "UrunKategoriler",
                column: "urunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunKategoriler");

            migrationBuilder.DropTable(
                name: "Kategoriler");

            migrationBuilder.DropTable(
                name: "Urunler");
        }
    }
}

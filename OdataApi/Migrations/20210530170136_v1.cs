using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OdataApi.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Yapimcilar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(400)", maxLength: 400, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yapimcilar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Oyunlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Adi = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: false),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Puani = table.Column<double>(type: "float", nullable: false),
                    YapimciId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Oyunlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Oyunlar_Yapimcilar_YapimciId",
                        column: x => x.YapimciId,
                        principalTable: "Yapimcilar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Yorumlar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YorumcuAdi = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Aciklamasi = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Tarihi = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OyunId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Yorumlar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Yorumlar_Oyunlar_OyunId",
                        column: x => x.OyunId,
                        principalTable: "Oyunlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Oyunlar_YapimciId",
                table: "Oyunlar",
                column: "YapimciId");

            migrationBuilder.CreateIndex(
                name: "IX_Yorumlar_OyunId",
                table: "Yorumlar",
                column: "OyunId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Yorumlar");

            migrationBuilder.DropTable(
                name: "Oyunlar");

            migrationBuilder.DropTable(
                name: "Yapimcilar");
        }
    }
}

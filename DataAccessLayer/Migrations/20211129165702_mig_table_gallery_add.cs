using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_gallery_add : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Galleries",
                columns: table => new
                {
                    GalleryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GalleryTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GalleryContent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GalleryImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GalleryStatus = table.Column<bool>(type: "bit", nullable: false),
                    GalleryCreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GalleryUpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Galleries", x => x.GalleryID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Galleries");
        }
    }
}

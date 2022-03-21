using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_table_add_galleryimage_rename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GalleryImage",
                table: "GalleryImage");

            migrationBuilder.RenameTable(
                name: "GalleryImage",
                newName: "GalleryImages");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalleryImages",
                table: "GalleryImages",
                column: "GalleryImageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GalleryImages",
                table: "GalleryImages");

            migrationBuilder.RenameTable(
                name: "GalleryImages",
                newName: "GalleryImage");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GalleryImage",
                table: "GalleryImage",
                column: "GalleryImageId");
        }
    }
}

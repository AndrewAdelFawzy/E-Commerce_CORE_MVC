using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.Infrastructure.Migrations
{
    public partial class AlterBrandTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Brands",
                newName: "ProfileImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BackgroundImageUrl",
                table: "Brands",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BackgroundImageUrl",
                table: "Brands");

            migrationBuilder.RenameColumn(
                name: "ProfileImageUrl",
                table: "Brands",
                newName: "ImageUrl");
        }
    }
}

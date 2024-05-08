using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalyearproject.Migrations
{
    /// <inheritdoc />
    public partial class v13 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "expire_date",
                table: "Posts",
                newName: "expired_date");

            migrationBuilder.AddColumn<string>(
                name: "experience",
                table: "Posts",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "experience",
                table: "Posts");

            migrationBuilder.RenameColumn(
                name: "expired_date",
                table: "Posts",
                newName: "expire_date");
        }
    }
}

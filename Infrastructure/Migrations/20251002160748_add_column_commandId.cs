using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_column_commandId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KeySearch",
                table: "RequestSearchHisory");

            migrationBuilder.DropColumn(
                name: "Keyword",
                table: "RequestSearchHisory");

            migrationBuilder.DropColumn(
                name: "TypeSearch",
                table: "RequestSearchHisory");

            migrationBuilder.AddColumn<int>(
                name: "CommandId",
                table: "RequestSearchHisory",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommandId",
                table: "RequestSearchHisory");

            migrationBuilder.AddColumn<string>(
                name: "KeySearch",
                table: "RequestSearchHisory",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "Keyword",
                table: "RequestSearchHisory",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "TypeSearch",
                table: "RequestSearchHisory",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}

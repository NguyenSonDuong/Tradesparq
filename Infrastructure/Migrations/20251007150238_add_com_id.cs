using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_com_id : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultCount",
                table: "RequestSearchHisory");

            migrationBuilder.DropColumn(
                name: "SearchDate",
                table: "RequestSearchHisory");

            migrationBuilder.AddColumn<string>(
                name: "ComId",
                table: "Command",
                type: "longtext",
                nullable: false,
                collation: "utf8mb4_unicode_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ComId",
                table: "Command");

            migrationBuilder.AddColumn<int>(
                name: "ResultCount",
                table: "RequestSearchHisory",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SearchDate",
                table: "RequestSearchHisory",
                type: "datetime(6)",
                nullable: true);
        }
    }
}

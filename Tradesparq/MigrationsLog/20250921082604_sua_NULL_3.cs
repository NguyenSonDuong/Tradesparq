using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradesparq.Migrations
{
    /// <inheritdoc />
    public partial class sua_NULL_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "uuid",
                table: "Companies",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Var",
                table: "Companies",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Uname",
                table: "Companies",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "Companies",
                type: "longtext",
                nullable: true,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "varchar(64)",
                oldMaxLength: 64)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "uuid",
                keyValue: null,
                column: "uuid",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "uuid",
                table: "Companies",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Var",
                keyValue: null,
                column: "Var",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Var",
                table: "Companies",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Uname",
                keyValue: null,
                column: "Uname",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Uname",
                table: "Companies",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");

            migrationBuilder.UpdateData(
                table: "Companies",
                keyColumn: "Uid",
                keyValue: null,
                column: "Uid",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Uid",
                table: "Companies",
                type: "varchar(64)",
                maxLength: 64,
                nullable: false,
                collation: "utf8mb4_unicode_ci",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_unicode_ci");
        }
    }
}

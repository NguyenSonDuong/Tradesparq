using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tradesparq.Migrations
{
    /// <inheritdoc />
    public partial class xoalienket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Companies_CompanyId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Faxs_Companies_CompanyId",
                table: "Faxs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Companies_CompanyId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_Companies_CompanyId",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PostalCodes_CompanyId",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_CompanyId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Faxs_CompanyId",
                table: "Faxs");

            migrationBuilder.DropIndex(
                name: "IX_Emails_CompanyId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities");

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "PostalCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "PhoneNumbers",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "Faxs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "Emails",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompaniesId",
                table: "Cities",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_CompaniesId",
                table: "PostalCodes",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_CompaniesId",
                table: "PhoneNumbers",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Faxs_CompaniesId",
                table: "Faxs",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_CompaniesId",
                table: "Emails",
                column: "CompaniesId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CompaniesId",
                table: "Cities",
                column: "CompaniesId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_CompaniesId",
                table: "Cities",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Companies_CompaniesId",
                table: "Emails",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Faxs_Companies_CompaniesId",
                table: "Faxs",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Companies_CompaniesId",
                table: "PhoneNumbers",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_Companies_CompaniesId",
                table: "PostalCodes",
                column: "CompaniesId",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Companies_CompaniesId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_Emails_Companies_CompaniesId",
                table: "Emails");

            migrationBuilder.DropForeignKey(
                name: "FK_Faxs_Companies_CompaniesId",
                table: "Faxs");

            migrationBuilder.DropForeignKey(
                name: "FK_PhoneNumbers_Companies_CompaniesId",
                table: "PhoneNumbers");

            migrationBuilder.DropForeignKey(
                name: "FK_PostalCodes_Companies_CompaniesId",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PostalCodes_CompaniesId",
                table: "PostalCodes");

            migrationBuilder.DropIndex(
                name: "IX_PhoneNumbers_CompaniesId",
                table: "PhoneNumbers");

            migrationBuilder.DropIndex(
                name: "IX_Faxs_CompaniesId",
                table: "Faxs");

            migrationBuilder.DropIndex(
                name: "IX_Emails_CompaniesId",
                table: "Emails");

            migrationBuilder.DropIndex(
                name: "IX_Cities_CompaniesId",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "PostalCodes");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "PhoneNumbers");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "Faxs");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "Emails");

            migrationBuilder.DropColumn(
                name: "CompaniesId",
                table: "Cities");

            migrationBuilder.CreateIndex(
                name: "IX_PostalCodes_CompanyId",
                table: "PostalCodes",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_PhoneNumbers_CompanyId",
                table: "PhoneNumbers",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Faxs_CompanyId",
                table: "Faxs",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Emails_CompanyId",
                table: "Emails",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_CompanyId",
                table: "Cities",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Companies_CompanyId",
                table: "Cities",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Emails_Companies_CompanyId",
                table: "Emails",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Faxs_Companies_CompanyId",
                table: "Faxs",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PhoneNumbers_Companies_CompanyId",
                table: "PhoneNumbers",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_PostalCodes_Companies_CompanyId",
                table: "PostalCodes",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

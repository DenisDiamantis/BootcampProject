using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalProject.Back.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Certificates",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.AddColumn<int>(
                name: "CertificateId",
                table: "UserCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                table: "UserCertificates",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "UserCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Grade",
                table: "UserCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "UserCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "UserCertificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "UserCertificates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Certificates",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Photo",
                table: "Certificates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 4, 15, 34, 25, 156, DateTimeKind.Local).AddTicks(5674));

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificates_CertificateId",
                table: "UserCertificates",
                column: "CertificateId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCertificates_UserId",
                table: "UserCertificates",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates",
                column: "CertificateId",
                principalTable: "Certificates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Certificates_CertificateId",
                table: "UserCertificates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserCertificates_Users_UserId",
                table: "UserCertificates");

            migrationBuilder.DropIndex(
                name: "IX_UserCertificates_CertificateId",
                table: "UserCertificates");

            migrationBuilder.DropIndex(
                name: "IX_UserCertificates_UserId",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "CertificateId",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Created",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Grade",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UserCertificates");

            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Certificates");

            migrationBuilder.DropColumn(
                name: "Photo",
                table: "Certificates");

            migrationBuilder.InsertData(
                table: "Certificates",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Microsoft Certified: Azure Fundamentals" },
                    { 2, "Microsoft Certified: Azure Administrator Associate" }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                column: "CreatedAt",
                value: new DateTime(2024, 1, 4, 13, 57, 14, 104, DateTimeKind.Local).AddTicks(7891));
        }
    }
}

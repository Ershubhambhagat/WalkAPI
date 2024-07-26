using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk_API.Migrations.NZWalkAuthDb
{
    /// <inheritdoc />
    public partial class updateidforrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "readerRoleID");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "writerRoleID");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212", "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212", "Reader", "READER" },
                    { "AD334324-4BE9-47F5-B8E8-98541B6FB2AE", "AD334324-4BE9-47F5-B8E8-98541B6FB2AE", "Writer", "WRITER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "AD334324-4BE9-47F5-B8E8-98541B6FB2AE");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "readerRoleID", "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212", "Reader", "READER" },
                    { "writerRoleID", "AD334324-4BE9-47F5-B8E8-98541B6FB2AE", "Writer", "WRITER" }
                });
        }
    }
}

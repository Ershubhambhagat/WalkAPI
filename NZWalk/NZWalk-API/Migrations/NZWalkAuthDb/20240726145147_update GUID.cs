using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalk_API.Migrations.NZWalkAuthDb
{
    /// <inheritdoc />
    public partial class updateGUID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "readerRoleID",
                column: "ConcurrencyStamp",
                value: "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "writerRoleID",
                column: "ConcurrencyStamp",
                value: "AD334324-4BE9-47F5-B8E8-98541B6FB2AE");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "readerRoleID",
                column: "ConcurrencyStamp",
                value: "e7b1286e-ad2e-45da-8130-a033d302ab65");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "writerRoleID",
                column: "ConcurrencyStamp",
                value: "8abb16f0-da36-487d-8505-5585700fddf9");
        }
    }
}

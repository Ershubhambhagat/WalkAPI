using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NZWalk_API.Migrations.NZWalkAuthDb
{
    /// <inheritdoc />
    public partial class updateroleguid : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "2aaf722c-d5b9-4a53-a55f-b5f4eaa130eb", "2aaf722c-d5b9-4a53-a55f-b5f4eaa130eb", "Writer", "WRITER" },
                    { "d859333c-eea5-48d6-ad98-9c968fb75491", "d859333c-eea5-48d6-ad98-9c968fb75491", "Reader", "READER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2aaf722c-d5b9-4a53-a55f-b5f4eaa130eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d859333c-eea5-48d6-ad98-9c968fb75491");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212", "A036ED82-C7E0-4E09-84EB-8BE9FBFD7212", "Reader", "READER" },
                    { "AD334324-4BE9-47F5-B8E8-98541B6FB2AE", "AD334324-4BE9-47F5-B8E8-98541B6FB2AE", "Writer", "WRITER" }
                });
        }
    }
}

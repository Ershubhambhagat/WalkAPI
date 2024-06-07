using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NZWalk_API.Migrations
{
    /// <inheritdoc />
    public partial class RenametoRegionImageUrltoWalkImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RegionImageUrl",
                table: "Walks",
                newName: "WalkImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WalkImageUrl",
                table: "Walks",
                newName: "RegionImageUrl");
        }
    }
}

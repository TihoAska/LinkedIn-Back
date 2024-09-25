using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIn.Migrations
{
    /// <inheritdoc />
    public partial class AddedBannerImageToProfileDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BannerImage",
                table: "ProfileDetails",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BannerImage",
                table: "ProfileDetails");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LinkedIn.Migrations
{
    /// <inheritdoc />
    public partial class ChangedIsRemoteToLocationType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemote",
                table: "Experiences");

            migrationBuilder.AddColumn<string>(
                name: "LocationType",
                table: "Experiences",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocationType",
                table: "Experiences");

            migrationBuilder.AddColumn<bool>(
                name: "IsRemote",
                table: "Experiences",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class addednotify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllowedToNotify",
                table: "UserSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAllowedToNotify",
                table: "UserSettings");
        }
    }
}

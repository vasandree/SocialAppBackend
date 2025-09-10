using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class newnotifications : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsAllowedToNotify",
                table: "UserSettings",
                newName: "TaskNotifications");

            migrationBuilder.AddColumn<bool>(
                name: "EventNotifications",
                table: "UserSettings",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EventNotifications",
                table: "UserSettings");

            migrationBuilder.RenameColumn(
                name: "TaskNotifications",
                table: "UserSettings",
                newName: "IsAllowedToNotify");
        }
    }
}

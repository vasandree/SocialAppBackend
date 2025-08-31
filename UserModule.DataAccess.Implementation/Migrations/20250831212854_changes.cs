using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ChatInstance",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Language",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "Theme",
                table: "UserSettings");

            migrationBuilder.DropColumn(
                name: "AllowsWriteToPm",
                table: "TelegramAccounts");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "TelegramAccounts");

            migrationBuilder.DropColumn(
                name: "LanguageCode",
                table: "TelegramAccounts");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "TelegramAccounts");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "TelegramAccounts");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ChatInstance",
                table: "UserSettings",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Language",
                table: "UserSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Theme",
                table: "UserSettings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "AllowsWriteToPm",
                table: "TelegramAccounts",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "TelegramAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LanguageCode",
                table: "TelegramAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "TelegramAccounts",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "TelegramAccounts",
                type: "text",
                nullable: true);
        }
    }
}

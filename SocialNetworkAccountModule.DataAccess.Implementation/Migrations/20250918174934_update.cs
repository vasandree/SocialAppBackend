using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkAccountModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "UsersAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "PersonsAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "UsersAccounts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "PersonsAccounts");
        }
    }
}

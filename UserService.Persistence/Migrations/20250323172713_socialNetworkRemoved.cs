using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserService.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class socialNetworkRemoved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworkAccounts_Users_UserId",
                table: "SocialNetworkAccounts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworkAccounts",
                table: "SocialNetworkAccounts");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "SocialNetworkAccounts");

            migrationBuilder.RenameTable(
                name: "SocialNetworkAccounts",
                newName: "SocialNetworkAccount");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworkAccounts_UserId",
                table: "SocialNetworkAccount",
                newName: "IX_SocialNetworkAccount_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworkAccount",
                table: "SocialNetworkAccount",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworkAccount_Users_UserId",
                table: "SocialNetworkAccount",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SocialNetworkAccount_Users_UserId",
                table: "SocialNetworkAccount");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SocialNetworkAccount",
                table: "SocialNetworkAccount");

            migrationBuilder.RenameTable(
                name: "SocialNetworkAccount",
                newName: "SocialNetworkAccounts");

            migrationBuilder.RenameIndex(
                name: "IX_SocialNetworkAccount_UserId",
                table: "SocialNetworkAccounts",
                newName: "IX_SocialNetworkAccounts_UserId");

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "SocialNetworkAccounts",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SocialNetworkAccounts",
                table: "SocialNetworkAccounts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SocialNetworkAccounts_Users_UserId",
                table: "SocialNetworkAccounts",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

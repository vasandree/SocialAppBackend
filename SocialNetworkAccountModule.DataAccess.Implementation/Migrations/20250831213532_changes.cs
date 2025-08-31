using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialNetworkAccountModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Username",
                table: "UsersAccounts");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "PersonsAccounts");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatorId",
                table: "UsersAccounts",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "UsersAccounts");

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
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialNetworkAccounts.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PersonsAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PersonsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonsAccounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialNetworkUrls",
                columns: table => new
                {
                    Type = table.Column<int>(type: "integer", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialNetworkUrls", x => x.Type);
                });

            migrationBuilder.CreateTable(
                name: "UsersAccounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersAccounts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "SocialNetworkUrls",
                columns: new[] { "Type", "Url" },
                values: new object[,]
                {
                    { 0, "https://www.facebook.com/" },
                    { 1, "https://x.com/" },
                    { 2, "https://www.linkedin.com/in/" },
                    { 3, "https://www.instagram.com/" },
                    { 4, "https://www.youtube.com/" },
                    { 5, "https://www.pinterest.com/" },
                    { 6, "https://www.snapchat.com/add/" },
                    { 7, "https://www.tiktok.com/@" },
                    { 8, "https://www.reddit.com/user/" },
                    { 9, "https://wa.me/" },
                    { 10, "https://github.com/" },
                    { 11, "https://t.me/" },
                    { 12, "https://www.twitch.tv/" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PersonsAccounts");

            migrationBuilder.DropTable(
                name: "SocialNetworkUrls");

            migrationBuilder.DropTable(
                name: "UsersAccounts");
        }
    }
}

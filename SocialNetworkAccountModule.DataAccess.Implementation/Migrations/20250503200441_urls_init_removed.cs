using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SocialNetworkAccountModule.DataAccess.Implementation.Migrations
{
    /// <inheritdoc />
    public partial class urls_init_removed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 0);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "SocialNetworkUrls",
                keyColumn: "Type",
                keyValue: 12);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

using System.Security.Cryptography;
using System.Text;
using System.Web;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using User.Contracts.Dtos.Telegram;
using User.Contracts.Helpers;
using User.Domain.Enums;

namespace User.Application.Helpers
{
    public class TelegramHelper : ITelegramHelper
    {
        private readonly string? _botToken;

        public TelegramHelper(IConfiguration configuration)
        {
            _botToken = configuration.GetSection("TelegramBotApiKey").Value;
        }

        public TelegramInitData ParseInitData(string? initData)
        {
            var result = new TelegramInitData();

            var queryParams = ParseQueryString(initData);

            if (queryParams.TryGetValue("user", out var userJson) && !string.IsNullOrEmpty(userJson))
            {
                try
                {
                    result.User = JsonConvert.DeserializeObject<TelegramUser>(HttpUtility.UrlDecode(userJson));
                }
                catch (JsonSerializationException)
                {
                    result.User = null;
                }
            }

            result.ChatInstance = queryParams.ContainsKey("chat_instance") ? queryParams["chat_instance"] : null;
            result.ChatType = queryParams.ContainsKey("chat_type") ? queryParams["chat_type"] : null;

            if (long.TryParse(queryParams.GetValueOrDefault("auth_date"), out var authDate))
            {
                result.AuthDate = authDate;
            }

            result.Signature = queryParams.ContainsKey("signature") ? queryParams["signature"] : null;
            result.Hash = queryParams.ContainsKey("hash") ? queryParams["hash"] : null;

            return result;
        }

        public bool ValidateInitData(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return false;
            }

            var queryParams = ParseQueryString(data);

            if (!queryParams.TryGetValue("hash", out var receivedHash) || string.IsNullOrEmpty(receivedHash))
            {
                return false;
            }

            queryParams.Remove("hash");

            var dataCheckString = ConstructDataCheckString(queryParams);

            var secretKey = GenerateSecretKey(_botToken);

            var computedHash = ComputeHmacSha256(dataCheckString, secretKey);

            if (!string.Equals(computedHash, receivedHash, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            if (long.TryParse(queryParams.GetValueOrDefault("auth_date"), out var authDate))
            {
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                const int maxAgeInSeconds = 600; 
                if (currentTime - authDate > maxAgeInSeconds)
                {
                    return false; 
                }
            }

            return true;
        }

        public Language GetLanguage(string code)
        {
            return code == "ru" ? Language.Ru : Language.En;
        }

        private static Dictionary<string, string?> ParseQueryString(string? queryString)
        {
            var result = new Dictionary<string, string?>();

            if (string.IsNullOrEmpty(queryString))
            {
                return result;
            }

            foreach (var pair in queryString.Split('&'))
            {
                var parts = pair.Split(new[] { '=' }, 2);

                if (parts.Length == 2 && !string.IsNullOrEmpty(parts[0]))
                {
                    result[HttpUtility.UrlDecode(parts[0])] = HttpUtility.UrlDecode(parts[1]);
                }
                else if (parts.Length == 1 && !string.IsNullOrEmpty(parts[0]))
                {
                    result[HttpUtility.UrlDecode(parts[0])] = string.Empty;
                }
            }

            return result;
        }

        private static string ConstructDataCheckString(Dictionary<string, string?> queryParams)
        {
            var sb = new StringBuilder();

            foreach (var kvp in queryParams.OrderBy(k => k.Key))
            {
                sb.Append($"{kvp.Key}={kvp.Value}\n");
            }

            return sb.ToString().TrimEnd('\n');
        }

        private static byte[] GenerateSecretKey(string? botToken)
        {
            using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes("WebAppData"));
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(botToken));
        }

        private static string ComputeHmacSha256(string data, byte[] key)
        {
            using var hmac = new HMACSHA256(key);
            var hashBytes = hmac.ComputeHash(Encoding.UTF8.GetBytes(data));
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
        }
    }
}
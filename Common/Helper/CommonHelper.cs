using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class CommonHelper
    {
        public static string SpTransID()
        {
            return BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0).ToString().Substring(0, 16);
        }
        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }
            url = System.Uri.UnescapeDataString(url);

            var isValid = Uri.TryCreate(url, UriKind.RelativeOrAbsolute, out Uri uriResult);

            if (!isValid || uriResult is null)
            {
                return false;
            }

            return (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
        public static async Task<bool> SendAlertToTelegram(string message, string botId, string chatId)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.telegram.org/bot{botId}/sendMessage");
                    var collection = new List<KeyValuePair<string, string>>
                {
                    new("chat_id", chatId),
                    new("text", message),
                    new("parse_mode", "markdown")
                };
                    var content = new FormUrlEncodedContent(collection);
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return false;
        }

        public static string? ExtractWebhookPaymentUrls(string url, string paramName)
        {
            // Split the URL into parts, handling multiple `:` properly
            var urlParts = url.Contains("#")? url.Split('#'): url.Split('?');
            var queryParams = new List<string>();

            // Add each part after the first one (ignoring the base URL)
            for (int i = 1; i < urlParts.Length; i++)
            {
                queryParams.AddRange(urlParts[i].Split(new[] { '&', '?' }, StringSplitOptions.RemoveEmptyEntries));
            }
            // Loop through each parameter
            foreach (var param in queryParams)
            {
                var keyValue = param.Split(new char[] { '=' }, 2);
                var key = keyValue[0];
                var value = keyValue.Length > 1 ? keyValue[1] : "";


                int lastIndex = value.LastIndexOf('/');
                if (lastIndex != -1)
                {
                    // Check if '/' is at the end of the string
                    if (lastIndex == value.Length - 1)
                    {
                        value = value.Substring(0, lastIndex);
                    }
                }

                if (paramName.ToLower() == key.ToLower())
                {
                    return System.Uri.UnescapeDataString(value);
                }

            }

            return null;
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class UrlMaker
    {
        public static string? GetNotifyUrl(dynamic obj)
        {
            var url = obj.NotifyUrl;
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }
            var variables = ExtractValues(url);
            if (variables == null)
            {
                return null;
            }
            url = MakeUrl(url, variables, obj);
            return url;
        }

        private static string MakeUrl(string url, IEnumerable<string> variables, dynamic obj)
        {
            foreach (var variable in variables)
            {
                var val = GetPorpertyVal(obj, variable);

                string pattern = $@"\{{{variable}\}}";
                Regex regex = new Regex(pattern);
                url = regex.Replace(url, val);
            }
            return url;
        }

        private static string? GetPorpertyVal(dynamic dynamicObject, string propertyName)
        {
            if (HasProperty(dynamicObject, propertyName))
            {
                object propertyValue = GetPropertyValue(dynamicObject, propertyName);
                return propertyValue?.ToString();
            }
            else
            {
                return null;
            }
        }
        static bool HasProperty(dynamic obj, string propertyName)
        {
            return ((IDictionary<string, object>)obj).ContainsKey(propertyName);
        }

        static object GetPropertyValue(dynamic obj, string propertyName)
        {
            return ((IDictionary<string, object>)obj)[propertyName];
        }
        static List<string> ExtractValues(string input)
        {
            string pattern = @"\{([^}]+)\}";
            List<string> values = new List<string>();

            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(input);

            foreach (Match match in matches)
            {
                values.Add(match.Groups[1].Value);
            }

            return values;
        }
        public static string CompressUrl(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                byte[] buffer = Encoding.UTF8.GetBytes(url);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                    {
                        gzipStream.Write(buffer, 0, buffer.Length);
                    }

                    memoryStream.Position = 0;

                    byte[] compressedData = new byte[memoryStream.Length];
                    memoryStream.Read(compressedData, 0, compressedData.Length);

                    return Convert.ToBase64String(compressedData);
                }
            }
            return "";
        }

        public static string DecompressUrl(string compressedUrl)
        {
            if (!string.IsNullOrEmpty(compressedUrl))
            {
                //compressedUrl = compressedUrl.Replace("/", "");
                compressedUrl = compressedUrl.Replace(" ", "+");
                byte[] compressedData = Convert.FromBase64String(compressedUrl);

                using (MemoryStream memoryStream = new MemoryStream(compressedData))
                {
                    using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                    {
                        using (StreamReader streamReader = new StreamReader(gzipStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
            return "";
        }
        public static WebHookData getUrl(string url)
        {
            // Base64 encoded strings from the URL
            Regex regex = new Regex(@"psr=([^:]+):pfr=([^:]+)");


            // Match the regex pattern in the input string
            Match match = regex.Match(url);

            // Check if the regex pattern matched
            if (match.Success)
            {
                // Extract the Base64 encoded strings for psr and pfr
                string psrBase64 = match.Groups[1].Value;
                string pfrBase64 = match.Groups[2].Value;


                // Create and populate WebHookData object
                WebHookData data = new WebHookData
                {
                    psr = DecompressUrl(psrBase64),
                    pfr = DecompressUrl(pfrBase64)
                };
                return data;
            }
            return null;
        }

        public static WebHookData getUrlV2(string url)
        {
            const string psrPattern = @"psr=(?<psrValue>[^:]+)";
            const string pfrPattern = @"pfr=(?<pfrValue>[^:]+)";

            Match psrMatch = Regex.Match(url, psrPattern);
            Match pfrMatch = Regex.Match(url, pfrPattern);

            string psrBase64 = psrMatch.Success ? psrMatch.Groups["psrValue"].Value : "";
            string pfrBase64 = pfrMatch.Success ? pfrMatch.Groups["pfrValue"].Value : "";

            if (psrMatch.Success || pfrMatch.Success)
            {
                return new WebHookData
                {
                    psr = DecompressUrl(psrBase64),
                    pfr = DecompressUrl(pfrBase64)
                };
            }

            return null;
        }
    }
    public class WebHookData
    {
        public string psr { get; set; }
        public string pfr { get; set; }
    }
    public record Header(string key, string value);
}

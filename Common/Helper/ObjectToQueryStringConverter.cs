using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Web;

namespace Common.Helper
{
    public static class ObjectToQueryStringConverter
    {
        public static string Convert(object obj)
        {
            var properties = obj.GetType().GetProperties();
            var encodedPairs = properties.Select(property =>
            {
                var queryKeyAttribute = property.GetCustomAttribute(typeof(JsonProperty));
                var key = queryKeyAttribute != null ? queryKeyAttribute.ToString() : property.Name;
                //var key = HttpUtility.UrlEncode(ToCamelCase(property.Name));
                var value = HttpUtility.UrlEncode(property.GetValue(obj)?.ToString() ?? "");
                return $"{key}={value}";
            });

            return string.Join("&", encodedPairs);
        }
        static string ToCamelCase(string input)
        {
            if (string.IsNullOrEmpty(input) || !char.IsUpper(input[0]))
            {
                return input;
            }

            char[] chars = input.ToCharArray();
            for (int i = 0; i < chars.Length; i++)
            {
                if (i == 1 && !char.IsUpper(chars[i]))
                {
                    break;
                }

                bool hasNext = (i + 1 < chars.Length);
                if (i > 0 && hasNext && !char.IsUpper(chars[i + 1]))
                {
                    chars[i] = char.ToLower(chars[i]);
                    break;
                }

                chars[i] = char.ToLowerInvariant(chars[i]);
            }

            return new string(chars);
        }
    }
}

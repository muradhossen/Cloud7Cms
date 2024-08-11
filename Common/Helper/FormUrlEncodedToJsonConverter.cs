using System.Text.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public class FormUrlEncodedToJsonConverter
    {
        public static async Task<string> ConvertAsync(FormUrlEncodedContent formUrlEncodedContent)
        {
            var formData = await formUrlEncodedContent.ReadAsStringAsync();
            var formDataPairs = formData.Split('&')
                                       .Select(pair => pair.Split('='))
                                       .ToDictionary(parts => Uri.UnescapeDataString(parts[0]),
                                                     parts => Uri.UnescapeDataString(parts[1]));

            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };
            return JsonSerializer.Serialize(formDataPairs, options);
        }
    }
}

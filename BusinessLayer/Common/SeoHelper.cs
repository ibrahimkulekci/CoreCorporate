using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Common
{
    public static class SeoHelper
    {
        public static string ConvertToValidUrl(string stringToConvert)
        {
            string result = string.Empty;
            result = stringToConvert.ToLower().Replace("ç", "c").Replace("ğ", "g")
                .Replace("ı", "i").Replace("ö", "o").Replace("ş", "s")
                .Replace("ü", "u").Replace("  ", " ").Replace(" ", "-")
                .Replace("'", "").Replace("\"", "").Replace(":", "")
                .Replace(",", "").Replace(";", "").Replace("!", "").Replace("?", "")
                .Replace("(", "").Replace(")", "").Replace("[", "").Replace("]", "")
                .Replace("{", "").Replace("}", "").Replace("%", "").Replace("/", "")
                .Replace("+", "").Replace("&", "").Replace("=", "").Replace("_", "")
                .Replace(".", "").Replace("#", "").Replace("$", "");

            return result;
        }
    }
}

using System;
using System.Linq;
using System.Text;

namespace Wps.Client.Utils
{
    public static class StringHelper
    {

        public static string CustomFormatJoin<T>(string format, IFormatProvider formatProvider, string separator, params T[] elements) where T : IFormattable
        {
            var stringBuilder = new StringBuilder();
            if (elements.Length > 2)
            {
                foreach (var el in elements.Take(elements.Length - 1))
                {
                    stringBuilder.Append(el.ToString(format, formatProvider));
                    stringBuilder.Append(separator);
                }
            }

            stringBuilder.Append(elements.Last().ToString(format, formatProvider));

            return stringBuilder.ToString();
        }

        public static string CustomFormatJoin<T>(IFormatProvider formatProvider, string separator, params T[] elements) where T : IFormattable
        {
            return CustomFormatJoin(string.Empty, formatProvider, separator, elements);
        }

    }
}

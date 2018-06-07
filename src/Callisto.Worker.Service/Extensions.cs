using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Callisto.Worker.Service
{
    /// <summary>
    /// Defines the <see cref="Extensions" />
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// The EncodedValue
        /// </summary>
        /// <param name="value">The <see cref="KeyValuePair{string, string}"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string EncodedValue(this Dictionary<string, string> dictionary, string key)
        {
            var item = dictionary.FirstOrDefault(c => c.Key == key);
            var result = item.Value ?? string.Empty;
            return HttpUtility.UrlEncode(result);
        }
    }
}

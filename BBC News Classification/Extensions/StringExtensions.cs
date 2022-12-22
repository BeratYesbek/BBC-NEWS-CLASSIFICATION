using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BBC_News_Classification.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// Clear punctuation in string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string StripPunctuation(this string s)
        {
            var sb = new StringBuilder();
            foreach (char c in s)
            {
                if (!char.IsPunctuation(c))
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}

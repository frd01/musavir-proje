using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace Tacmin.Core.Extensions
{
    public static class StringExtensions
    {
        public static MvcHtmlString Raw(this string str)
        {
            return MvcHtmlString.Create(str);
        }

        //HttpUtility.JavascriptStringEncode benzeri
        public static string JavascriptStringEncode(this string src, bool forsinglequote = true)
        {
            var quote = '\'';

            if (!forsinglequote)
                quote = '"';

            var jsstr = JsonConvert.ToString(src.Coalesce(), quote);

            return jsstr.Substring(1, jsstr.Length - 2);
        }

        public static string EncodeBase64(this string src)
        {
            if (src == null)
                return null;

            var EncryptAsBytes = Encoding.UTF8.GetBytes(src);

            return Convert.ToBase64String(EncryptAsBytes);
        }

        public static string DecodeBase64(this string src)
        {
            if (src == null)
                return null;

            var DecryptAsBytes = Convert.FromBase64String(src);

            return Encoding.UTF8.GetString(DecryptAsBytes);
        }

        public static string SafeSubstring(this string src, int length)
        {
            src = src.Coalesce();
            return src.Substring(0, src.Length > length && length > 0 ? length : src.Length);
        }

        public static string SafeSubstring(this string src, int startIndex, int length)
        {
            if (src == null)
                return null;

            if (startIndex > 0)
                return src.Substring(startIndex, (src.Length - startIndex) > length ? length : (src.Length - startIndex));

            return src.SafeSubstring(length);
        }

        public static string Repeat(this string src, int count)
        {
            return String.Concat(Enumerable.Repeat(src, count));
        }

        public static string[] Split(this string src, string delim)
        {
            return src.Coalesce().Split(new[] { delim }, StringSplitOptions.RemoveEmptyEntries);
        }

        public static IEnumerable<string> Split(this string src, int chunkSize)
        {
            return src.Coalesce().ChunkBy(chunkSize).Select(m => String.Join("", m));
        }

        public static bool IsEmpty(this string value)
        {
            return String.IsNullOrWhiteSpace(value);
        }

        public static bool IsCaseSensitiveEqual(this string value, string comparing)
        {
            return String.CompareOrdinal(value, comparing) == 0;
        }

        public static string Coalesce(this string input)
        {
            return input ?? "";
        }

        public static string Coalesce(this string input, string def)
        {
            return input.Coalesce().IsEmpty() ? def : input.Coalesce();
        }

        public static string FormatInvariant(this string instance, params object[] args)
        {
            return String.Format(CultureInfo.InvariantCulture, instance, args);
        }

        public static string Md5(this string stringToHash)
        {
            var md5 = new MD5CryptoServiceProvider();
            var emailBytes = Encoding.UTF8.GetBytes(stringToHash);
            var hashedEmailBytes = md5.ComputeHash(emailBytes);
            var sb = new StringBuilder();
            foreach (var b in hashedEmailBytes)
            {
                sb.Append(b.ToString("x2").ToLower());
            }
            return sb.ToString().ToUpperInvariant();
        }

        public static string Reverse(this string value)
        {
            var charArray = value.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public static string TrimEnd(this string input, string suffix)
        {
            if (input != null && suffix != null && input.EndsWith(suffix))
                return input.Substring(0, input.Length - suffix.Length);

            return input;
        }

        public static string TrimStart(this string input, string prefix)
        {
            if (input != null && prefix != null && input.StartsWith(prefix))
                return input.Substring(prefix.Length);

            return input;
        }

        public static string StripNonAscii(this string input)
        {
            return Regex.Replace(input, @"[^\u0000-\u007F]+", "");
        }

        public static string SplitTitleCase(this string input)
        {
            var spl = input.Coalesce()
                .Split("_")
                .Select(s => s[0].ToString().ToUpper() + s.Substring(1).ToLower());
            return String.Join(" ", spl);
        }

        /// <summary>
        /// Cümledeki Tüm Kelimelerin İlk Harflerini Büyük Yapar
        /// </summary>
        public static string ToTitleCase(this string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        public static short ToShort(this string val, short defaultValue = default)
        {
            return Int16.TryParse(val, out var res) ? res : defaultValue;
        }

        public static int ToInt(this string val, int defaultValue = default)
        {
            return Int32.TryParse(val, out var res) ? res : defaultValue;
        }

        public static long ToLong(this string val, long defaultValue = default)
        {
            return Int64.TryParse(val, out var res) ? res : defaultValue;
        }

        public static double ToDouble(this string val, double defaultValue = default)
        {
            return Double.TryParse(val, out var res) ? res : defaultValue;
        }

        /// <summary>
        /// Cümlenin İlk Harfini Büyük Yapar
        /// </summary>
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null:
                    throw new ArgumentNullException(nameof(input));
                case "":
                    throw new ArgumentException($"{nameof(input)} boş olamaz", nameof(input));
                default:
                    return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source?.IndexOf(toCheck, comp) >= 0;
        }

        public static bool ToBool(this string value)
        {
            bool.TryParse(value, out var result);
            return result;
        }

        public static bool IsValidPhoneNumber(this string phoneNumber)
        {
            var mach = Regex.Match(phoneNumber, @"^(05(\d{9}))$", RegexOptions.IgnoreCase);
            return mach.Success;
        }

        public static float ToFloat(this string val, float defaultValue = default)
        {
            return Single.TryParse(val, out var res) ? res : defaultValue;
        }

        public static decimal ToDecimal(this string val, decimal defaultValue = default)
        {
            return decimal.TryParse(val, out var res) ? res : defaultValue;
        }
    }
}
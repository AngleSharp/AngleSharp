namespace AngleSharp.Services.Default
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents the default loader service. This class can be inherited.
    /// </summary>
    public class LocaleEncodingService : IEncodingService
    {
        static readonly Dictionary<String, Encoding> suggestions = new Dictionary<String, Encoding>(StringComparer.OrdinalIgnoreCase)
        {
            { "ar", TextEncoding.Utf8 },
            { "cy", TextEncoding.Utf8 },
            { "fa", TextEncoding.Utf8 },
            { "hr", TextEncoding.Utf8 },
            { "kk", TextEncoding.Utf8 },
            { "mk", TextEncoding.Utf8 },
            { "or", TextEncoding.Utf8 },
            { "ro", TextEncoding.Utf8 },
            { "sr", TextEncoding.Utf8 },
            { "vi", TextEncoding.Utf8 },
            { "be", TextEncoding.Latin5 },
            { "bg", TextEncoding.Windows1251 },
            { "ru", TextEncoding.Windows1251 },
            { "uk", TextEncoding.Windows1251 },
            { "cs", TextEncoding.Latin2 },
            { "hu", TextEncoding.Latin2 },
            { "pl", TextEncoding.Latin2 },
            { "sl", TextEncoding.Latin2 },
            { "tr", TextEncoding.Windows1254 },
            { "ku", TextEncoding.Windows1254 },
            { "he", TextEncoding.Windows1255 },
            { "lv", TextEncoding.Latin13 },
            { "ja", TextEncoding.Utf8 }, //  Windows-31J ???? Replaced by something better anyway
            { "ko", TextEncoding.GetEncoding("ks_c_5601-1987") },
            { "lt", TextEncoding.Windows1257 },
            { "sk", TextEncoding.Windows1250 },
            { "th", TextEncoding.Windows874 }
        };

        /// <summary>
        /// Suggests the initial Encoding for the given locale.
        /// </summary>
        /// <param name="locale">
        /// The locale defined by the BCP 47 language tag.
        /// </param>
        /// <returns>The suggested encoding.</returns>
        public Encoding Suggest(String locale)
        {
            if (!String.IsNullOrEmpty(locale) && locale.Length > 1)
            {
                var encoding = default(Encoding);

                if (suggestions.TryGetValue(locale.Substring(0, 2), out encoding))
                    return encoding;
                else if (locale.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                    return TextEncoding.Gb18030;
                else if (locale.Equals("zh-tw", StringComparison.OrdinalIgnoreCase))
                    return TextEncoding.Big5;
            }

            return TextEncoding.Windows1252;
        }
    }
}

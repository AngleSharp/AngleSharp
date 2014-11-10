namespace AngleSharp
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Various HTML encoding helpers.
    /// </summary>
    static class DocumentEncoding
    {
        #region Encodings

        static readonly Dictionary<String, Encoding> encodings = new Dictionary<String, Encoding>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Encoding> suggestions = new Dictionary<String, Encoding>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Gets the UTF-8 encoding.
        /// </summary>
        public static readonly Encoding UTF8 = Encoding.UTF8;

        /// <summary>
        /// Gets the UTF-16 (Big Endian) encoding.
        /// </summary>
        public static readonly Encoding UTF16BE = Encoding.BigEndianUnicode;

        /// <summary>
        /// Gets the UTF-16 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding UTF16LE = Encoding.Unicode;

        /// <summary>
        /// Gets the UTF-32 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding UTF32LE = GetEncoding("UTF-32LE");

        /// <summary>
        /// Gets the UTF-32 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding UTF32BE = GetEncoding("UTF-32BE");

        /// <summary>
        /// Gets the Windows-874 encoding.
        /// </summary>
        public static readonly Encoding Windows874 = GetEncoding("windows-874");

        /// <summary>
        /// Gets the Windows-1250 encoding.
        /// </summary>
        public static readonly Encoding Windows1250 = GetEncoding("windows-1250");

        /// <summary>
        /// Gets the Windows-1251 encoding.
        /// </summary>
        public static readonly Encoding Windows1251 = GetEncoding("windows-1251");

        /// <summary>
        /// Gets the Windows-1252 encoding.
        /// </summary>
        public static readonly Encoding Windows1252 = GetEncoding("windows-1252");

        /// <summary>
        /// Gets the Windows-1253 encoding.
        /// </summary>
        public static readonly Encoding Windows1253 = GetEncoding("windows-1253");

        /// <summary>
        /// Gets the Windows-1254 encoding.
        /// </summary>
        public static readonly Encoding Windows1254 = GetEncoding("windows-1254");

        /// <summary>
        /// Gets the Windows-1255 encoding.
        /// </summary>
        public static readonly Encoding Windows1255 = GetEncoding("windows-1255");

        /// <summary>
        /// Gets the Windows-1256 encoding.
        /// </summary>
        public static readonly Encoding Windows1256 = GetEncoding("windows-1256");

        /// <summary>
        /// Gets the Windows-1257 encoding.
        /// </summary>
        public static readonly Encoding Windows1257 = GetEncoding("windows-1257");

        /// <summary>
        /// Gets the chinese government standard encoding.
        /// </summary>
        public static readonly Encoding GB18030 = GetEncoding("GB18030");

        /// <summary>
        /// Gets the Big5 encoding.
        /// </summary>
        public static readonly Encoding Big5 = GetEncoding("big5");

        /// <summary>
        /// Gets the iso-8859-5 encoding.
        /// </summary>
        public static readonly Encoding Latin5 = GetEncoding("iso-8859-5");

        /// <summary>
        /// Gets the iso-8859-2 encoding.
        /// </summary>
        public static readonly Encoding Latin2 = GetEncoding("iso-8859-2");

        /// <summary>
        /// Gets the iso-8859-13 encoding.
        /// </summary>
        public static readonly Encoding Latin13 = GetEncoding("iso-8859-13");

        #endregion

        #region Initialization

        static DocumentEncoding()
        {
            encodings.Add("unicode-1-1-utf-8", UTF8);
            encodings.Add("utf-8", UTF8);
            encodings.Add("utf8", UTF8);
            encodings.Add("utf-16be", UTF16BE);
            encodings.Add("utf-16", UTF16LE);
            encodings.Add("utf-16le", UTF16LE);
            encodings.Add("dos-874", Windows874);
            encodings.Add("iso-8859-11", Windows874);
            encodings.Add("iso8859-11", Windows874);
            encodings.Add("iso885911", Windows874);
            encodings.Add("tis-620", Windows874);
            encodings.Add("windows-874", Windows874);
            encodings.Add("cp1250", Windows1250);
            encodings.Add("windows-1250", Windows1250);
            encodings.Add("x-cp1250", Windows1250);
            encodings.Add("cp1251", Windows1251);
            encodings.Add("windows-1251", Windows1251);
            encodings.Add("x-cp1251", Windows1251);
            encodings.Add("ansi_x3.4-1968", Windows1252);
            encodings.Add("ascii", Windows1252);
            encodings.Add("cp1252", Windows1252);
            encodings.Add("cp819", Windows1252);
            encodings.Add("csisolatin1", Windows1252);
            encodings.Add("ibm819", Windows1252);
            encodings.Add("iso-8859-1", Windows1252);
            encodings.Add("iso-ir-100", Windows1252);
            encodings.Add("iso8859-1", Windows1252);
            encodings.Add("iso88591", Windows1252);
            encodings.Add("iso_8859-1", Windows1252);
            encodings.Add("iso_8859-1:1987", Windows1252);
            encodings.Add("l1", Windows1252);
            encodings.Add("latin1", Windows1252);
            encodings.Add("us-ascii", Windows1252);
            encodings.Add("windows-1252", Windows1252);
            encodings.Add("x-cp1252", Windows1252);
            encodings.Add("cp1253", Windows1253);
            encodings.Add("windows-1253", Windows1253);
            encodings.Add("x-cp1253", Windows1253);
            encodings.Add("cp1254", Windows1254);
            encodings.Add("csisolatin5", Windows1254);
            encodings.Add("iso-8859-9", Windows1254);
            encodings.Add("iso-ir-148", Windows1254);
            encodings.Add("iso8859-9", Windows1254);
            encodings.Add("iso88599", Windows1254);
            encodings.Add("iso_8859-9", Windows1254);
            encodings.Add("iso_8859-9:1989", Windows1254);
            encodings.Add("l5", Windows1254);
            encodings.Add("latin5", Windows1254);
            encodings.Add("windows-1254", Windows1254);
            encodings.Add("x-cp1254", Windows1254);
            encodings.Add("cp1255", Windows1255);
            encodings.Add("windows-1255", Windows1255);
            encodings.Add("x-cp1255", Windows1255);
            encodings.Add("cp1256", Windows1256);
            encodings.Add("windows-1256", Windows1256);
            encodings.Add("x-cp1256", Windows1256);
            encodings.Add("cp1257", Windows1257);
            encodings.Add("windows-1257", Windows1257);
            encodings.Add("x-cp1257", Windows1257);
            var w1258 = GetEncoding("windows-1258");
            encodings.Add("cp1258", w1258);
            encodings.Add("windows-1258", w1258);
            encodings.Add("x-cp1258", w1258);
            var macintosh = GetEncoding("macintosh");
            encodings.Add("csmacintosh", macintosh);
            encodings.Add("mac", macintosh);
            encodings.Add("macintosh", macintosh);
            encodings.Add("x-mac-roman", macintosh);
            var maccyrillic = GetEncoding("x-mac-cyrillic"); ;
            encodings.Add("x-mac-cyrillic", maccyrillic);
            encodings.Add("x-mac-ukrainian", maccyrillic);
            var i866 = GetEncoding("cp866");
            encodings.Add("866", i866);
            encodings.Add("cp866", i866);
            encodings.Add("csibm866", i866);
            encodings.Add("ibm866", i866);
            encodings.Add("csisolatin2", Latin2);
            encodings.Add("iso-8859-2", Latin2);
            encodings.Add("iso-ir-101", Latin2);
            encodings.Add("iso8859-2", Latin2);
            encodings.Add("iso88592", Latin2);
            encodings.Add("iso_8859-2", Latin2);
            encodings.Add("iso_8859-2:1987", Latin2);
            encodings.Add("l2", Latin2);
            encodings.Add("latin2", Latin2);
            var latin3 = GetEncoding("iso-8859-3");
            encodings.Add("csisolatin3", latin3);
            encodings.Add("iso-8859-3", latin3);
            encodings.Add("iso-ir-109", latin3);
            encodings.Add("iso8859-3", latin3);
            encodings.Add("iso88593", latin3);
            encodings.Add("iso_8859-3", latin3);
            encodings.Add("iso_8859-3:1988", latin3);
            encodings.Add("l3", latin3);
            encodings.Add("latin3", latin3);
            var latin4 = GetEncoding("iso-8859-4");
            encodings.Add("csisolatin4", latin4);
            encodings.Add("iso-8859-4", latin4);
            encodings.Add("iso-ir-110", latin4);
            encodings.Add("iso8859-4", latin4);
            encodings.Add("iso88594", latin4);
            encodings.Add("iso_8859-4", latin4);
            encodings.Add("iso_8859-4:1988", latin4);
            encodings.Add("l4", latin4);
            encodings.Add("latin4", latin4);
            encodings.Add("csisolatincyrillic", Latin5);
            encodings.Add("cyrillic", Latin5);
            encodings.Add("iso-8859-5", Latin5);
            encodings.Add("iso-ir-144", Latin5);
            encodings.Add("iso8859-5", Latin5);
            encodings.Add("iso88595", Latin5);
            encodings.Add("iso_8859-5", Latin5);
            encodings.Add("iso_8859-5:1988", Latin5);
            var latin6 = GetEncoding("iso-8859-6");
            encodings.Add("arabic", latin6);
            encodings.Add("asmo-708", latin6);
            encodings.Add("csiso88596e", latin6);
            encodings.Add("csiso88596i", latin6);
            encodings.Add("csisolatinarabic", latin6);
            encodings.Add("ecma-114", latin6);
            encodings.Add("iso-8859-6", latin6);
            encodings.Add("iso-8859-6-e", latin6);
            encodings.Add("iso-8859-6-i", latin6);
            encodings.Add("iso-ir-127", latin6);
            encodings.Add("iso8859-6", latin6);
            encodings.Add("iso88596", latin6);
            encodings.Add("iso_8859-6", latin6);
            encodings.Add("iso_8859-6:1987", latin6);
            var latin7 = GetEncoding("iso-8859-7");
            encodings.Add("csisolatingreek", latin7);
            encodings.Add("ecma-118", latin7);
            encodings.Add("elot_928", latin7);
            encodings.Add("greek", latin7);
            encodings.Add("greek8", latin7);
            encodings.Add("iso-8859-7", latin7);
            encodings.Add("iso-ir-126", latin7);
            encodings.Add("iso8859-7", latin7);
            encodings.Add("iso88597", latin7);
            encodings.Add("iso_8859-7", latin7);
            encodings.Add("iso_8859-7:1987", latin7);
            encodings.Add("sun_eu_greek", latin7);
            var latin8 = GetEncoding("iso-8859-8");
            encodings.Add("csiso88598e", latin8);
            encodings.Add("csisolatinhebrew", latin8);
            encodings.Add("hebrew", latin8);
            encodings.Add("iso-8859-8", latin8);
            encodings.Add("iso-8859-8-e", latin8);
            encodings.Add("iso-ir-138", latin8);
            encodings.Add("iso8859-8", latin8);
            encodings.Add("iso88598", latin8);
            encodings.Add("iso_8859-8", latin8);
            encodings.Add("iso_8859-8:1988", latin8);
            encodings.Add("visual", latin8);
            var latini = GetEncoding("iso-8859-8-i");
            encodings.Add("csiso88598i", latini);
            encodings.Add("iso-8859-8-i", latini);
            encodings.Add("logical", latini);
            encodings.Add("iso-8859-13", Latin13);
            encodings.Add("iso8859-13", Latin13);
            encodings.Add("iso885913", Latin13);
            var latin15 = GetEncoding("iso-8859-15");
            encodings.Add("csisolatin9", latin15);
            encodings.Add("iso-8859-15", latin15);
            encodings.Add("iso8859-15", latin15);
            encodings.Add("iso885915", latin15);
            encodings.Add("iso_8859-15", latin15);
            encodings.Add("l9", latin15);
            var kr = GetEncoding("koi8-r");
            encodings.Add("cskoi8r", kr);
            encodings.Add("koi", kr);
            encodings.Add("koi8", kr);
            encodings.Add("koi8-r", kr);
            encodings.Add("koi8_r", kr);
            encodings.Add("koi8-u", GetEncoding("koi8-u"));
            var chinese = GetEncoding("x-cp20936");
            encodings.Add("chinese", chinese);
            encodings.Add("csgb2312", chinese);
            encodings.Add("csiso58gb231280", chinese);
            encodings.Add("gb2312", chinese);
            encodings.Add("gb_2312", chinese);
            encodings.Add("gb_2312-80", chinese);
            encodings.Add("gbk", chinese);
            encodings.Add("iso-ir-58", chinese);
            encodings.Add("x-gbk", chinese);
            encodings.Add("hz-gb-2312", GetEncoding("hz-gb-2312"));
            encodings.Add("gb18030", GB18030);
            var big5 = GetEncoding("big5");
            encodings.Add("big5", big5);
            encodings.Add("big5-hkscs", big5);
            encodings.Add("cn-big5", big5);
            encodings.Add("csbig5", big5);
            encodings.Add("x-x-big5", big5);
            var isojp = GetEncoding("iso-2022-jp");
            encodings.Add("csiso2022jp", isojp);
            encodings.Add("iso-2022-jp", isojp);
            var isokr = GetEncoding("iso-2022-kr");
            encodings.Add("csiso2022kr", isokr);
            encodings.Add("iso-2022-kr", isokr);
            var isocn = GetEncoding("iso-2022-cn");
            encodings.Add("iso-2022-cn", isocn);
            encodings.Add("iso-2022-cn-ext", isocn);

            suggestions.Add("ar", UTF8);
            suggestions.Add("cy", UTF8);
            suggestions.Add("fa", UTF8);
            suggestions.Add("hr", UTF8);
            suggestions.Add("kk", UTF8);
            suggestions.Add("mk", UTF8);
            suggestions.Add("or", UTF8);
            suggestions.Add("ro", UTF8);
            suggestions.Add("sr", UTF8);
            suggestions.Add("vi", UTF8);
            suggestions.Add("be", Latin5);
            suggestions.Add("bg", Windows1251);
            suggestions.Add("ru", Windows1251);
            suggestions.Add("uk", Windows1251);
            suggestions.Add("cs", Latin2);
            suggestions.Add("hu", Latin2);
            suggestions.Add("pl", Latin2);
            suggestions.Add("sl", Latin2);
            suggestions.Add("tr", Windows1254);
            suggestions.Add("ku", Windows1254);
            suggestions.Add("he", Windows1255);
            suggestions.Add("lv", Latin13);
            //  Windows-31J ???? Replaced by something better anyway
            suggestions.Add("ja", UTF8);
            suggestions.Add("ko", GetEncoding("ks_c_5601-1987"));
            suggestions.Add("lt", Windows1257);
            suggestions.Add("sk", Windows1250);
            suggestions.Add("th", Windows874);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to extract the encoding from the given http-equiv content string.
        /// </summary>
        /// <param name="content">The value of the attribute.</param>
        /// <returns>The extracted encoding or null if the encoding is invalid.</returns>
        public static Encoding Parse(String content)
        {
            var encoding = String.Empty;
            var position = 0;
            content = content.ToLowerInvariant();

            for (int i = position; i < content.Length - 7; i++)
            {
                if (content.Substring(i).StartsWith(AttributeNames.Charset))
                {
                    position = i + 7;
                    break;
                }
            }

            if (position > 0 && position < content.Length)
            {
                for (int i = position; i < content.Length - 1; i++)
                {
                    if (content[i].IsSpaceCharacter())
                        position++;
                    else
                        break;
                }

                if (content[position] != Specification.Equality)
                    return Parse(content.Substring(position));

                position++;

                for (int i = position; i < content.Length; i++)
                {
                    if (content[i].IsSpaceCharacter())
                        position++;
                    else
                        break;
                }

                if (position < content.Length)
                {
                    if (content[position] == Specification.DoubleQuote)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Specification.DoubleQuote);

                        if (index != -1)
                            encoding = content.Substring(0, index);
                    }
                    else if (content[position] == Specification.SingleQuote)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Specification.SingleQuote);

                        if (index != -1)
                            encoding = content.Substring(0, index);
                    }
                    else
                    {
                        content = content.Substring(position);
                        var index = 0;

                        for (int i = 0; i < content.Length; i++)
                        {
                            if (content[i].IsSpaceCharacter())
                                break;
                            else if (content[i] == ';')
                                break;
                            else
                                index++;
                        }

                        encoding = content.Substring(0, index);
                    }
                }
            }

            if (!IsSupported(encoding))
                return null;

            return Resolve(encoding);
        }

        /// <summary>
        /// Detects if a valid encoding has been found in the given charset string.
        /// </summary>
        /// <param name="charset">The parsed charset string.</param>
        /// <returns>True if a valid encdoing has been found, otherwise false.</returns>
        public static Boolean IsSupported(String charset)
        {
            return encodings.ContainsKey(charset);
        }

        /// <summary>
        /// Resolves an Encoding instance given by the charset string.
        /// If the desired encoding is not found (or supported), then
        /// UTF-8 will be returned.
        /// </summary>
        /// <param name="charset">The charset string.</param>
        /// <returns>An instance of the Encoding class or null.</returns>
        public static Encoding Resolve(String charset)
        {
            Encoding encoding;

            if (charset != null && encodings.TryGetValue(charset, out encoding))
                return encoding;

            return Encoding.UTF8;
        }

        /// <summary>
        /// Suggests an Encoding for the given local.
        /// </summary>
        /// <param name="local">The local defined by the BCP 47 language tag.</param>
        /// <returns>The suggested encoding.</returns>
        public static Encoding Suggest(String local)
        {
            if (!String.IsNullOrEmpty(local) && local.Length > 1)
            {
                var firstTwo = local.Substring(0, 2).ToLowerInvariant();
                Encoding encoding;

                if (suggestions.TryGetValue(local.Substring(0, 2), out encoding))
                    return encoding;

                if (local.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                    return GB18030;
                else if (local.Equals("zh-tw", StringComparison.OrdinalIgnoreCase))
                    return Big5;
            }

            return Windows1252;
        }

        /// <summary>
        /// Gets the encoding for lesser used charsets. This might result in an
        /// exception depending on the platform (mostly Windows Phone *).
        /// Exceptions are handled by returning UTF8. That should work well in
        /// most scenarios.
        /// </summary>
        /// <param name="name">The name of the charset.</param>
        /// <returns>The encoding for the given charset.</returns>
        static Encoding GetEncoding(String name)
        {
            try
            {
                return Encoding.GetEncoding(name);
            }
            catch
            {
                // We use a catch em all since WP8 does throw a different exception than W*.
                return Encoding.UTF8;
            }
        }

        #endregion
    }
}

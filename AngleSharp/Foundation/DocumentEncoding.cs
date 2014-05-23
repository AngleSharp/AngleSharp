namespace AngleSharp
{
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

        static DocumentEncoding()
        {
            encodings.Add("unicode-1-1-utf-8", Encoding.UTF8);
            encodings.Add("utf-8", Encoding.UTF8);
            encodings.Add("utf8", Encoding.UTF8);
            encodings.Add("utf-16be", Encoding.BigEndianUnicode);
            encodings.Add("utf-16", Encoding.Unicode);
            encodings.Add("utf-16le", Encoding.Unicode);
            var w874 = GetEncoding("windows-874");
            encodings.Add("dos-874", w874);
            encodings.Add("iso-8859-11", w874);
            encodings.Add("iso8859-11", w874);
            encodings.Add("iso885911", w874);
            encodings.Add("tis-620", w874);
            encodings.Add("windows-874", w874);
            var w1250 = GetEncoding("windows-1250");
            encodings.Add("cp1250", w1250);
            encodings.Add("windows-1250", w1250);
            encodings.Add("x-cp1250", w1250);
            var w1251 = GetEncoding("windows-1251");
            encodings.Add("cp1251", w1251);
            encodings.Add("windows-1251", w1251);
            encodings.Add("x-cp1251", w1251);
            var w1252 = GetEncoding("windows-1252");
            encodings.Add("ansi_x3.4-1968", w1252);
            encodings.Add("ascii", w1252);
            encodings.Add("cp1252", w1252);
            encodings.Add("cp819", w1252);
            encodings.Add("csisolatin1", w1252);
            encodings.Add("ibm819", w1252);
            encodings.Add("iso-8859-1", w1252);
            encodings.Add("iso-ir-100", w1252);
            encodings.Add("iso8859-1", w1252);
            encodings.Add("iso88591", w1252);
            encodings.Add("iso_8859-1", w1252);
            encodings.Add("iso_8859-1, Encoding.UTF8);1987", w1252);
            encodings.Add("l1", w1252);
            encodings.Add("latin1", w1252);
            encodings.Add("us-ascii", w1252);
            encodings.Add("windows-1252", w1252);
            encodings.Add("x-cp1252", w1252);
            var w1253 = GetEncoding("windows-1253");
            encodings.Add("cp1253", w1253);
            encodings.Add("windows-1253", w1253);
            encodings.Add("x-cp1253", w1253);
            var w1254 = GetEncoding("windows-1254");
            encodings.Add("cp1254", w1254);
            encodings.Add("csisolatin5", w1254);
            encodings.Add("iso-8859-9", w1254);
            encodings.Add("iso-ir-148", w1254);
            encodings.Add("iso8859-9", w1254);
            encodings.Add("iso88599", w1254);
            encodings.Add("iso_8859-9", w1254);
            encodings.Add("iso_8859-9, w1254);1989", w1254);
            encodings.Add("l5", w1254);
            encodings.Add("latin5", w1254);
            encodings.Add("windows-1254", w1254);
            encodings.Add("x-cp1254", w1254);
            var w1255 = GetEncoding("windows-1255");
            encodings.Add("cp1255", w1255);
            encodings.Add("windows-1255", w1255);
            encodings.Add("x-cp1255", w1255);
            var w1256 = GetEncoding("windows-1256");
            encodings.Add("cp1256", w1256);
            encodings.Add("windows-1256", w1256);
            encodings.Add("x-cp1256", w1256);
            var w1257 = GetEncoding("windows-1257");
            encodings.Add("cp1257", w1257);
            encodings.Add("windows-1257", w1257);
            encodings.Add("x-cp1257", w1257);
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
            var latin2 = GetEncoding("iso-8859-2");
            encodings.Add("csisolatin2", latin2);
            encodings.Add("iso-8859-2", latin2);
            encodings.Add("iso-ir-101", latin2);
            encodings.Add("iso8859-2", latin2);
            encodings.Add("iso88592", latin2);
            encodings.Add("iso_8859-2", latin2);
            encodings.Add("iso_8859-2, latin2);1987", latin2);
            encodings.Add("l2", latin2);
            encodings.Add("latin2", latin2);
            var latin3 = GetEncoding("iso-8859-3");
            encodings.Add("csisolatin3", latin3);
            encodings.Add("iso-8859-3", latin3);
            encodings.Add("iso-ir-109", latin3);
            encodings.Add("iso8859-3", latin3);
            encodings.Add("iso88593", latin3);
            encodings.Add("iso_8859-3", latin3);
            encodings.Add("iso_8859-3, latin3);1988", latin3);
            encodings.Add("l3", latin3);
            encodings.Add("latin3", latin3);
            var latin4 = GetEncoding("iso-8859-4");
            encodings.Add("csisolatin4", latin4);
            encodings.Add("iso-8859-4", latin4);
            encodings.Add("iso-ir-110", latin4);
            encodings.Add("iso8859-4", latin4);
            encodings.Add("iso88594", latin4);
            encodings.Add("iso_8859-4", latin4);
            encodings.Add("iso_8859-4, latin4);1988", latin4);
            encodings.Add("l4", latin4);
            encodings.Add("latin4", latin4);
            var latin5 = GetEncoding("iso-8859-5");
            encodings.Add("csisolatincyrillic", latin5);
            encodings.Add("cyrillic", latin5);
            encodings.Add("iso-8859-5", latin5);
            encodings.Add("iso-ir-144", latin5);
            encodings.Add("iso8859-5", latin5);
            encodings.Add("iso88595", latin5);
            encodings.Add("iso_8859-5", latin5);
            encodings.Add("iso_8859-5, latin5);1988", latin5);
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
            encodings.Add("iso_8859-6, latin6);1987", latin6);
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
            encodings.Add("iso_8859-7, latin7);1987", latin7);
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
            encodings.Add("iso_8859-8, latin8);1988", latin8);
            encodings.Add("visual", latin8);
            var latini = GetEncoding("iso-8859-8-i");
            encodings.Add("csiso88598i", latini);
            encodings.Add("iso-8859-8-i", latini);
            encodings.Add("logical", latini);
            var latin13 = GetEncoding("iso-8859-13");
            encodings.Add("iso-8859-13", latin13);
            encodings.Add("iso8859-13", latin13);
            encodings.Add("iso885913", latin13);
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
            encodings.Add("gb18030", GetEncoding("GB18030"));
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
        }

        #endregion

        /// <summary>
        /// Tries to extract the encoding from the given http-equiv content string.
        /// </summary>
        /// <param name="content">The value of the attribute.</param>
        /// <returns>The extracted encoding or an empty string.</returns>
        public static String Extract(String content)
        {
            var position = 0;
            content = content.ToLower();

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
                    return Extract(content.Substring(position));

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
                            return content.Substring(0, index);
                    }
                    else if (content[position] == Specification.SingleQuote)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Specification.SingleQuote);

                        if (index != -1)
                            return content.Substring(0, index);
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

                        return content.Substring(0, index);
                    }
                }
            }

            return String.Empty;
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

            if (encodings.TryGetValue(charset, out encoding))
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
                var firstTwo = local.Substring(0, 2).ToLower();

                switch (firstTwo)
                {
                    case "ar":
                    case "cy":
                    case "fa":
                    case "hr":
                    case "kk":
                    case "mk":
                    case "or":
                    case "ro":
                    case "sr":
                    case "vi":
                        return Encoding.UTF8;

                    case "be":
                        return GetEncoding("iso-8859-5");

                    case "bg":
                    case "ru":
                    case "uk":
                        return GetEncoding("windows-1251");

                    case "cs":
                    case "hu":
                    case "pl":
                    case "sl":
                        return GetEncoding("iso-8859-2");

                    case "tr":
                    case "ku":
                        return GetEncoding("windows-1254");

                    case "he":
                        return GetEncoding("windows-1255");

                    case "lv":
                        return GetEncoding("iso-8859-13");

                    case "ja"://  Windows-31J ???? Replaced by something better anyway
                        return Encoding.UTF8;

                    case "ko":
                        return GetEncoding("ks_c_5601-1987");

                    case "lt":
                        return GetEncoding("windows-1257");

                    case "sk":
                        return GetEncoding("windows-1250");

                    case "th":
                        return GetEncoding("windows-874");
                }

                if (local.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                    return GetEncoding("GB18030");
                else if (local.Equals("zh-tw", StringComparison.OrdinalIgnoreCase))
                    return GetEncoding("big5");
            }

            return GetEncoding("windows-1252");
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
    }
}

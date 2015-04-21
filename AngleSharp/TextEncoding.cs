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
    static class TextEncoding
    {
        #region Fields

        static readonly Dictionary<String, Encoding> encodings = new Dictionary<String, Encoding>(StringComparer.OrdinalIgnoreCase);
        static readonly Dictionary<String, Encoding> suggestions = new Dictionary<String, Encoding>(StringComparer.OrdinalIgnoreCase);

        #endregion

        #region Encodings

        /// <summary>
        /// Gets the UTF-8 encoding.
        /// </summary>
        public static readonly Encoding Utf8 = new UTF8Encoding(false);

        /// <summary>
        /// Gets the UTF-16 (Big Endian) encoding.
        /// </summary>
        public static readonly Encoding Utf16Be = new UnicodeEncoding(true, false);

        /// <summary>
        /// Gets the UTF-16 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding Utf16Le = new UnicodeEncoding(false, false);

        /// <summary>
        /// Gets the UTF-32 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding Utf32Le = GetEncoding("UTF-32LE");

        /// <summary>
        /// Gets the UTF-32 (Little Endian) encoding.
        /// </summary>
        public static readonly Encoding Utf32Be = GetEncoding("UTF-32BE");

        /// <summary>
        /// Gets the chinese government standard encoding.
        /// </summary>
        public static readonly Encoding Gb18030 = GetEncoding("GB18030");

        /// <summary>
        /// Gets the Big5 encoding.
        /// </summary>
        public static readonly Encoding Big5 = GetEncoding("big5");

        /// <summary>
        /// Gets the Windows-1252 encoding.
        /// </summary>
        public static readonly Encoding Windows1252 = GetEncoding("windows-1252");

        #endregion

        #region Initialization

        static TextEncoding()
        {
            encodings.Add("unicode-1-1-utf-8", Utf8);
            encodings.Add("utf-8", Utf8);
            encodings.Add("utf8", Utf8);

            encodings.Add("utf-16be", Utf16Be);
            encodings.Add("utf-16", Utf16Le);
            encodings.Add("utf-16le", Utf16Le);

            var windows874 = GetEncoding("windows-874");
            encodings.Add("dos-874", windows874);
            encodings.Add("iso-8859-11", windows874);
            encodings.Add("iso8859-11", windows874);
            encodings.Add("iso885911", windows874);
            encodings.Add("tis-620", windows874);
            encodings.Add("windows-874", windows874);

            var windows1250 = GetEncoding("windows-1250");
            encodings.Add("cp1250", windows1250);
            encodings.Add("windows-1250", windows1250);
            encodings.Add("x-cp1250", windows1250);

            var windows1251 = GetEncoding("windows-1251");
            encodings.Add("cp1251", windows1251);
            encodings.Add("windows-1251", windows1251);
            encodings.Add("x-cp1251", windows1251);

            encodings.Add("x-user-defined", Windows1252);
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

            var windows1253 = GetEncoding("windows-1253");
            encodings.Add("cp1253", windows1253);
            encodings.Add("windows-1253", windows1253);
            encodings.Add("x-cp1253", windows1253);

            var windows1254 = GetEncoding("windows-1254");
            encodings.Add("cp1254", windows1254);
            encodings.Add("csisolatin5", windows1254);
            encodings.Add("iso-8859-9", windows1254);
            encodings.Add("iso-ir-148", windows1254);
            encodings.Add("iso8859-9", windows1254);
            encodings.Add("iso88599", windows1254);
            encodings.Add("iso_8859-9", windows1254);
            encodings.Add("iso_8859-9:1989", windows1254);
            encodings.Add("l5", windows1254);
            encodings.Add("latin5", windows1254);
            encodings.Add("windows-1254", windows1254);
            encodings.Add("x-cp1254", windows1254);

            var windows1255 = GetEncoding("windows-1255");
            encodings.Add("cp1255", windows1255);
            encodings.Add("windows-1255", windows1255);
            encodings.Add("x-cp1255", windows1255);

            var windows1256 = GetEncoding("windows-1256");
            encodings.Add("cp1256", windows1256);
            encodings.Add("windows-1256", windows1256);
            encodings.Add("x-cp1256", windows1256);

            var windows1257 = GetEncoding("windows-1257");
            encodings.Add("cp1257", windows1257);
            encodings.Add("windows-1257", windows1257);
            encodings.Add("x-cp1257", windows1257);

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
            encodings.Add("iso_8859-2:1987", latin2);
            encodings.Add("l2", latin2);
            encodings.Add("latin2", latin2);

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

            var latin5 = GetEncoding("iso-8859-5");
            encodings.Add("csisolatincyrillic", latin5);
            encodings.Add("cyrillic", latin5);
            encodings.Add("iso-8859-5", latin5);
            encodings.Add("iso-ir-144", latin5);
            encodings.Add("iso8859-5", latin5);
            encodings.Add("iso88595", latin5);
            encodings.Add("iso_8859-5", latin5);
            encodings.Add("iso_8859-5:1988", latin5);

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
            encodings.Add("gb18030", Gb18030);

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
            encodings.Add("shift_jis", GetEncoding("shift_jis"));

            var eucjp = GetEncoding("euc-jp");
            encodings.Add("euc-jp", eucjp);

            suggestions.Add("ar", Utf8);
            suggestions.Add("cy", Utf8);
            suggestions.Add("fa", Utf8);
            suggestions.Add("hr", Utf8);
            suggestions.Add("kk", Utf8);
            suggestions.Add("mk", Utf8);
            suggestions.Add("or", Utf8);
            suggestions.Add("ro", Utf8);
            suggestions.Add("sr", Utf8);
            suggestions.Add("vi", Utf8);
            suggestions.Add("be", latin5);
            suggestions.Add("bg", windows1251);
            suggestions.Add("ru", windows1251);
            suggestions.Add("uk", windows1251);
            suggestions.Add("cs", latin2);
            suggestions.Add("hu", latin2);
            suggestions.Add("pl", latin2);
            suggestions.Add("sl", latin2);
            suggestions.Add("tr", windows1254);
            suggestions.Add("ku", windows1254);
            suggestions.Add("he", windows1255);
            suggestions.Add("lv", GetEncoding("iso-8859-13"));
            //  Windows-31J ???? Replaced by something better anyway
            suggestions.Add("ja", Utf8);
            suggestions.Add("ko", GetEncoding("ks_c_5601-1987"));
            suggestions.Add("lt", windows1257);
            suggestions.Add("sk", windows1250);
            suggestions.Add("th", windows874);
        }

        #endregion

        #region Extensions

        /// <summary>
        /// Checks if the provided encoding is any UTF-16 encoding.
        /// </summary>
        /// <param name="encoding">The encoding to check.</param>
        /// <returns>The result of the check (UTF-16BE, UTF-16LE).</returns>
        public static Boolean IsUnicode(this Encoding encoding)
        {
            return encoding == Utf16Be || encoding == Utf16Le;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Tries to extract the encoding from the given http-equiv content
        /// string.
        /// </summary>
        /// <param name="content">The value of the attribute.</param>
        /// <returns>
        /// The extracted encoding or null if the encoding is invalid.
        /// </returns>
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

                if (content[position] != Symbols.Equality)
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
                    if (content[position] == Symbols.DoubleQuote)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Symbols.DoubleQuote);

                        if (index != -1)
                            encoding = content.Substring(0, index);
                    }
                    else if (content[position] == Symbols.SingleQuote)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Symbols.SingleQuote);

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
        /// Detects if a valid encoding has been found in the given charset
        /// string.
        /// </summary>
        /// <param name="charset">The parsed charset string.</param>
        /// <returns>
        /// True if a valid encdoing has been found, otherwise false.
        /// </returns>
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

            return Utf8;
        }

        /// <summary>
        /// Suggests an Encoding for the given local.
        /// </summary>
        /// <param name="local">
        /// The local defined by the BCP 47 language tag.
        /// </param>
        /// <returns>The suggested encoding.</returns>
        public static Encoding Suggest(String local)
        {
            if (!String.IsNullOrEmpty(local) && local.Length > 1)
            {
                Encoding encoding;

                if (suggestions.TryGetValue(local.Substring(0, 2), out encoding))
                    return encoding;
                else if (local.Equals("zh-cn", StringComparison.OrdinalIgnoreCase))
                    return Gb18030;
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
                // We use a catch em all since WP8 does throw a different
                // exception than W*.
                return Utf8;
            }
        }

        #endregion

        #region Punycode

        const Int32 PunycodeBase = 36;
        const Int32 tmin = 1;
        const Int32 tmax = 26;
        static readonly String AcePrefix = "xn--";
        static readonly Char[] PossibleDots = { '.', '\u3002', '\uFF0E', '\uFF61' };

        static Boolean IsSupplementary(Int32 test)
        {
            return test >= 0x10000;
        }

        static Boolean IsDot(Char c)
        {
            return PossibleDots.Contains(c);
        }

        static Char EncodeDigit(Int32 d)
        {
            // 26-35 map to ASCII 0-9
            if (d > 25)
                return (Char)(d - 26 + '0');

            // 0-25 map to a-z or A-Z
            return (Char)(d + 'a');
        }

        static Char EncodeBasic(Char bcp)
        {
            if (Char.IsUpper(bcp))
                bcp += (Char)('a' - 'A');
            
            return bcp;
        }

        static Int32 AdaptChar(Int32 delta, Int32 numpoints, Boolean firsttime)
        {
            const Int32 Skew = 38;
            const Int32 Damp = 700;

            var k = 0u;

            delta = firsttime ? delta / Damp : delta / 2;
            delta += delta / numpoints;

            for (k = 0; delta > ((PunycodeBase - tmin) * tmax) / 2; k += PunycodeBase)
                delta /= PunycodeBase - tmin;

            return (Int32)(k + (PunycodeBase - tmin + 1) * delta / (delta + Skew));
        }

        public static String PunycodeEncode(String unicode)
        {
            const Int32 InitialBias = 72;
            const Int32 InitialNumber = 0x80;
            const Int32 MaxIntValue = 0x7ffffff;
            const Int32 LabelLimit = 63;
            const Int32 DefaultNameLimit = 255;

            // 0 length strings aren't allowed
            if (unicode.Length == 0)
                return unicode;

            var output = new StringBuilder(unicode.Length);
            var iNextDot = 0;
            var iAfterLastDot = 0;
            var iOutputAfterLastDot = 0;

            // Find the next dot
            while (iNextDot < unicode.Length)
            {
                // Find end of this segment
                iNextDot = unicode.IndexOfAny(PossibleDots, iAfterLastDot);

                if (iNextDot < 0)
                    iNextDot = unicode.Length;

                // Only allowed to have empty . section at end (www.microsoft.com.)
                if (iNextDot == iAfterLastDot)
                    break;

                // We'll need an Ace prefix
                output.Append(AcePrefix);

                var basicCount = 0;
                var numProcessed = 0;

                for (basicCount = iAfterLastDot; basicCount < iNextDot; basicCount++)
                {
                    if (unicode[basicCount] < 0x80)
                    {
                        output.Append(EncodeBasic(unicode[basicCount]));
                        numProcessed++;
                    }
                    else if (Char.IsSurrogatePair(unicode, basicCount))
                        basicCount++;
                }

                var numBasicCodePoints = numProcessed;

                if (numBasicCodePoints == iNextDot - iAfterLastDot)
                {
                    output.Remove(iOutputAfterLastDot, AcePrefix.Length);
                }
                else
                {
                    // If it has some non-basic code points the input cannot start with xn--
                    if (unicode.Length - iAfterLastDot >= AcePrefix.Length && unicode.Substring(iAfterLastDot, AcePrefix.Length).Equals(AcePrefix, StringComparison.OrdinalIgnoreCase))
                        break;

                    // Need to do ACE encoding
                    var numSurrogatePairs = 0;

                    // Add a delimiter (-) if we had any basic code points (between basic and encoded pieces)
                    if (numBasicCodePoints > 0)
                        output.Append('-');

                    // Initialize the state
                    var n = InitialNumber;
                    var delta = 0;
                    var bias = InitialBias;

                    // Main loop
                    while (numProcessed < (iNextDot - iAfterLastDot))
                    {
                        var j = 0;
                        var m = 0;
                        var test = 0;

                        for (m = MaxIntValue, j = iAfterLastDot; j < iNextDot; j += IsSupplementary(test) ? 2 : 1)
                        {
                            test = unicode.ConvertToUtf32(j);

                            if (test >= n && test < m)
                                m = test;
                        }

                        // Increase delta enough to advance the decoder's 
                        // <n,i> state to <m,0>, but guard against overflow:
                        delta += (m - n) * ((numProcessed - numSurrogatePairs) + 1);
                        n = m;

                        for (j = iAfterLastDot; j < iNextDot; j += IsSupplementary(test) ? 2 : 1)
                        {
                            // Make sure we're aware of surrogates
                            test = unicode.ConvertToUtf32(j);

                            // Adjust for character position (only the chars in our string already, some
                            // haven't been processed.

                            if (test < n)
                                delta++;

                            if (test == n)
                            {
                                // Represent delta as a generalized variable-length integer:
                                int q, k;

                                for (q = delta, k = PunycodeBase; ; k += PunycodeBase)
                                {
                                    int t = k <= bias ? tmin : k >= bias + tmax ? tmax : k - bias;

                                    if (q < t)
                                        break;

                                    output.Append(EncodeDigit(t + (q - t) % (PunycodeBase - t)));
                                    q = (q - t) / (PunycodeBase - t);
                                }

                                output.Append(EncodeDigit(q));
                                bias = AdaptChar(delta, (numProcessed - numSurrogatePairs) + 1, numProcessed == numBasicCodePoints);
                                delta = 0;
                                numProcessed++;

                                if (IsSupplementary(m))
                                {
                                    numProcessed++;
                                    numSurrogatePairs++;
                                }
                            }
                        }

                        ++delta;
                        ++n;
                    }
                }

                // Make sure its not too big
                if (output.Length - iOutputAfterLastDot > LabelLimit)
                    throw new ArgumentException();

                // Done with this segment, add dot if necessary
                if (iNextDot != unicode.Length)
                    output.Append(PossibleDots[0]);

                iAfterLastDot = iNextDot + 1;
                iOutputAfterLastDot = output.Length;
            }

            var rest = IsDot(unicode[unicode.Length - 1]) ? 0 : 1;
            var maxlength = DefaultNameLimit - rest;

            // Throw if we're too long
            if (output.Length > maxlength)
                output.Remove(maxlength, output.Length - maxlength);

            return output.ToString();
        }

        #endregion
    }
}

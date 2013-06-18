using System;
using System.Text;

namespace AngleSharp
{
    /// <summary>
    /// Various HTML encoding helpers.
    /// </summary>
    static class HtmlEncoding
    {
        public const String CHARSET = "charset";

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
                if (content.Substring(i).StartsWith(CHARSET))
                {
                    position = i + 7;
                    break;
                }
            }

            if (position > 0 && position < content.Length)
            {
                for (int i = position; i < content.Length - 1; i++)
                {
                    if (Specification.IsSpaceCharacter(content[i]))
                        position++;
                    else
                        break;
                }

                if (content[position] != Specification.EQ)
                    return Extract(content.Substring(position));

                position++;

                for (int i = position; i < content.Length; i++)
                {
                    if (Specification.IsSpaceCharacter(content[i]))
                        position++;
                    else
                        break;
                }

                if (position < content.Length)
                {
                    if (content[position] == Specification.DQ)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Specification.DQ);

                        if (index != -1)
                            return content.Substring(0, index);
                    }
                    else if (content[position] == Specification.SQ)
                    {
                        content = content.Substring(position + 1);
                        var index = content.IndexOf(Specification.SQ);

                        if (index != -1)
                            return content.Substring(0, index);
                    }
                    else
                    {
                        content = content.Substring(position);
                        var index = 0;

                        for (int i = 0; i < content.Length; i++)
                        {
                            if (Specification.IsSpaceCharacter(content[i]))
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
            return Resolve(charset) != null;
        }

        /// <summary>
        /// Resolves an Encoding instance given by the charset string.
        /// </summary>
        /// <param name="charset">The charset string.</param>
        /// <returns>An instance of the Encoding class or null.</returns>
        public static Encoding Resolve(String charset)
        {
            charset = charset.ToLower();

            switch (charset)
            {
                case "unicode-1-1-utf-8":
                case "utf-8":
                case "utf8":
                    return Encoding.UTF8;

                case "utf-16be":
                    return Encoding.BigEndianUnicode;

                case "utf-16":
                case "utf-16le":
                    return Encoding.Unicode;

                case "dos-874":
                case "iso-8859-11":
                case "iso8859-11":
                case "iso885911":
                case "tis-620":
                case "windows-874":
                    return Encoding.GetEncoding(874);

                case "cp1250":
                case "windows-1250":
                case "x-cp1250":
                    return Encoding.GetEncoding(1250);

                case "cp1251":
                case "windows-1251":
                case "x-cp1251":
                    return Encoding.GetEncoding(1251);

                case "ansi_x3.4-1968":
                case "ascii":
                case "cp1252": 
                case "cp819":
                case "csisolatin1":
                case "ibm819":
                case "iso-8859-1":
                case "iso-ir-100": 
                case "iso8859-1": 
                case "iso88591":
                case "iso_8859-1":
                case "iso_8859-1:1987":
                case "l1":
                case "latin1":
                case "us-ascii":
                case "windows-1252":
                case "x-cp1252":
                    return Encoding.GetEncoding(1252);

                case "cp1253":
                case "windows-1253":
                case "x-cp1253":
                    return Encoding.GetEncoding(1253);

                case "cp1254":
                case "csisolatin5":
                case "iso-8859-9":
                case "iso-ir-148":
                case "iso8859-9":
                case "iso88599":
                case "iso_8859-9":
                case "iso_8859-9:1989":
                case "l5":
                case "latin5":
                case "windows-1254":
                case "x-cp1254":
                    return Encoding.GetEncoding(1254);

                case "cp1255":
                case "windows-1255":
                case "x-cp1255":
                    return Encoding.GetEncoding(1255);

                case "cp1256":
                case "windows-1256":
                case "x-cp1256":
                    return Encoding.GetEncoding(1256);

                case "cp1257":
                case "windows-1257":
                case "x-cp1257":
                    return Encoding.GetEncoding(1257);

                case "cp1258":
                case "windows-1258":
                case "x-cp1258":
                    return Encoding.GetEncoding(1258);

                case "csmacintosh":
                case "mac":
                case "macintosh":
                case "x-mac-roman":
                    return Encoding.GetEncoding(10000);

                case "x-mac-cyrillic":
                case "x-mac-ukrainian":
                    return Encoding.GetEncoding(10007);

                case "866":
                case "cp866":
                case "csibm866":
                case "ibm866":
                    return Encoding.GetEncoding(866);

                case "csisolatin2":
                case "iso-8859-2":
                case "iso-ir-101":
                case "iso8859-2":
                case "iso88592":
                case "iso_8859-2":
                case "iso_8859-2:1987":
                case "l2":
                case "latin2":
                    return Encoding.GetEncoding(28592);

                case "csisolatin3":
                case "iso-8859-3":
                case "iso-ir-109":
                case "iso8859-3":
                case "iso88593":
                case "iso_8859-3":
                case "iso_8859-3:1988":
                case "l3":
                case "latin3":
                    return Encoding.GetEncoding(28593);

                case "csisolatin4":
                case "iso-8859-4":
                case "iso-ir-110":
                case "iso8859-4":
                case "iso88594":
                case "iso_8859-4":
                case "iso_8859-4:1988":
                case "l4":
                case "latin4":
                    return Encoding.GetEncoding(28594);

                case "csisolatincyrillic":
                case "cyrillic":
                case "iso-8859-5":
                case "iso-ir-144":
                case "iso8859-5":
                case "iso88595":
                case "iso_8859-5":
                case "iso_8859-5:1988":
                    return Encoding.GetEncoding(28595);

                case "arabic":
                case "asmo-708":
                case "csiso88596e":
                case "csiso88596i":
                case "csisolatinarabic":
                case "ecma-114":
                case "iso-8859-6":
                case "iso-8859-6-e":
                case "iso-8859-6-i":
                case "iso-ir-127":
                case "iso8859-6":
                case "iso88596":
                case "iso_8859-6":
                case "iso_8859-6:1987":
                    return Encoding.GetEncoding(28596);

                case "csisolatingreek":
                case "ecma-118":
                case "elot_928":
                case "greek":
                case "greek8":
                case "iso-8859-7":
                case "iso-ir-126":
                case "iso8859-7":
                case "iso88597":
                case "iso_8859-7":
                case "iso_8859-7:1987":
                case "sun_eu_greek":
                    return Encoding.GetEncoding(28597);

                case "csiso88598e":
                case "csisolatinhebrew":
                case "hebrew":
                case "iso-8859-8":
                case "iso-8859-8-e":
                case "iso-ir-138":
                case "iso8859-8":
                case "iso88598":
                case "iso_8859-8":
                case "iso_8859-8:1988":
                case "visual":
                    return Encoding.GetEncoding(28598);

                case "csiso88598i":
                case "iso-8859-8-i":
                case "logical":
                    return Encoding.GetEncoding(38598);

                case "iso-8859-13":
                case "iso8859-13":
                case "iso885913":
                    return Encoding.GetEncoding(28603);

                case "csisolatin9":
                case "iso-8859-15":
                case "iso8859-15":
                case "iso885915":
                case "iso_8859-15":
                case "l9":
                    return Encoding.GetEncoding(28605);

                case "cskoi8r":
                case "koi":
                case "koi8":
                case "koi8-r":
                case "koi8_r":
                    return Encoding.GetEncoding(20866);

                case "koi8-u":
                    return Encoding.GetEncoding(21866);

                case "chinese":
                case "csgb2312":
                case "csiso58gb231280":
                case "gb2312":
                case "gb_2312":
                case "gb_2312-80":
                case "gbk":
                case "iso-ir-58":
                case "x-gbk":
                    return Encoding.GetEncoding(20936);

                case "hz-gb-2312":
                    return Encoding.GetEncoding(52936);

                case "gb18030":
                    return Encoding.GetEncoding(54936);

                case "big5":
                case "big5-hkscs":
                case "cn-big5":
                case "csbig5":
                case "x-x-big5":
                    return Encoding.GetEncoding(950);

                case "csiso2022jp":
                case "iso-2022-jp":
                    return Encoding.GetEncoding(50222);

                case "csiso2022kr":
                case "iso-2022-kr":
                    return Encoding.GetEncoding(50225);

                case "iso-2022-cn":
                case "iso-2022-cn-ext":
                    return Encoding.GetEncoding(50220);

                default:
                    return null;
            }
        }

        /// <summary>
        /// Suggests an Encoding for the given local.
        /// </summary>
        /// <param name="local">The local defined by the BCP 47 language tag.</param>
        /// <returns>The suggested encoding.</returns>
        public static Encoding Suggest(string local)
        {
            var firstTwo = local.Substring(0, 2).ToLower();

            switch(firstTwo)
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
                    return Encoding.GetEncoding(28595);

                case "bg":
                case "ru":
                case "uk":
                    return Encoding.GetEncoding(1251);

                case "cs":
                case "hu":
                case "pl":
                case "sl":
                    return Encoding.GetEncoding(28592);

                case "tr":
                case "ku":
                    return Encoding.GetEncoding(1254);
                    
                case "he":
                    return Encoding.GetEncoding(1255);

                case "lv":
                    return Encoding.GetEncoding(28603);

                case "ja"://  Windows-31J ???? Replaced by something better anyway
                    return Encoding.UTF8;

                case "ko":
                    return Encoding.GetEncoding(949);

                case "lt":
                    return Encoding.GetEncoding(1257);

                case "sk":
                    return Encoding.GetEncoding(1250);

                case "th":
                    return Encoding.GetEncoding(874);
            }

            if (local.Equals("zh-CN", StringComparison.OrdinalIgnoreCase))
                return Encoding.GetEncoding(54936);
            else if (local.Equals("zh-TW", StringComparison.OrdinalIgnoreCase))
                return Encoding.GetEncoding(950);

            return Encoding.GetEncoding(1252);
        }
    }
}

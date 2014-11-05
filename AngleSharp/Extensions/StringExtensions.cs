namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.DOM;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Useful methods for string objects.
    /// </summary>
    [DebuggerStepThrough]
    static class StringExtensions
    {
        /// <summary>
        /// Examines if a the given list of characters contains a certain element.
        /// </summary>
        /// <param name="list">The list of characters.</param>
        /// <param name="element">The element to search for.</param>
        /// <returns>The status of the check.</returns>
        public static Boolean Contains(this IEnumerable<Char> list, Char element)
        {
            foreach (var entry in list)
                if (entry == element)
                    return true;

            return false;
        }

        /// <summary>
        /// Converts the given value to an enumeration value (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted enum value.</returns>
        public static T ToEnum<T>(this String value, T defaultValue) 
            where T : struct, IComparable
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            T converted = default(T);

            if (Enum.TryParse(value, true, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to a double (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted double.</returns>
        public static Double ToDouble(this String value, Double defaultValue = 0.0)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            Double converted;

            if (Double.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted integer.</returns>
        public static Int32 ToInteger(this String value, Int32 defaultValue = 0)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            Int32 converted;

            if (Int32.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an unsigned integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted unsigned integer.</returns>
        public static UInt32 ToInteger(this String value, UInt32 defaultValue = 0)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            UInt32 converted;

            if (UInt32.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to a boolean (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted boolean.</returns>
        public static Boolean ToBoolean(this String value, Boolean defaultValue = false)
        {
            if (String.IsNullOrEmpty(value))
                return defaultValue;

            Boolean converted;

            if (Boolean.TryParse(value, out converted))
                return converted;

            return defaultValue;
        }

        /// <summary>
        /// Replaces the first occurance of the string search with replace.
        /// </summary>
        /// <param name="text">The text to use.</param>
        /// <param name="search">The string to search for.</param>
        /// <param name="replace">The one-time replacement string.</param>
        /// <returns>The result of the search-and-replace.</returns>
        public static String ReplaceFirst(this String text, String search, String replace)
        {
            var pos = text.IndexOf(search);

            if (pos < 0)
                return text;

            return String.Concat(text.Substring(0, pos), replace, text.Substring(pos + search.Length));
        }

        /// <summary>
        /// Returns a value indicating whether the specified object occurs within this string.
        /// This method might seem obsolete, but it is quite useful in case of porting
        /// AngleSharp to a PCL, where String instances to not have a Contains method.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="content">The string to seek.</param>
        /// <returns>True if the value parameter occurs within this string, or if value is the empty string.</returns>
        public static Boolean Contains(this String str, String content)
        {
            return str.IndexOf(content) >= 0;
        }

        /// <summary>
        /// Collapses and strips all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse and strip.</param>
        /// <returns>The modified string with collapsed and stripped spaces.</returns>
        public static String CollapseAndStrip(this String str)
        {
            var chars = new List<Char>();
            var hasSpace = true;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].IsSpaceCharacter())
                {
                    if (hasSpace)
                        continue;

                    chars.Add(Specification.Space);
                    hasSpace = true;
                }
                else
                {
                    hasSpace = false;
                    chars.Add(str[i]);
                }
            }

            if (hasSpace && chars.Count > 0)
                chars.RemoveAt(chars.Count - 1);

            return new String(chars.ToArray());
        }

        /// <summary>
        /// Collapses all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse.</param>
        /// <returns>The modified string with collapsed spaces.</returns>
        public static String Collapse(this String str)
        {
            var chars = new List<Char>();
            var hasSpace = false;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i].IsSpaceCharacter())
                {
                    if (hasSpace)
                        continue;

                    chars.Add(Specification.Space);
                    hasSpace = true;
                }
                else
                {
                    hasSpace = false;
                    chars.Add(str[i]);
                }
            }

            return new String(chars.ToArray());
        }

        /// <summary>
        /// Examines if a the given list of string contains a certain element.
        /// </summary>
        /// <param name="list">The list of strings.</param>
        /// <param name="element">The element to search for.</param>
        /// <param name="comparison">The default comparison to use.</param>
        /// <returns>The status of the check.</returns>
        public static Boolean Contains(this String[] list, String element, StringComparison comparison = StringComparison.Ordinal)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (list[i].Equals(element, comparison))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2)
        {
            return element == item1 || element == item2;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3)
        {
            return element == item1 || element == item2 || element == item3;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <param name="item4">The fourth item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4)
        {
            return element == item1 || element == item2 || element == item3 || element == item4;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <param name="item4">The fourth item to compare to.</param>
        /// <param name="item5">The fifth item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4, String item5)
        {
            return element == item1 || element == item2 || element == item3 || element == item4 || element == item5;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <param name="item4">The fourth item to compare to.</param>
        /// <param name="item5">The fifth item to compare to.</param>
        /// <param name="item6">The sixth item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4, String item5, String item6)
        {
            return element == item1 || element == item2 || element == item3 || element == item4 || element == item5 || element == item6;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <param name="item4">The fourth item to compare to.</param>
        /// <param name="item5">The fifth item to compare to.</param>
        /// <param name="item6">The sixth item to compare to.</param>
        /// <param name="item7">The seventh item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4, String item5, String item6, String item7)
        {
            return element == item1 || element == item2 || element == item3 || element == item4 || element == item5 || element == item6 || element == item7;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="elements">The allowed (equal) elements.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static Boolean IsOneOf(this String element, params String[] elements)
        {
            for (var i = 0; i != elements.Length; i++)
            {
                if (element == elements[i])
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Strips all line breaks from the given string.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the line breaks.</returns>
        public static String StripLineBreaks(this String str)
        {
            var array = str.ToCharArray();
            var shift = 0;
            var length = array.Length;

            for (var i = 0; i < length; )
            {
                array[i] = array[i + shift];

                if (array[i].IsLineBreak())
                {
                    shift++;
                    length--;
                }
                else
                    i++;
            }

            return new String(array, 0, length);
        }

        /// <summary>
        /// Strips all leading and tailing space characters from the given string.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        public static String StripLeadingTailingSpaces(this String str)
        {
            return StripLeadingTailingSpaces(str.ToCharArray());
        }

        /// <summary>
        /// Strips all leading and tailing space characters from the given char array.
        /// </summary>
        /// <param name="array">The array of characters to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        public static String StripLeadingTailingSpaces(this Char[] array)
        {
            var start = 0;
            var end = array.Length - 1;

            while (start < array.Length && array[start].IsSpaceCharacter())
                start++;

            while (end > start && array[end].IsSpaceCharacter())
                end--;

            return new String(array, start, 1 + end - start);
        }

        /// <summary>
        /// Splits the string with the given char delimiter.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitWithoutTrimming(this String str, Char c)
        {
            return SplitWithoutTrimming(str.ToCharArray(), c);
        }

        /// <summary>
        /// Splits the char array with the given char delimiter.
        /// </summary>
        /// <param name="chars">The char array to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitWithoutTrimming(this Char[] chars, Char c)
        {
            var list = new List<String>();
            var index = 0;

            for (var i = 0; i < chars.Length; i++)
            {
                if (chars[i] == c)
                {
                    if (i > index)
                        list.Add(new String(chars, index, i - index));

                    index = i + 1;
                }
            }

            if (chars.Length > index)
                list.Add(new String(chars, index, chars.Length - index));

            return list.ToArray();
        }

        /// <summary>
        /// Splits the string on commas.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitCommas(this String str)
        {
            return str.SplitWithTrimming(',');
        }

        /// <summary>
        /// Splits the string on dash characters.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitHyphens(this String str)
        {
            return SplitWithTrimming(str, Specification.Minus);
        }

        /// <summary>
        /// Splits the string on space characters.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitSpaces(this String str)
        {
            var list = new List<String>();
            var buffer = new List<Char>();
            var chars = str.ToCharArray();

            for (var i = 0; i <= chars.Length; i++)
            {
                if (i == chars.Length || chars[i].IsSpaceCharacter())
                {
                    if (buffer.Count > 0)
                    {
                        var token = buffer.ToArray().StripLeadingTailingSpaces();

                        if (token.Length != 0)
                            list.Add(token);

                        buffer.Clear();
                    }
                }
                else
                    buffer.Add(chars[i]);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Splits the string with the given char delimiter and trims the leading and tailing spaces.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitWithTrimming(this String str, Char c)
        {
            var list = new List<String>();
            var buffer = new List<Char>();
            var chars = str.ToCharArray();

            for (var i = 0; i <= chars.Length; i++)
            {
                if (i == chars.Length || chars[i] == c)
                {
                    if (buffer.Count > 0)
                    {
                        var token = buffer.ToArray().StripLeadingTailingSpaces();

                        if (token.Length != 0)
                            list.Add(token);

                        buffer.Clear();
                    }
                }
                else
                    buffer.Add(chars[i]);
            }

            return list.ToArray();
        }

        /// <summary>
        /// Determines if the given string consists only of digits (0-9) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-digits
        /// </summary>
        /// <param name="s">The characters to examine.</param>
        /// <returns>The result of the test.</returns>
        public static Boolean IsDigit(this String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!s[i].IsDigit())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines if the given string only contains characters, which are hexadecimal (0-9a-fA-F) as specified here:
        /// http://www.whatwg.org/specs/web-apps/current-work/multipage/common-microsyntaxes.html#ascii-hex-digits
        /// </summary>
        /// <param name="s">The string to examine.</param>
        /// <returns>The result of the test.</returns>
        public static Boolean IsHex(this String s)
        {
            for (int i = 0; i < s.Length; i++)
            {
                if (!s[i].IsHex())
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The hexadecimal representation.</param>
        /// <returns>The integer number.</returns>
        public static Int32 FromHex(this String s)
        {
            return Int32.Parse(s, NumberStyles.HexNumber);
        }

        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The decimal representation.</param>
        /// <returns>The integer number.</returns>
        public static Int32 FromDec(this String s)
        {
            return Int32.Parse(s, NumberStyles.Integer);
        }

        /// <summary>
        /// Replaces characters in names and values that cannot be expressed by using the given
        /// encoding with &amp;#...; base-10 unicode point.
        /// </summary>
        /// <param name="value">The value to sanatize.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The sanatized value.</returns>
        public static String HtmlEncode(this String value, Encoding encoding)
        {
            //TODO Decide if the encoding is sufficient (How?)
            return value;
        }

        /// <summary>
        /// Serializes the string to a CSS string.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The CSS string representation.</returns>
        public static String CssString(this String value)
        {
            var builder = Pool.NewStringBuilder();
            builder.Append(Specification.DoubleQuote);

            if (!String.IsNullOrEmpty(value))
            {
                foreach (var character in value)
                {
                    if (character == Specification.Null)
                        throw new DomException(ErrorCode.InvalidCharacter);
                    else if (character == Specification.DoubleQuote || character == Specification.ReverseSolidus)
                        builder.Append(Specification.ReverseSolidus).Append(character);
                    else if (character.IsInRange(0x1, 0x1f) || character == (Char)0x7b)
                        builder.Append(Specification.ReverseSolidus).Append(character.ToHex()).Append(Specification.Space);
                    else
                        builder.Append(character);
                }
            }

            builder.Append(Specification.DoubleQuote);
            return builder.ToPool();
        }

        /// <summary>
        /// Serializes the string to a CSS url.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The CSS url representation.</returns>
        public static String CssUrl(this String value)
        {
            return String.Concat(FunctionNames.Url, "(", value.CssString(), ")");
        }

        /// <summary>
        /// Replaces characters in names and values that should not be in URL values.
        /// Replaces the bytes 0x20 (U+0020 SPACE if interpreted as ASCII) with a single 0x2B byte ("+" (U+002B)
        /// character if interpreted as ASCII).
        /// If a byte is not in the range 0x2A, 0x2D, 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A, it is
        /// replaced with its hexadecimal value (zero-padded if necessary), starting with the percent sign.
        /// </summary>
        /// <param name="value">The value to encode.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The encoded value.</returns>
        public static String UrlEncode(this String value, Encoding encoding)
        {
            var builder = Pool.NewStringBuilder();
            var content = encoding.GetBytes(value);

            foreach (var val in content)
            {
                var chr = (Char)val;

                if (chr == Specification.Space)
                    builder.Append(Specification.Plus);
                else if (chr == Specification.Asterisk || chr == Specification.Minus || chr == Specification.Dot || chr.IsAlphanumericAscii())
                    builder.Append(chr);
                else
                    builder.Append(Specification.Percent).Append(val.ToString("X2"));
            }

            return builder.ToPool();
        }
    }
}

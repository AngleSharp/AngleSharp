using System;
using System.Collections.Generic;
using AngleSharp.DOM;
using AngleSharp.DOM.Html;

namespace AngleSharp
{
    /// <summary>
    /// Some useful extensions.
    /// </summary>
    static class Extensions
    {
        /// <summary>
        /// Examines if a the given list of characters contains a certain element.
        /// </summary>
        /// <param name="list">The list of characters.</param>
        /// <param name="element">The element to search for.</param>
        /// <returns>The status of the check.</returns>
        public static bool Contains(this IEnumerable<char> list, char element)
        {
            foreach (var entry in list)
                if (entry == element)
                    return true;

            return false;
        }

        /// <summary>
        /// Collapses and strips all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse and strip.</param>
        /// <returns>The modified string with collapsed and stripped spaces.</returns>
        public static string CollapseAndStrip(this string str)
        {
            var chars = new List<char>();
            var hasSpace = true;

            for (int i = 0; i < str.Length; i++)
            {
                if (Specification.IsSpaceCharacter(str[i]))
                {
                    if (hasSpace)
                        continue;

                    chars.Add(Specification.SPACE);
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

            return new string(chars.ToArray());
        }

        /// <summary>
        /// Collapses all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse.</param>
        /// <returns>The modified string with collapsed spaces.</returns>
        public static string Collapse(this string str)
        {
            var chars = new List<char>();
            var hasSpace = false;

            for (int i = 0; i < str.Length; i++)
            {
                if (Specification.IsSpaceCharacter(str[i]))
                {
                    if (hasSpace)
                        continue;

                    chars.Add(Specification.SPACE);
                    hasSpace = true;
                }
                else
                {
                    hasSpace = false;
                    chars.Add(str[i]);
                }
            }

            return new string(chars.ToArray());
        }

        /// <summary>
        /// Examines if a the given list of string contains a certain element.
        /// </summary>
        /// <param name="list">The list of strings.</param>
        /// <param name="element">The element to search for.</param>
        /// <returns>The status of the check.</returns>
        public static bool Contains(this string[] list, string element)
        {
            for (int i = 0; i < list.Length; i++)
                if (list[i] == element)
                    return true;

            return false;
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="elements">The allowed (equal) elements.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsOneOf(this string element, params string[] elements)
        {
            for (var i = 0; i != elements.Length; i++)
                if (element.Equals(elements[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="node">The node to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsTableElement(this Node node)
        {
            return (node is HTMLTableElement || node is HTMLTableSectionElement || node is HTMLTableRowElement);
        }

        /// <summary>
        /// Examines if the given element is one of the table elements (table, tbody, tfoot, thead, tr).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsTableElement(this string tagName)
        {
            return (tagName == HTMLTableElement.Tag || tagName == HTMLTableSectionElement.BodyTag ||
                tagName == HTMLTableSectionElement.FootTag || tagName == HTMLTableSectionElement.HeadTag ||
                tagName == HTMLTableRowElement.Tag);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsTableSectionElement(this string tagName)
        {
            return (tagName == HTMLTableSectionElement.BodyTag || tagName == HTMLTableSectionElement.FootTag || 
                tagName == HTMLTableSectionElement.HeadTag);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsTableCellElement(this string tagName)
        {
            return (tagName == HTMLTableCellElement.NormalTag || tagName == HTMLTableCellElement.HeadTag);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (caption, col, colgroup, tbody, tfoot, thead).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <param name="includeRow">True if the tr element should also be tested.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsGeneralTableElement(this string tagName, bool includeRow = false)
        {
            return (tagName == HTMLTableCaptionElement.Tag || tagName == HTMLTableColElement.ColTag ||
                tagName == HTMLTableColElement.ColgroupTag || tagName == HTMLTableSectionElement.BodyTag ||
                tagName == HTMLTableSectionElement.FootTag || tagName == HTMLTableSectionElement.HeadTag) || (includeRow && tagName == HTMLTableRowElement.Tag);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (body, caption, col, colgroup, html, td, th).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <param name="includeRow">True if the tr element should also be tested.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsSpecialTableElement(this string tagName, bool includeRow = false)
        {
            return (tagName == HTMLBodyElement.Tag || tagName == HTMLHtmlElement.Tag ||
                tagName == HTMLTableColElement.ColgroupTag || tagName == HTMLTableColElement.ColTag ||
                tagName == HTMLTableCellElement.HeadTag || tagName == HTMLTableCellElement.NormalTag ||
                tagName == HTMLTableCaptionElement.Tag) || (includeRow && tagName == HTMLTableRowElement.Tag);
        }

        /// <summary>
        /// Examines if the given tag name matches one of the elements (html, body, br).
        /// </summary>
        /// <param name="tagName">The tag name to examine</param>
        /// <param name="includeHead">True if the head element should also be tested.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        public static bool IsHtmlBodyOrBreakRowElement(this string tagName, bool includeHead = false)
        {
            return (tagName == HTMLHtmlElement.Tag || tagName == HTMLBodyElement.Tag ||
                tagName == HTMLBRElement.Tag) || (includeHead && tagName == HTMLHeadElement.Tag);
        }

        /// <summary>
        /// Converts the given char (should be A-Z) to a lowercase version.
        /// </summary>
        /// <param name="chr">The uppercase char.</param>
        /// <returns>The lowercase char of A-Z, otherwise undefined.</returns>
        public static char ToLower(this char chr)
        {
            return (char)(chr + 0x20);
        }

        /// <summary>
        /// Converts a given character from the hex representation (0-9A-Fa-f) to an integer.
        /// </summary>
        /// <param name="c">The character to convert.</param>
        /// <returns>The integer value or undefined behavior if invalid.</returns>
        public static int FromHex(this char c)
        {
            return Specification.IsDigit(c) ? c - 0x30 : c - (Specification.IsLowercaseAscii(c) ? 0x57 : 0x37);
        }

        /// <summary>
        /// Strips all line breaks from the given string.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the line breaks.</returns>
        public static string StripLineBreaks(this string str)
        {
            var array = str.ToCharArray();
            var shift = 0;
            var length = array.Length;

            for (var i = 0; i < length;)
            {
                array[i] = array[i + shift];

                if (Specification.IsLineBreak(array[i]))
                {
                    shift++;
                    length--;
                }
                else
                    i++;
            }

            return new string(array, 0, length);
        }

        /// <summary>
        /// Strips all leading and tailing space characters from the given string.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        public static string StripLeadingTailingSpaces(this string str)
        {
            return StripLeadingTailingSpaces(str.ToCharArray());
        }

        /// <summary>
        /// Strips all leading and tailing space characters from the given char array.
        /// </summary>
        /// <param name="array">The array of characters to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        public static string StripLeadingTailingSpaces(this char[] array)
        {
            var start = 0;
            var end = array.Length - 1;

            while (start < array.Length && Specification.IsSpaceCharacter(array[start]))
                start++;

            while (end > start && Specification.IsSpaceCharacter(array[end]))
                end--;

            return new string(array, start, 1 + end - start);
        }

        /// <summary>
        /// Splits the string with the given char delimiter.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static string[] SplitWithoutTrimming(this string str, char c)
        {
            return SplitWithoutTrimming(str.ToCharArray(), c);
        }

        /// <summary>
        /// Splits the char array with the given char delimiter.
        /// </summary>
        /// <param name="chars">The char array to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static string[] SplitWithoutTrimming(this char[] chars, char c)
        {
            var list = new List<string>();
            var index = 0;

            for (var i = 0; i < chars.Length; i++)
            {
                if (chars[i] == c)
                {
                    if (i > index)
                        list.Add(new string(chars, index, i - index));

                    index = i + 1;
                }
            }

            if (chars.Length > index)
                list.Add(new string(chars, index, chars.Length - index));

            return list.ToArray();
        }

        /// <summary>
        /// Splits the string on commas.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static string[] SplitCommas(this string str)
        {
            return str.SplitWithTrimming(',');
        }

        /// <summary>
        /// Splits the string on dash characters.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static string[] SplitHyphens(this string str)
        {
            return SplitWithTrimming(str, Specification.DASH);
        }

        /// <summary>
        /// Splits the string on space characters.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static string[] SplitSpaces(this string str)
        {
            var list = new List<string>();
            var buffer = new List<char>();
            var chars = str.ToCharArray();

            for (var i = 0; i <= chars.Length; i++)
            {
                if (i == chars.Length || Specification.IsSpaceCharacter(chars[i]))
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
        public static string[] SplitWithTrimming(this string str, char c)
        {
            var list = new List<string>();
            var buffer = new List<char>();
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
    }
}

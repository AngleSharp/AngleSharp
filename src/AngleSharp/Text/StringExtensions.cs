namespace AngleSharp.Text
{
    using AngleSharp.Attributes;
    using AngleSharp.Browser;
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using AngleSharp.Io;
    using System;
    using System.Buffers;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Text;
    using Common;

    /// <summary>
    /// Useful methods for string objects.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public readonly struct Lease<T> : IDisposable
        {
            private readonly ArrayPool<T> owner;

            /// <summary>
            ///
            /// </summary>
            /// <param name="owner"></param>
            /// <param name="data"></param>
            /// <param name="requestedLength"></param>
            public Lease(ArrayPool<T> owner, T[] data, Int32 requestedLength)
            {
                this.owner = owner;
                this.Data = data;
                this.RequestedLength = requestedLength;
            }

            /// <summary>
            ///
            /// </summary>
            public T[] Data { get; }

            /// <summary>
            ///
            /// </summary>
            public Span<T> Span => Data.AsSpan(0, RequestedLength);

            /// <summary>
            ///
            /// </summary>
            public Int32 RequestedLength { get; }

            /// <summary>
            ///
            /// </summary>
            public void Dispose()
            {
                owner.Return(this.Data, false);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pool"></param>
        /// <param name="length"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static Lease<T> Borrow<T>(this ArrayPool<T> pool, Int32 length)
        {
            var arr = ArrayPool<T>.Shared.Rent(length);
            return new Lease<T>(ArrayPool<T>.Shared, arr, length);
        }

        /// <summary>
        /// Checks if the given string has a certain character at a specific
        /// index. The index is optional (default is 0).
        /// </summary>
        /// <param name="value">The value to examine.</param>
        /// <param name="chr">The character to look for.</param>
        /// <param name="index">The index of the character.</param>
        /// <returns>True if the value has the char, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]

        public static Boolean Has([NotNullWhen(true)] this String? value, Char chr, Int32 index = 0)
        {
            return value != null && value.Length > index && value[index] == chr;
        }

        /// <summary>
        /// Retrieves a string describing the compatibility mode of the given quirksmode.
        /// </summary>
        /// <param name="mode">A specific quriks mode.</param>
        /// <returns>The compatibility string.</returns>
        internal static String GetCompatiblity(this QuirksMode mode)
        {
            var description = "CSS1Compat";

            var fieldName = mode.ToString();

            if (fieldName != null)
            {
                var field = typeof(QuirksMode).GetField(fieldName);

                if (field != null)
                {
                    var domDescriptionAttribute = field.GetCustomAttribute<DomDescriptionAttribute>();

                    if (domDescriptionAttribute != null)
                    {
                        description = domDescriptionAttribute.Description;
                    }
                }
            }

            return description;
        }

        /// <summary>
        /// Transforms the given string to lower case by the HTML specification.
        /// </summary>
        /// <param name="value">The string to be transformed.</param>
        /// <returns>The resulting string.</returns>
        public static StringOrMemory HtmlLower(this StringOrMemory value)
        {
            var length = value.Length;

            for (var i = 0; i < length; i++)
            {
                var c = value[i];

                if (c.IsUppercaseAscii())
                {
                    if (length < 128)
                    {
                        // safe to stackalloc inside loop because will immediately return
                        Span<Char> result = stackalloc Char[length];
                        return Slow(value, i, result);
                    }
                    else
                    {
                        var rent = ArrayPool<Char>.Shared.Rent(length);
                        Span<Char> result = rent.AsSpan(0, length);
                        var tmp = Slow(value, i, result);
                        ArrayPool<Char>.Shared.Return(rent);
                        return tmp;
                    }
                }
            }

            return value;

            static String Slow(StringOrMemory value, Int32 i, Span<Char> result)
            {
                for (var j = 0; j < i; j++)
                {
                    result[j] = value[j];
                }

                var c = value[i];
                result[i] = Char.ToLowerInvariant(c);

                for (var j = i + 1; j < value.Length; j++)
                {
                    c = value[j];

                    if (c.IsUppercaseAscii())
                    {
                        c = Char.ToLowerInvariant(c);
                    }

                    result[j] = c;
                }

                return result.CreateString();
            }
        }

        /// <summary>
        /// Transforms the given string to lower case by the HTML specification.
        /// </summary>
        /// <param name="value">The string to be transformed.</param>
        /// <returns>The resulting string.</returns>
        public static String HtmlLower(this String value)
        {
            var length = value.Length;

            for (var i = 0; i < length; i++)
            {
                var c = value[i];

                if (c.IsUppercaseAscii())
                {
                    if (length < 128)
                    {
                        // safe to stackalloc inside loop because will immediately return
                        Span<Char> result = stackalloc Char[length];
                        return Slow(value, i, result);
                    }
                    else
                    {
                        var rent = ArrayPool<Char>.Shared.Rent(length);
                        Span<Char> result = rent.AsSpan(0, length);
                        var tmp = Slow(value, i, result);
                        ArrayPool<Char>.Shared.Return(rent);
                        return tmp;
                    }
                }
            }

            return value;

            static String Slow(StringOrMemory value, Int32 i, Span<Char> result)
            {
                for (var j = 0; j < i; j++)
                {
                    result[j] = value[j];
                }

                var c = value[i];
                result[i] = Char.ToLowerInvariant(c);

                for (var j = i + 1; j < value.Length; j++)
                {
                    c = value[j];

                    if (c.IsUppercaseAscii())
                    {
                        c = Char.ToLowerInvariant(c);
                    }

                    result[j] = c;
                }

                return result.CreateString();
            }
        }

        /// <summary>
        /// Converts the given value to a sandbox flag.
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="allowFullscreen">Should full screen be allowed?</param>
        /// <returns>The sandbox flag.</returns>
        public static Sandboxes ParseSecuritySettings(this String value, Boolean allowFullscreen = false)
        {
            var values = value.SplitSpaces();
            var output = Sandboxes.Navigation | Sandboxes.Plugins | Sandboxes.DocumentDomain;

            if (!values.Contains("allow-popups", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.AuxiliaryNavigation;
            }

            if (!values.Contains("allow-top-navigation", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.TopLevelNavigation;
            }

            if (!values.Contains("allow-same-origin", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.Origin;
            }

            if (!values.Contains("allow-forms", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.Forms;
            }

            if (!values.Contains("allow-pointer-lock", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.PointerLock;
            }

            if (!values.Contains("allow-scripts", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.Scripts;
                output |= Sandboxes.AutomaticFeatures;
            }

            if (!values.Contains("allow-presentation", StringComparison.OrdinalIgnoreCase))
            {
                output |= Sandboxes.Presentation;
            }

            if (!allowFullscreen)
            {
                output |= Sandboxes.Fullscreen;
            }

            return output;
        }

        /// <summary>
        /// Converts the given value to an enumeration value (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted enum value.</returns>
        public static T ToEnum<T>(this String? value, T defaultValue)
            where T : struct, Enum
        {
            if (!String.IsNullOrEmpty(value) && Enum.TryParse(value, true, out T converted))
            {
                return converted;
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to a double (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted double.</returns>
        public static Double ToDouble(this String? value, Double defaultValue = 0.0)
        {
            if (!String.IsNullOrEmpty(value) && Double.TryParse(value, NumberStyles.Any, NumberFormatInfo.InvariantInfo, out var converted))
            {
                return converted;
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted integer.</returns>
        public static Int32 ToInteger(this String? value, Int32 defaultValue = 0)
        {
            if (!String.IsNullOrEmpty(value) && Int32.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var converted))
            {
                return converted;
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to an unsigned integer (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted unsigned integer.</returns>
        public static UInt32 ToInteger(this String? value, UInt32 defaultValue = 0)
        {
            if (!String.IsNullOrEmpty(value) && UInt32.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var converted))
            {
                return converted;
            }

            return defaultValue;
        }

        /// <summary>
        /// Converts the given value to a boolean (or not).
        /// </summary>
        /// <param name="value">The value to convert.</param>
        /// <param name="defaultValue">The default value to consider (optional).</param>
        /// <returns>The converted boolean.</returns>
        public static Boolean ToBoolean(this String? value, Boolean defaultValue = false)
        {
            if (!String.IsNullOrEmpty(value) && Boolean.TryParse(value, out var converted))
            {
                return converted;
            }

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
            {
                return text;
            }

            return String.Concat(text.Substring(0, pos), replace, text.Substring(pos + search.Length));
        }

        /// <summary>
        /// Collapses and strips all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse and strip.</param>
        /// <returns>The modified string with collapsed and stripped spaces.</returns>
        public static String CollapseAndStrip(this String str)
        {
            if (str.Length == 0)
            {
                return str;
            }

            var buffer = ArrayPool<Char>.Shared.Rent(str.Length);

            var hasSpace = true;
            var index = 0;
            var l = str.Length;

            for (var i = 0; i < l; i++)
            {
                if (str[i].IsSpaceCharacter())
                {
                    if (!hasSpace)
                    {
                        hasSpace = true;
                        buffer[index++] = Symbols.Space;
                    }
                }
                else
                {
                    hasSpace = false;
                    buffer[index++] = str[i];
                }
            }

            if (hasSpace && index > 0)
            {
                index--;
            }

            var result = new String(buffer, 0, index);

            ArrayPool<char>.Shared.Return(buffer);

            return result;
        }

        /// <summary>
        /// Collapses all spaces in the given string.
        /// </summary>
        /// <param name="str">The string to collapse.</param>
        /// <returns>The modified string with collapsed spaces.</returns>
        public static String Collapse(this String str)
        {
            var sb = StringBuilderPool.Obtain();

            var hasSpace = false;
            var l = str.Length;

            for (var i = 0; i < l; i++)
            {
                if (str[i].IsSpaceCharacter())
                {
                    if (!hasSpace)
                    {
                        sb.Append(Symbols.Space);
                        hasSpace = true;
                    }
                }
                else
                {
                    hasSpace = false;
                    sb.Append(str[i]);
                }
            }

            return sb.ToPool();
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
            var l = list.Length;

            for (var i = 0; i < l; i++)
            {
                if (list[i].Equals(element, comparison))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the given string satisfies the rules for a custom element name.
        /// </summary>
        /// <param name="tag">The current tag name.</param>
        /// <returns>True if the string matches a custom element name.</returns>
        public static Boolean IsCustomElement(this String tag)
        {
            if (tag.IndexOf('-') != -1 && !TagNames.DisallowedCustomElementNames.Contains(tag))
            {
                var l = tag.Length;

                for (var i = 0; i < l; i++)
                {
                    if (!tag[i].IsCustomElementName())
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the given string satisfies the rules for a custom element name.
        /// </summary>
        /// <param name="tag">The current tag name.</param>
        /// <returns>True if the string matches a custom element name.</returns>
        public static Boolean IsCustomElement(this StringOrMemory tag)
        {
            if (tag.Memory.Span.IndexOf('-') != -1 && !TagNames._mDisallowedCustomElementNames.Contains(tag))
            {
                var l = tag.Length;

                for (var i = 0; i < l; i++)
                {
                    if (!tag[i].IsCustomElementName())
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if two strings are exactly equal.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this String? current, String? other) =>
            String.Equals(current, other, StringComparison.Ordinal);

        /// <summary>
        /// Checks if two strings are exactly equal.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this String? current, StringOrMemory other) =>
            other.Memory.Span.SequenceEqual(current.AsSpan());

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this Span<Char> current, String? other)
        {
            if (other == null) return false;
            return current.SequenceEqual(other.AsSpan());
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this ReadOnlySpan<Char> current, ReadOnlyMemory<Char> other)
        {
            return current.SequenceEqual(other.Span);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Is(this ReadOnlySpan<Char> current, ReadOnlySpan<Char> other)
        {
            return current.SequenceEqual(other);
        }

        /// <summary>
        /// Checks if two strings are equal when viewed case-insensitive.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this String? current, String? other) =>
            String.Equals(current, other, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        /// Checks if two strings are equal when viewed case-insensitive.
        /// </summary>
        /// <param name="current">The current string.</param>
        /// <param name="other">The other string.</param>
        /// <returns>True if both are equal, false otherwise.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this String? current, StringOrMemory other) =>
            MemoryExtensions.Equals(current.AsSpan(), other.Memory.Span, StringComparison.OrdinalIgnoreCase);

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this Span<Char> current, String? other)
        {
            if (other == null) return false;
            return System.MemoryExtensions.Equals(current, other.AsSpan(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this Span<Char> current, ReadOnlySpan<Char> other)
        {
            return System.MemoryExtensions.Equals(current, other, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this Span<Char> current, ReadOnlyMemory<Char> other)
        {
            return System.MemoryExtensions.Equals(current, other.Span, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="current"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean Isi(this ReadOnlySpan<Char> current, String? other)
        {
            if (other == null) return false;
            return System.MemoryExtensions.Equals(current, other.AsSpan(), StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOneOf(this String? element, String item1, String item2)
            => element.Is(item1) || element.Is(item2);

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3) =>
            element.Is(item1) || element.Is(item2) || element.Is(item3);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOneOf(this string element, StringOrMemory item1, StringOrMemory item2, StringOrMemory item3) =>
            element.Is(item1) || element.Is(item2) || element.Is(item3);

        /// <summary>
        /// Examines if the given element is equal to one of the given elements.
        /// </summary>
        /// <param name="element">The element to check for equality.</param>
        /// <param name="item1">The first item to compare to.</param>
        /// <param name="item2">The second item to compare to.</param>
        /// <param name="item3">The third item to compare to.</param>
        /// <param name="item4">The fourth item to compare to.</param>
        /// <returns>True if the element is equal to one of the elements, otherwise false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4) =>
            element.Is(item1) || element.Is(item2) || element.Is(item3) || element.Is(item4);

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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean IsOneOf(this String element, String item1, String item2, String item3, String item4, String item5) =>
            element.Is(item1) || element.Is(item2) || element.Is(item3) || element.Is(item4) || element.Is(item5);

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
            var index = 0;

            while (index < length)
            {
                array[index] = array[index + shift];

                if (array[index].IsLineBreak())
                {
                    shift++;
                    length--;
                }
                else
                {
                    index++;
                }
            }

            return new String(array, 0, length);
        }

        /// <summary>
        /// Strips all leading and trailing space characters from the given char array.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>A new string, which excludes the leading and tailing spaces.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String StripLeadingTrailingSpaces(this string str)
        {
            var start = 0;
            var end = str.Length - 1;

            while (start < str.Length && str[start].IsSpaceCharacter())
            {
                start++;
            }

            while (end > start && str[end].IsSpaceCharacter())
            {
                end--;
            }

            return str.Substring(start, 1 + end - start);
        }

        /// <summary>
        /// Splits the char array with the given char delimiter.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="c">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitWithoutTrimming(this string str, Char c)
        {
            var list = new List<String>();
            var index = 0;
            var l = str.Length;

            for (var i = 0; i < l; i++)
            {
                if (str[i] == c)
                {
                    if (i > index)
                    {
                        list.Add(str.Substring(index, i - index));
                    }

                    index = i + 1;
                }
            }

            if (l > index)
            {
                list.Add(str.Substring(index, l - index));
            }

            return list.ToArray();
        }

        /// <summary>
        /// Splits the string on commas.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String[] SplitCommas(this String str) =>
            str.SplitWithTrimming(',');

        /// <summary>
        /// Checks if the provided string starts with the given value, either by exactly matching it,
        /// or by comparing against the start including an additional dash character.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="value">The value to check against.</param>
        /// <param name="comparison">The string comparison mode.</param>
        /// <returns>True if the string is exactly equal to or starts with the given value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Boolean HasHyphen(this String str, String value, StringComparison comparison = StringComparison.Ordinal) =>
            String.Equals(str, value, comparison) ||
            (str.Length > value.Length && str.StartsWith(value, comparison) && str[value.Length] == Symbols.Minus);

        /// <summary>
        /// Splits the string on space characters.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitSpaces(this String str)
        {
            var list = new List<String>();
            var buffer = ArrayPool<Char>.Shared.Rent(str.Length);
            int c = 0;

            for (var i = 0; i <= str.Length; i++)
            {
                if (i == str.Length || str[i].IsSpaceCharacter())
                {
                    if (c > 0)
                    {
                        var token = new String(buffer, 0, c).StripLeadingTrailingSpaces();

                        if (token.Length != 0)
                        {
                            list.Add(token);
                        }

                        c = 0;
                    }
                }
                else
                {
                    buffer[c] = str[i];

                    c++;
                }
            }

            ArrayPool<char>.Shared.Return(buffer);

            return list.ToArray();
        }

        /// <summary>
        /// Splits the string with the given char delimiter and trims the leading and tailing spaces.
        /// </summary>
        /// <param name="str">The string to examine.</param>
        /// <param name="ch">The delimiter character.</param>
        /// <returns>The list of tokens.</returns>
        public static String[] SplitWithTrimming(this String str, Char ch)
        {
            var list = new List<String>();
            var buffer = ArrayPool<Char>.Shared.Rent(str.Length);
            int c = 0;

            for (var i = 0; i <= str.Length; i++)
            {
                if (i == str.Length || str[i] == ch)
                {
                    if (c > 0)
                    {
                        var token = new String(buffer, 0, c).StripLeadingTrailingSpaces();

                        if (token.Length != 0)
                        {
                            list.Add(token);
                        }

                        c = 0;
                    }
                }
                else
                {
                    buffer[c] = str[i];

                    c++;
                }
            }

            ArrayPool<char>.Shared.Return(buffer);

            return list.ToArray();
        }

        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The hexadecimal representation.</param>
        /// <returns>The integer number.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromHex(this String s) =>
            Int32.Parse(s, NumberStyles.HexNumber, CultureInfo.InvariantCulture);

        /// <summary>
        /// Converts the given string to an integer.
        /// </summary>
        /// <param name="s">The decimal representation.</param>
        /// <returns>The integer number.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Int32 FromDec(this String s) =>
            Int32.Parse(s, NumberStyles.Integer, CultureInfo.InvariantCulture);

        /// <summary>
        /// Replaces characters in names and values that cannot be expressed by using the given
        /// encoding with &amp;#...; base-10 unicode point.
        /// </summary>
        /// <param name="value">The value to sanatize.</param>
        /// <param name="encoding">The encoding to consider.</param>
        /// <returns>The sanatized value.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static String HtmlEncode(this String value, Encoding encoding)
        {
            //In the PCL we don't have access to EncoderFallback and cannot
            //integrate such a logic without much logic overhead.
            return value;
        }

        /// <summary>
        /// Serializes the string to a CSS string.
        /// </summary>
        /// <param name="value">The value to serialize.</param>
        /// <returns>The CSS string representation.</returns>
        public static String CssString(this String value)
        {
            var builder = StringBuilderPool.Obtain();
            builder.Append(Symbols.DoubleQuote);

            if (!String.IsNullOrEmpty(value))
            {
                var l = value.Length;

                for (var i = 0; i < l; i++)
                {
                    var character = value[i];

                    if (character == Symbols.Null)
                    {
                        builder.ReturnToPool();

                        throw new DomException(DomError.InvalidCharacter);
                    }

                    if (character == Symbols.DoubleQuote || character == Symbols.ReverseSolidus)
                    {
                        builder.Append(Symbols.ReverseSolidus).Append(character);
                    }
                    else if (character.IsInRange(0x1, 0x1f) || character == (Char)0x7b)
                    {
                        builder.Append(Symbols.ReverseSolidus).Append(character.ToHex()).Append(i + 1 != l ? " " : "");
                    }
                    else
                    {
                        builder.Append(character);
                    }
                }
            }

            builder.Append(Symbols.DoubleQuote);
            return builder.ToPool();
        }

        /// <summary>
        /// Creates a CSS function from the string with the given argument.
        /// </summary>
        /// <param name="value">The CSS function name.</param>
        /// <param name="argument">The CSS function argument.</param>
        /// <returns>The CSS function string.</returns>
        public static String CssFunction(this String value, String argument) =>
            String.Concat(value, "(", argument, ")");

        /// <summary>
        /// Replaces characters in names and values that should not be in URL
        /// values. Replaces the bytes 0x20 (U+0020 SPACE if interpreted as
        /// ASCII) with a single 0x2B byte ("+" (U+002B) character if
        /// interpreted as ASCII). If a byte is not in the range 0x2A, 0x2D,
        /// 0x2E, 0x30 to 0x39, 0x41 to 0x5A, 0x5F, 0x61 to 0x7A, it is
        /// replaced with its hexadecimal value (zero-padded if necessary),
        /// starting with the percent sign.
        /// </summary>
        /// <param name="content">The content to encode.</param>
        /// <returns>The encoded value.</returns>
        public static String UrlEncode(this Byte[] content)
        {
            var builder = StringBuilderPool.Obtain();
            var l = content.Length;

            for (var i = 0; i < l; i++)
            {
                var chr = (Char)content[i];

                if (chr == Symbols.Space)
                {
                    builder.Append(Symbols.Plus);
                }
                else if (chr == Symbols.Asterisk || chr == Symbols.Minus || chr == Symbols.Dot || chr == Symbols.Underscore || chr == Symbols.Tilde || chr.IsAlphanumericAscii())
                {
                    builder.Append(chr);
                }
                else
                {
                    builder.Append(Symbols.Percent).Append(content[i].ToString("X2"));
                }
            }

            return builder.ToPool();
        }

        /// <summary>
        /// Decodes the provided percent encoded string. An exception is thrown
        /// in case of an invalid input value.
        /// </summary>
        /// <param name="value">The value to decode.</param>
        /// <returns>The decoded content.</returns>
        public static Byte[] UrlDecode(this String value)
        {
            var ms = new MemoryStream();
            var l = value.Length;

            for (var i = 0; i < l; i++)
            {
                var chr = value[i];

                if (chr == Symbols.Plus)
                {
                    var b = (Byte)Symbols.Space;
                    ms.WriteByte(b);
                }
                else if (chr == Symbols.Percent)
                {
                    if (i + 2 >= l)
                    {
                        throw new FormatException();
                    }

                    var code = 16 * value[++i].FromHex() + value[++i].FromHex();
                    var b = (Byte)code;
                    ms.WriteByte(b);
                }
                else
                {
                    var b = (Byte)chr;
                    ms.WriteByte(b);
                }
            }

            return ms.ToArray();
        }

        /// <summary>
        /// Replaces every occurrence of a "CR" (U+000D) character not followed
        /// by a "LF" (U+000A) character, and every occurrence of a "LF"
        /// (U+000A) character not preceded by a "CR" (U+000D) character, by a
        /// two-character string consisting of a U+000D CARRIAGE RETURN "CRLF"
        /// (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        public static String NormalizeLineEndings(this String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var builder = StringBuilderPool.Obtain();
                var isCR = false;
                var l = value.Length;

                for (var i = 0; i < l; i++)
                {
                    var current = value[i];
                    var isLF = current == Symbols.LineFeed;

                    if (isCR && !isLF)
                    {
                        builder.Append(Symbols.LineFeed);
                    }
                    else if (!isCR && isLF)
                    {
                        builder.Append(Symbols.CarriageReturn);
                    }

                    isCR = current == Symbols.CarriageReturn;
                    builder.Append(current);
                }

                if (isCR)
                {
                    builder.Append(Symbols.LineFeed);
                }

                return builder.ToPool();
            }

            return value;
        }

        /// <summary>
        /// Converts the current string to one of the encoding types.
        /// </summary>
        /// <param name="encType">The string to convert.</param>
        /// <returns>
        /// The valid encoding type string or null.
        /// </returns>
        public static String? ToEncodingType(this String? encType) =>
            encType.Isi(MimeTypeNames.Plain) ||
            encType.Isi(MimeTypeNames.MultipartForm) ||
            encType.Isi(MimeTypeNames.ApplicationJson) ?
                encType?.ToLowerInvariant() : null;

        /// <summary>
        /// Converts the current string to one of the form methods.
        /// </summary>
        /// <param name="method">The string to convert.</param>
        /// <returns>
        /// The valid form method string or null.
        /// </returns>
        public static String? ToFormMethod(this String? method) =>
            method.Isi(FormMethodNames.Get) ||
            method.Isi(FormMethodNames.Post) ||
            method.Isi(FormMethodNames.Dialog) ?
                method?.ToLowerInvariant() : null;
    }
}

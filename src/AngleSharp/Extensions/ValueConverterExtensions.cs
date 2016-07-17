namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Css.ValueConverters;
    using AngleSharp.Css.Values;
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Essential extensions for using the value converters.
    /// </summary>
    static class ValueConverterExtensions
    {
        public static IPropertyValue ConvertDefault(this IValueConverter converter)
        {
            return converter.Convert(Enumerable.Empty<CssToken>());
        }

        public static Boolean HasDefault(this IValueConverter converter)
        {
            return converter.ConvertDefault() != null;
        }

        public static IPropertyValue VaryStart(this IValueConverter converter, List<CssToken> list)
        {
            for (var count = list.Count; count > 0; count--)
            {
                if (list[count - 1].Type == CssTokenType.Whitespace)
                    continue;

                var value = converter.Convert(list.Take(count));

                if (value != null)
                {
                    list.RemoveRange(0, count);
                    list.Trim();
                    return value;
                }
            }

            return converter.ConvertDefault();
        }

        public static IPropertyValue VaryAll(this IValueConverter converter, List<CssToken> list)
        {
            for (var i = 0; i < list.Count; i++)
            {
                if (list[i].Type == CssTokenType.Whitespace)
                    continue;

                for (var j = list.Count; j > i; j--)
                {
                    var count = j - i;

                    if (list[j - 1].Type == CssTokenType.Whitespace)
                        continue;

                    var value = converter.Convert(list.Skip(i).Take(count));

                    if (value != null)
                    {
                        list.RemoveRange(i, count);
                        list.Trim();
                        return value;
                    }
                }
            }

            return converter.ConvertDefault();
        }

        public static IValueConverter Many(this IValueConverter converter, Int32 min = 1, Int32 max = UInt16.MaxValue)
        {
            return new OneOrMoreValueConverter(converter, min, max);
        }

        public static IValueConverter FromList(this IValueConverter converter)
        {
            return new ListValueConverter(converter);
        }

        public static IValueConverter ToConverter<T>(this Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter Periodic(this IValueConverter converter, params String[] labels)
        {
            return new PeriodicValueConverter(converter, labels);
        }

        public static IValueConverter RequiresEnd(this IValueConverter listConverter, IValueConverter endConverter)
        {
            return new EndListValueConverter(listConverter, endConverter);
        }

        public static IValueConverter Required(this IValueConverter converter)
        {
            return new RequiredValueConverter(converter);
        }

        public static IValueConverter Option(this IValueConverter converter)
        {
            return new OptionValueConverter(converter);
        }

        public static IValueConverter For(this IValueConverter converter, params String[] labels)
        {
            return new ConstraintValueConverter(converter, labels);
        }

        public static IValueConverter Option<T>(this IValueConverter converter, T defaultValue)
        {
            return new OptionValueConverter<T>(converter, defaultValue);
        }

        public static IValueConverter Or(this IValueConverter primary, IValueConverter secondary)
        {
            return new OrValueConverter(primary, secondary);
        }

        public static IValueConverter Or(this IValueConverter primary, String keyword)
        {
            return primary.Or<Object>(keyword, null);
        }

        public static IValueConverter Or<T>(this IValueConverter primary, String keyword, T value)
        {
            var identifier = new IdentifierValueConverter<T>(keyword, value);
            return new OrValueConverter(primary, identifier);
        }

        public static IValueConverter OrNone(this IValueConverter primary)
        {
            return primary.Or(Keywords.None);
        }

        public static IValueConverter OrDefault(this IValueConverter primary)
        {
            return primary.OrInherit().Or(Keywords.Initial);
        }

        public static IValueConverter OrDefault<T>(this IValueConverter primary, T value)
        {
            return primary.OrInherit().Or(Keywords.Initial, value);
        }

        public static IValueConverter OrInherit(this IValueConverter primary)
        {
            return primary.Or(Keywords.Inherit);
        }

        public static IValueConverter OrAuto(this IValueConverter primary)
        {
            return primary.Or(Keywords.Auto);
        }

        public static IValueConverter StartsWithKeyword(this IValueConverter converter, String keyword)
        {
            return new StartsWithValueConverter(CssTokenType.Ident, keyword, converter);
        }

        public static IValueConverter StartsWithDelimiter(this IValueConverter converter)
        {
            return new StartsWithValueConverter(CssTokenType.Delim, "/", converter);
        }

        public static IValueConverter WithCurrentColor(this IValueConverter converter)
        {
            return converter.Or(Keywords.CurrentColor, Color.Transparent);
        }
    }
}

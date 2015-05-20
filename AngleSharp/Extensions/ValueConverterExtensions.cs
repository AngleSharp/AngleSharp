namespace AngleSharp.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using AngleSharp.Css;
    using AngleSharp.Css.ValueConverters;
    using AngleSharp.Css.Values;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;
    
    /// <summary>
    /// Essential extensions for using the value converters.
    /// </summary>
    [DebuggerStepThrough]
    static class ValueConverterExtensions
    {
        #region Methods

        public static T Convert<T>(this IValueConverter<T> converter, IEnumerable<CssToken> value)
        {
            var result = default(T);
            converter.TryConvert(value, m => result = m);
            return result;
        }

        public static Boolean VaryStart<T>(this IValueConverter<T> converter, List<CssToken> list, Action<T> setResult)
        {
            return converter.VaryStart(list, (c, v) => c.TryConvert(v, setResult));
        }

        public static Boolean VaryStart<T>(this IValueConverter<T> converter, List<CssToken> list)
        {
            return converter.VaryStart(list, (c, v) => c.Validate(v));
        }

        static Boolean VaryStart<T>(this IValueConverter<T> converter, List<CssToken> list, Func<IValueConverter<T>, IEnumerable<CssToken>, Boolean> validate)
        {
            for (int count = list.Count; count > 0; count--)
            {
                if (list[count - 1].Type != CssTokenType.Whitespace && validate(converter, list.Take(count)))
                {
                    list.RemoveRange(0, count);
                    list.Trim();
                    return true;
                }
            }

            return validate(converter, Enumerable.Empty<CssToken>());
        }

        public static Boolean VaryAll<T>(this IValueConverter<T> converter, List<CssToken> list, Action<T> setResult)
        {
            return converter.VaryAll(list, (c, v) => c.TryConvert(v, setResult));
        }

        public static Boolean VaryAll<T>(this IValueConverter<T> converter, List<CssToken> list)
        {
            return converter.VaryAll(list, (c, v) => c.Validate(v));
        }

        static Boolean VaryAll<T>(this IValueConverter<T> converter, List<CssToken> list, Func<IValueConverter<T>, IEnumerable<CssToken>, Boolean> validate)
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Type == CssTokenType.Whitespace)
                    continue;

                for (int j = list.Count; j > i; j--)
                {
                    var count = j - i;

                    if (list[j - 1].Type != CssTokenType.Whitespace && validate(converter, list.Skip(i).Take(count)))
                    {
                        list.RemoveRange(i, count);
                        list.Trim();
                        return true;
                    }
                }
            }

            return validate(converter, Enumerable.Empty<CssToken>());
        }

        public static IValueConverter<T[]> Many<T>(this IValueConverter<T> converter, Int32 min = 1, Int32 max = UInt16.MaxValue)
        {
            return new OneOrMoreValueConverter<T>(converter, min, max);
        }

        public static IValueConverter<T[]> FromList<T>(this IValueConverter<T> converter)
        {
            return new ListValueConverter<T>(converter);
        }

        public static IValueConverter<T> ToConverter<T>(this Dictionary<String, T> values)
        {
            return new DictionaryValueConverter<T>(values);
        }

        public static IValueConverter<U> To<T, U>(this IValueConverter<T> converter, Func<T, U> result)
        {
            return new ToValueConverter<T, U>(converter, result);
        }

        public static IValueConverter<Tuple<T, T, T, T>> Periodic<T>(this IValueConverter<T> converter)
        {
            return converter.To(m => Tuple.Create(m, m, m, m)).Or(
                new ArgumentsValueConverter<T, T>(converter, converter).To(m => Tuple.Create(m.Item1, m.Item2, m.Item1, m.Item2))).Or(
                new ArgumentsValueConverter<T, T, T>(converter, converter, converter).To(m => Tuple.Create(m.Item1, m.Item2, m.Item3, m.Item2))).Or(
                new ArgumentsValueConverter<T, T, T, T>(converter, converter, converter, converter));
        }

        public static IValueConverter<Tuple<T[], U>> RequiresEnd<T, U>(this IValueConverter<T[]> listConverter, IValueConverter<U> endConverter)
        {
            return new EndListValueConverter<T, U>(listConverter, endConverter);
        }

        public static IValueConverter<T> Required<T>(this IValueConverter<T> converter)
        {
            return new RequiredValueConverter<T>(converter);
        }

        public static IValueConverter<CssValue> Val<T>(this IValueConverter<T> converter)
        {
            return new ToValueConverter<T>(converter);
        }

        public static IValueConverter<T> Option<T>(this IValueConverter<T> converter, T defaultValue)
        {
            return new OptionValueConverter<T>(converter, defaultValue);
        }

        public static IValueConverter<CssValue> Option(this IValueConverter<CssValue> converter)
        {
            return new OptionValueConverter<CssValue>(converter, null);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, IValueConverter<T> secondary)
        {
            return new OrValueConverter<T>(primary, secondary);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, String keyword, T value)
        {
            var identifier = new IdentifierValueConverter<T>(keyword, value);
            return new OrValueConverter<T>(primary, identifier);
        }

        public static IValueConverter<Nullable<T>> ToNullable<T>(this IValueConverter<T> primary)
            where T : struct
        {
            return primary.To(m => new Nullable<T>(m));
        }

        public static IValueConverter<T> OrDefault<T>(this IValueConverter<T> primary)
        {
            var identifier = new IdentifierValueConverter<T>(Keywords.Auto, default(T));
            return primary.Or(identifier);
        }

        public static IValueConverter<Nullable<T>> OrNullDefault<T>(this IValueConverter<T> primary)
            where T : struct
        {
            return primary.ToNullable().OrDefault();
        }

        public static IValueConverter<T> Constraint<T>(this IValueConverter<T> primary, Predicate<T> constraint)
        {
            return new ConstraintValueConverter<T>(primary, constraint);
        }

        public static IValueConverter<T> StartsWithKeyword<T>(this IValueConverter<T> converter, String keyword)
        {
            return new StartsWithValueConverter<T>(m => m.Type == CssTokenType.Ident && m.Data.Equals(keyword, StringComparison.OrdinalIgnoreCase), converter);
        }

        public static IValueConverter<T> StartsWithDelimiter<T>(this IValueConverter<T> converter)
        {
            return new StartsWithValueConverter<T>(m => m.Type == CssTokenType.Delim && m.Data == "/", converter);
        }

        public static IValueConverter<Color> WithCurrentColor(this IValueConverter<Color> converter)
        {
            return converter.Or(Keywords.CurrentColor, Color.Transparent);
        }

        #endregion
    }
}

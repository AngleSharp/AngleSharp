namespace AngleSharp.Extensions
{
    using AngleSharp.Css;
    using AngleSharp.Css.ValueConverters;
    using AngleSharp.Dom.Css;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// Essential extensions for using the value converters.
    /// </summary>
    [DebuggerStepThrough]
    static class ValueConverterExtensions
    {
        #region Fields

        static readonly IValueConverter<Boolean> delimiter = new StructValueConverter<Boolean>(m => m == CssValue.Delimiter ? (Boolean?)true : null);

        #endregion

        #region Methods

        public static T Convert<T>(this IValueConverter<T> converter, ICssValue value)
        {
            var result = default(T);
            converter.TryConvert(value, m => result = m);
            return result;
        }

        public static Boolean VaryStart<T>(this IValueConverter<T> converter, CssValueList list, Action<T> setResult)
        {
            return converter.VaryStart(list, (c, v) => c.TryConvert(v, setResult));
        }

        public static Boolean VaryStart<T>(this IValueConverter<T> converter, CssValueList list)
        {
            return converter.VaryStart(list, (c, v) => c.Validate(v));
        }

        static Boolean VaryStart<T>(this IValueConverter<T> converter, CssValueList list, Func<IValueConverter<T>, ICssValue, Boolean> validate)
        {
            var min = Math.Max(converter.MinArgs, 1);
            var max = converter.MaxArgs;
            var n = Math.Min(max, list.Length);

            for (int count = n; count >= min; count--)
            {
                var subset = count > 1 ? list.Subset(0, count) : list[0];

                if (validate(converter, subset))
                {
                    list.RemoveRange(0, count);
                    return true;
                }
            }

            return validate(converter, null);
        }

        public static Boolean VaryAll<T>(this IValueConverter<T> converter, CssValueList list, Action<T> setResult)
        {
            return converter.VaryAll(list, (c, v) => c.TryConvert(v, setResult));
        }

        public static Boolean VaryAll<T>(this IValueConverter<T> converter, CssValueList list)
        {
            return converter.VaryAll(list, (c, v) => c.Validate(v));
        }

        static Boolean VaryAll<T>(this IValueConverter<T> converter, CssValueList list, Func<IValueConverter<T>, ICssValue, Boolean> validate)
        {
            var min = Math.Max(converter.MinArgs, 1);
            var max = converter.MaxArgs;

            for (int i = 0; i < list.Length; i++)
            {
                var n = Math.Min(Math.Min(max, list.Length) + i, list.Length);

                for (int j = n; j >= i + min; j--)
                {
                    var count = j - i;
                    var subset = count > 1 ? list.Subset(i, j) : list[i];

                    if (validate(converter, subset))
                    {
                        list.RemoveRange(i, count);
                        return true;
                    }
                }
            }

            return validate(converter, null);
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

        public static IValueConverter<T> Atomic<T>(this IValueConverter<T> converter)
        {
            return new AtomicValueConverter<T>(converter);
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

        public static IValueConverter<ICssValue> Val<T>(this IValueConverter<T> converter)
        {
            return new ToValueConverter<T>(converter);
        }

        public static IValueConverter<T> Option<T>(this IValueConverter<T> converter, T defaultValue)
        {
            return new OptionValueConverter<T>(converter, defaultValue);
        }

        public static IValueConverter<ICssValue> Option(this IValueConverter<ICssValue> converter)
        {
            return new OptionValueConverter<ICssValue>(converter, null);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, IValueConverter<T> secondary)
        {
            return new OrValueConverter<T>(primary, secondary);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, String keyword, T value)
        {
            return new OrValueConverter<T>(primary, new IdentifierValueConverter<T>(keyword, value));
        }

        public static IValueConverter<Nullable<T>> ToNullable<T>(this IValueConverter<T> primary)
            where T : struct
        {
            return primary.To(m => new T?(m));
        }

        public static IValueConverter<T> OrDefault<T>(this IValueConverter<T> primary)
        {
            return primary.Or(new IdentifierValueConverter<T>(Keywords.Auto, default(T)));
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
            return new OrderedOptionsConverter<Boolean, T>(
                new IdentifierValueConverter<Boolean>(keyword, true).Required(), converter.Required()).To(m => m.Item2);
        }

        public static IValueConverter<T> StartsWithDelimiter<T>(this IValueConverter<T> converter)
        {
            return new OrderedOptionsConverter<Boolean, T>(delimiter.Required(), converter.Required()).To(m => m.Item2);
        }

        public static IValueConverter<Color> WithCurrentColor(this IValueConverter<Color> converter)
        {
            return converter.Or(Keywords.CurrentColor, Color.Transparent);
        }

        #endregion
    }
}

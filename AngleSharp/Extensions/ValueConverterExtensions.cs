namespace AngleSharp.Extensions
{
    using AngleSharp.DOM.Css;
    using System;

    static class ValueConverterExtensions
    {
        static readonly IValueConverter<Boolean> delimiter = new StructValueConverter<Boolean>(m => m == CssValue.Delimiter ? (Boolean?)true : null);

        public static T TryAll<T>(this IValueConverter<T> converter, CssValueList list, T defaultValue)
        {
            for (int i = 0; i < list.Length; i++)
            {
                if (converter.TryConvert(list[i], tmp => defaultValue = tmp))
                {
                    list.RemoveAt(i);
                    break;
                }
            }

            return defaultValue;
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

        public static IValueConverter<Tuple<T, U>> Optional<T, U>(this IValueConverter<T> listConverter, IValueConverter<U> optionConverter, U defaultValue)
        {
            return new OptionalValueConverter<T, U>(listConverter, optionConverter, defaultValue);
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

        public static IValueConverter<Tuple<T1, T2>> And<T1, T2>(this IValueConverter<T1> primary, IValueConverter<T2> secondary)
        {
            return new AndValueConverter<T1, T2>(primary, secondary);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, IValueConverter<T> secondary)
        {
            return new OrValueConverter<T>(primary, secondary);
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

        public static IValueConverter<U[]> Split<T, U>(this IValueConverter<T> condition, IValueConverter<U> converter)
        {
            return new SplitValueConverter<T, U>(condition, converter);
        }

        public static IValueConverter<T> Constraint<T>(this IValueConverter<T> primary, Predicate<T> constraint)
        {
            return new ConstraintValueConverter<T>(primary, constraint);
        }

        public static IValueConverter<T[]> OptionalSplit<T>(this IValueConverter<T> primary)
        {
            return new SplitValueConverter<Boolean, T>(delimiter, primary, false);
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
    }
}

namespace AngleSharp.Extensions
{
    using AngleSharp.DOM.Css;
    using System;

    static class ValueConverterExtensions
    {
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

        public static IValueConverter<Tuple<T1, T2>> And<T1, T2>(this IValueConverter<T1> primary, IValueConverter<T2> secondary)
        {
            return new AndValueConverter<T1, T2>(primary, secondary);
        }

        public static IValueConverter<T> Or<T>(this IValueConverter<T> primary, IValueConverter<T> secondary)
        {
            return new OrValueConverter<T>(primary, secondary);
        }

        public static IValueConverter<T> Constraint<T>(this IValueConverter<T> primary, Predicate<T> constraint)
        {
            return new ConstraintValueConverter<T>(primary, constraint);
        }
    }
}

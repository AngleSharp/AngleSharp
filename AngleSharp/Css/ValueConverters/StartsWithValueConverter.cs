namespace AngleSharp.Css.ValueConverters
{
    using AngleSharp.Parser.Css;
    using System;
    using System.Collections.Generic;

    sealed class StartsWithValueConverter<T> : IValueConverter<T>
    {
        readonly Predicate<CssToken> _condition;
        readonly IValueConverter<T> _converter;

        public StartsWithValueConverter(Predicate<CssToken> condition, IValueConverter<T> converter)
        {
            _condition = condition;
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            return _converter.TryConvert(Transform(value), setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _converter.Validate(Transform(value));
        }

        IEnumerable<CssToken> Transform(IEnumerable<CssToken> values)
        {
            var enumerator = values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Type != CssTokenType.Whitespace)
                    break;
            }

            if (_condition(enumerator.Current))
            {
                while (enumerator.MoveNext())
                    yield return enumerator.Current;
            }
        }
    }
}

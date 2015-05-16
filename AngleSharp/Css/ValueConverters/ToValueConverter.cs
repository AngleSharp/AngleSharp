namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    sealed class ToValueConverter<T> : IValueConverter<CssValue>
    {
        readonly IValueConverter<T> _converter;

        public ToValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<CssValue> setResult)
        {
            if (Validate(value))
            {
                setResult(new CssValue(value));
                return true;
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _converter.Validate(value);
        }
    }

    sealed class ToValueConverter<T, U> : IValueConverter<U>
    {
        readonly IValueConverter<T> _converter;
        readonly Func<T, U> _next;

        public ToValueConverter(IValueConverter<T> converter, Func<T, U> next)
        {
            _converter = converter;
            _next = next;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<U> setResult)
        {
            return _converter.TryConvert(value, item => setResult(_next(item)));
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _converter.Validate(value);
        }
    }
}

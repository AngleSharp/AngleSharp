namespace AngleSharp.DOM.Css
{
    using System;

    sealed class ToValueConverter<T, U> : IValueConverter<U>
    {
        readonly IValueConverter<T> _converter;
        readonly Func<T, U> _next;

        public ToValueConverter(IValueConverter<T> converter, Func<T, U> next)
        {
            _converter = converter;
            _next = next;
        }

        public Boolean TryConvert(CSSValue value, Action<U> setResult)
        {
            return _converter.TryConvert(value, item => setResult(_next(item)));
        }

        public Boolean Validate(CSSValue value)
        {
            return _converter.Validate(value);
        }
    }
}

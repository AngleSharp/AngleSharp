namespace AngleSharp.DOM.Css
{
    using System;

    sealed class OrValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _previous;
        readonly IValueConverter<T> _next;

        public OrValueConverter(IValueConverter<T> previous, IValueConverter<T> next)
        {
            _previous = previous;
            _next = next;
        }

        public Boolean TryConvert(CSSValue value, Action<T> setResult)
        {
            return _previous.TryConvert(value, setResult) || _next.TryConvert(value, setResult);
        }

        public Boolean Validate(CSSValue value)
        {
            return _previous.Validate(value) || _next.Validate(value);
        }
    }
}

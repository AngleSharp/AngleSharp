namespace AngleSharp.DOM.Css
{
    using System;

    sealed class ClassValueConverter<T> : IValueConverter<T>
        where T : class
    {
        readonly Func<CSSValue, T> _converter;

        public ClassValueConverter(Func<CSSValue, T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(CSSValue value, Action<T> setResult)
        {
            var result = _converter(value);

            if (result == null)
                return false;

            setResult(result);
            return true;
        }

        public Boolean Validate(CSSValue value)
        {
            return _converter(value) != null;
        }
    }
}

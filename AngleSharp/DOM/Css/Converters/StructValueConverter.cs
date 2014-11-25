namespace AngleSharp.DOM.Css
{
    using System;

    sealed class StructValueConverter<T> : IValueConverter<T>
        where T : struct
    {
        readonly Func<CSSValue, T?> _converter;

        public StructValueConverter(Func<CSSValue, T?> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(CSSValue value, Action<T> setResult)
        {
            var result = _converter(value);

            if (!result.HasValue)
                return false;

            setResult(result.Value);
            return true;
        }

        public Boolean Validate(CSSValue value)
        {
            return _converter(value).HasValue;
        }
    }
}

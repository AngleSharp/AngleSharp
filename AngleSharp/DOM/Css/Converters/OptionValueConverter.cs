namespace AngleSharp.DOM.Css
{
    using System;

    sealed class OptionValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;
        readonly T _default;

        public OptionValueConverter(IValueConverter<T> converter, T @default)
        {
            _converter = converter;
            _default = @default;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            if (value == null)
            {
                setResult(_default);
                return true;
            }

            return _converter.TryConvert(value, setResult);
        }

        public Boolean Validate(ICssValue value)
        {
            if (value == null)
                return true;

            return _converter.Validate(value);
        }
    }
}

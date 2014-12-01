namespace AngleSharp.DOM.Css
{
    using System;

    sealed class RequiredValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;

        public RequiredValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            if (value == null)
                return false;

            return _converter.TryConvert(value, setResult);
        }

        public Boolean Validate(ICssValue value)
        {
            if (value == null)
                return false;

            return _converter.Validate(value);
        }
    }
}

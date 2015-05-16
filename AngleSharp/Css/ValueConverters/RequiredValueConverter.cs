namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class RequiredValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;

        public RequiredValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            return value != null && _converter.TryConvert(value, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return value != null && _converter.Validate(value);
        }
    }
}

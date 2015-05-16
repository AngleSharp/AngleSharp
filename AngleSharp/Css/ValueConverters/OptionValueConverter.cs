namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class OptionValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;
        readonly T _default;

        public OptionValueConverter(IValueConverter<T> converter, T @default)
        {
            _converter = converter;
            _default = @default;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            if (value == null)
            {
                setResult(_default);
                return true;
            }

            return _converter.TryConvert(value, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return value == null || _converter.Validate(value);
        }
    }
}

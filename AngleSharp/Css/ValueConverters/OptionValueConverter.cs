namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Parser.Css;

    sealed class OptionValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;
        readonly T _defaultValue;

        public OptionValueConverter(IValueConverter<T> converter, T defaultValue)
        {
            _converter = converter;
            _defaultValue = defaultValue;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return value.Any() == false || _converter.Validate(value);
        }
    }
}

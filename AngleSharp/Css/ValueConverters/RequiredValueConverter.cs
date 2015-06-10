namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Parser.Css;

    sealed class RequiredValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _converter;

        public RequiredValueConverter(IValueConverter<T> converter)
        {
            _converter = converter;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return value.Any() && _converter.Validate(value);
        }
    }
}

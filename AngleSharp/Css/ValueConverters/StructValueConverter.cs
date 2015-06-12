namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AngleSharp.Parser.Css;

    sealed class StructValueConverter<T> : IValueConverter
        where T : struct, IFormattable
    {
        readonly Func<IEnumerable<CssToken>, T?> _converter;

        public StructValueConverter(Func<IEnumerable<CssToken>, T?> converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var val = _converter(value);
            return val.HasValue ? new StructValue(val.Value) : null;
        }

        sealed class StructValue : IPropertyValue
        {
            readonly T _value;

            public StructValue(T value)
            {
                _value = value;
            }

            public String CssText
            {
                get { return _value.ToString(null, CultureInfo.InvariantCulture); }
            }
        }
    }
}

namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

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
            return val.HasValue ? new StructValue(val.Value, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<StructValue>();
        }

        sealed class StructValue : IPropertyValue
        {
            readonly T _value;
            readonly CssValue _original;

            public StructValue(T value, IEnumerable<CssToken> tokens)
            {
                _value = value;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return _value.ToString(null, CultureInfo.InvariantCulture); }
            }

            public CssValue Original
            {
                get { return _original; }
            }

            public CssValue ExtractFor(String name)
            {
                return _original;
            }
        }
    }
}

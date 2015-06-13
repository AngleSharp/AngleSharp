namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Parser.Css;

    sealed class OptionValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;

        public OptionValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return value.Any() ? _converter.Convert(value) : new OptionValue();
        }

        sealed class OptionValue : IPropertyValue
        {
            public String CssText
            {
                get { return String.Empty; }
            }
        }
    }

    sealed class OptionValueConverter<T> : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly T _defaultValue;

        public OptionValueConverter(IValueConverter converter, T defaultValue)
        {
            _converter = converter;
            _defaultValue = defaultValue;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return value.Any() ? _converter.Convert(value) : new OptionValue(_defaultValue);
        }

        sealed class OptionValue : IPropertyValue
        {
            readonly T _value;

            public OptionValue(T value)
            {
                _value = value;
            }

            public String CssText
            {
                get { return String.Empty; }
            }
        }
    }
}

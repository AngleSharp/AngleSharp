namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class AnyValueConverter : IValueConverter
    {
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return new AnyValue(value);
        }

        sealed class AnyValue : IPropertyValue
        {
            readonly IEnumerable<CssToken> _value;

            public AnyValue(IEnumerable<CssToken> value)
            {
                _value = value;
            }

            public String CssText
            {
                get { return _value.ToText(); }
            }
        }
    }
}

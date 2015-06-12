namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class StringValueConverter : IValueConverter
    {
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var str = value.ToCssString();
            return str != null ? new StringValue(str) : null;
        }

        sealed class StringValue : IPropertyValue
        {
            readonly String _value;

            public StringValue(String value)
            {
                _value = value;
            }

            public String CssText
            {
                get { return _value.CssString(); }
            }
        }
    }
}

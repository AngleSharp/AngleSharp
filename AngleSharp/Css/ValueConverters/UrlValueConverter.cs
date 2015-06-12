namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class UrlValueConverter : IValueConverter
    {
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var str = value.ToUri();
            return str != null ? new UrlValue(str) : null;
        }

        sealed class UrlValue : IPropertyValue
        {
            readonly String _value;

            public UrlValue(String value)
            {
                _value = value;
            }

            public String CssText
            {
                get { return _value.CssUrl(); }
            }
        }
    }
}

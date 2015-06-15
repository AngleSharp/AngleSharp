namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    sealed class UrlValueConverter : IValueConverter
    {
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var str = value.ToUri();
            return str != null ? new UrlValue(str, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<UrlValue>();
        }

        sealed class UrlValue : IPropertyValue
        {
            readonly String _value;
            readonly CssValue _original;

            public UrlValue(String value, IEnumerable<CssToken> tokens)
            {
                _value = value;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return _value.CssUrl(); }
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

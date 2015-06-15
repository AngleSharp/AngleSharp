namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    sealed class StringsValueConverter : IValueConverter
    {
        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToItems();
            var n = items.Count;

            if (n % 2 == 0)
            {
                var values = new String[items.Count];

                for (int i = 0; i < n; i++)
                {
                    values[i] = items[i].ToCssString();

                    if (values[i] == null)
                        return null;
                }

                return new StringsValue(values, value);
            }

            return null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<StringsValue>();
        }

        sealed class StringsValue : IPropertyValue
        {
            readonly String[] _values;
            readonly CssValue _original;

            public StringsValue(String[] values, IEnumerable<CssToken> tokens)
            {
                _values = values;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Join(" ", _values.Select(m => m.CssString())); }
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

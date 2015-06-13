namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

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

                return new StringsValue(values);
            }

            return null;
        }

        sealed class StringsValue : IPropertyValue
        {
            readonly String[] _values;

            public StringsValue(String[] values)
            {
                _values = values;
            }

            public String CssText
            {
                get { return String.Join(" ", _values.Select(m => m.CssString())); }
            }
        }
    }
}

namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class ListValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;

        public ListValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToList();
            var values = new IPropertyValue[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                values[i] = _converter.Convert(items[i]);

                if (values[i] == null)
                    return null;
            }

            return new ListValue(values);
        }

        sealed class ListValue : IPropertyValue
        {
            readonly IPropertyValue[] _values;

            public ListValue(IPropertyValue[] values)
            {
                _values = values;
            }

            public String CssText
            {
                get { return String.Join(", ", _values.Select(m => m.CssText)); }
            }
        }
    }
}

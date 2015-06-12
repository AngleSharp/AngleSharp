namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class OneOrMoreValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;
        readonly Int32 _minimum;
        readonly Int32 _maximum;

        public OneOrMoreValueConverter(IValueConverter converter, Int32 minimum, Int32 maximum)
        {
            _converter = converter;
            _minimum = minimum;
            _maximum = maximum;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToItems();
            var n = items.Count;

            if (n >= _minimum && n <= _maximum)
            {
                var values = new IPropertyValue[items.Count];

                for (int i = 0; i < n; i++)
                {
                    values[i] = _converter.Convert(items[i]);

                    if (values[i] == null)
                        return null;
                }

                return new MultipleValue(values);
            }

            return null;
        }

        sealed class MultipleValue : IPropertyValue
        {
            readonly IPropertyValue[] _values;

            public MultipleValue(IPropertyValue[] values)
            {
                _values = values;
            }

            public String CssText
            {
                get { return String.Join(" ", _values.Select(m => m.CssText)); }
            }
        }
    }
}

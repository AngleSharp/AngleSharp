namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
using AngleSharp.Dom.Css;

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

            return new ListValue(values, value);
        }

        sealed class ListValue : IPropertyValue
        {
            readonly IPropertyValue[] _values;
            readonly CssValue _value;

            public ListValue(IPropertyValue[] values, IEnumerable<CssToken> tokens)
            {
                _values = values;
                _value = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Join(", ", _values.Select(m => m.CssText)); }
            }

            public CssValue Original
            {
                get { return _value; }
            }

            public CssValue ExtractFor(String name)
            {
                var tokens = new List<CssToken>();

                foreach (var value in _values)
                {
                    var extracted = value.ExtractFor(name);

                    if (extracted != null)
                    {
                        if (tokens.Count > 0)
                            tokens.Add(new CssToken(CssTokenType.Comma, ',', TextPosition.Empty));

                        tokens.AddRange(extracted);
                    }
                }

                return new CssValue(tokens);
            }
        }
    }
}

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

            return values.Length != 1 ? new ListValue(values, value) : values[0];
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            var result = properties.Guard<ListValue>();

            if (result == null)
            {
                var valueList = new List<List<CssToken>>[properties.Length];
                var dummies = new CssProperty[properties.Length];
                var max = 0;

                for (var i = 0; i < properties.Length; i++)
                {
                    var value = properties[i].DeclaredValue;
                    valueList[i] = value != null ? value.Original.ToList() : new List<List<CssToken>>();
                    dummies[i] = Factory.Properties.CreateLonghand(properties[i].Name);
                    max = Math.Max(max, valueList[i].Count);
                }

                var values = new IPropertyValue[max];

                for (int i = 0; i < max; i++)
                {
                    for (int j = 0; j < dummies.Length; j++)
                    {
                        var list = valueList[j];
                        var tokens = list.Count > i ? list[i] : Enumerable.Empty<CssToken>();
                        dummies[j].TrySetValue(new CssValue(tokens));
                    }

                    values[i] = _converter.Construct(dummies);
                }

                result = new ListValue(values, Enumerable.Empty<CssToken>());
            }

            return result;
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
                            tokens.Add(CssToken.Comma);

                        tokens.AddRange(extracted);
                    }
                }

                return new CssValue(tokens);
            }
        }
    }
}

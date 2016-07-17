namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class EndListValueConverter : IValueConverter
    {
        readonly IValueConverter _listConverter;
        readonly IValueConverter _endConverter;

        public EndListValueConverter(IValueConverter listConverter, IValueConverter endConverter)
        {
            _listConverter = listConverter;
            _endConverter = endConverter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var items = value.ToList();
            var n = items.Count - 1;
            var values = new IPropertyValue[n + 1];

            for (var i = 0; i < n; i++)
            {
                values[i] = _listConverter.Convert(items[i]);

                if (values[i] == null)
                {
                    return null;
                }
            }

            values[n] = _endConverter.Convert(items[n]);
            return values[n] != null ? new ListValue(values, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
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

            for (var i = 0; i < max; i++)
            {
                for (var j = 0; j < dummies.Length; j++)
                {
                    var list = valueList[j];
                    var tokens = list.Count > i ? list[i] : Enumerable.Empty<CssToken>();
                    dummies[j].TrySetValue(new CssValue(tokens));
                }

                var converter = (i < max - 1) ? _listConverter : _endConverter;
                values[i] = converter.Construct(dummies);
            }

            return new ListValue(values, Enumerable.Empty<CssToken>());
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
                        {
                            tokens.Add(CssToken.Whitespace);
                        }

                        tokens.AddRange(extracted);
                    }
                }

                return new CssValue(tokens);
            }
        }
    }
}

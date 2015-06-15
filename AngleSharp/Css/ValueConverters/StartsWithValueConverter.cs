namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    sealed class StartsWithValueConverter : IValueConverter
    {
        readonly CssTokenType _type;
        readonly String _data;
        readonly IValueConverter _converter;

        public StartsWithValueConverter(CssTokenType type, String data, IValueConverter converter)
        {
            _type = type;
            _data = data;
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var rest = Transform(value);
            return rest != null ? CreateFrom(_converter.Convert(rest), value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            var value = _converter.Construct(properties);
            return value != null ? CreateFrom(value, Enumerable.Empty<CssToken>()) : null;
        }

        IPropertyValue CreateFrom(IPropertyValue value, IEnumerable<CssToken> tokens)
        {
            return value != null ? new StartValue(_data, value, tokens) : null;
        }

        List<CssToken> Transform(IEnumerable<CssToken> values)
        {
            var enumerator = values.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (enumerator.Current.Type != CssTokenType.Whitespace)
                    break;
            }

            if (enumerator.Current.Type == _type && enumerator.Current.Data.Equals(_data, StringComparison.OrdinalIgnoreCase))
            {
                var list = new List<CssToken>();

                while (enumerator.MoveNext())
                {
                    if (enumerator.Current.Type == CssTokenType.Whitespace && list.Count == 0)
                        continue;

                    list.Add(enumerator.Current);
                }

                return list;
            }

            return null;
        }

        sealed class StartValue : IPropertyValue
        {
            readonly String _start;
            readonly IPropertyValue _value;
            readonly CssValue _original;

            public StartValue(String start, IPropertyValue value, IEnumerable<CssToken> tokens)
            {
                _start = start;
                _value = value;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return String.Concat(_start, " ", _value.CssText); }
            }

            public CssValue Original
            {
                get { return _original; }
            }

            public CssValue ExtractFor(String name)
            {
                return _value.ExtractFor(name);
            }
        }
    }
}

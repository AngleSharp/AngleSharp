namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
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
            return rest != null ? CreateFrom(_converter.Convert(rest)) : null;
        }

        IPropertyValue CreateFrom(IPropertyValue value)
        {
            return value != null ? new StartValue(_data, value) : null;
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

            public StartValue(String start, IPropertyValue value)
            {
                _start = start;
                _value = value;
            }

            public String CssText
            {
                get { return String.Concat(_start, " ", _value.CssText); }
            }
        }
    }
}

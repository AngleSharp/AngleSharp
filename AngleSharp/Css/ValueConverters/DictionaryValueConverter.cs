namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class DictionaryValueConverter<T> : IValueConverter
    {
        readonly Dictionary<String, T> _values;

        public DictionaryValueConverter(Dictionary<String, T> values)
        {
            _values = values;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var identifier = value.ToIdentifier();
            var mode = default(T);
            return identifier != null && _values.TryGetValue(identifier, out mode) ?
                new EnumeratedValue(identifier, mode) : null;
        }

        sealed class EnumeratedValue : IPropertyValue
        {
            readonly String _identifier;
            readonly T _value;

            public EnumeratedValue(String identifier, T value)
            {
                _identifier = identifier;
                _value = value;
            }

            public String CssText
            {
                get { return _identifier; }
            }
        }
    }
}

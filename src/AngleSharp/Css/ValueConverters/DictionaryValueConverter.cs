namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

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
                new EnumeratedValue(identifier, mode, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<EnumeratedValue>();
        }

        sealed class EnumeratedValue : IPropertyValue
        {
            readonly String _identifier;
            readonly T _value;
            readonly CssValue _original;

            public EnumeratedValue(String identifier, T value, IEnumerable<CssToken> tokens)
            {
                _identifier = identifier;
                _value = value;
                _original = new CssValue(tokens);
            }

            public String CssText
            {
                get { return _identifier; }
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

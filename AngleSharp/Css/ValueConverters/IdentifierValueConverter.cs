namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class IdentifierValueConverter : IValueConverter
    {
        readonly Func<IEnumerable<CssToken>, String> _converter;

        public IdentifierValueConverter(Func<IEnumerable<CssToken>, String> converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var result = _converter(value);
            return result != null ? new IdentifierValue(result) : null;
        }

        sealed class IdentifierValue : IPropertyValue
        {
            readonly String _identifier;

            public IdentifierValue(String identifier)
            {
                _identifier = identifier;
            }

            public String CssText
            {
                get { return _identifier; }
            }
        }
    }

    sealed class IdentifierValueConverter<T> : IValueConverter
    {
        readonly String _identifier;
        readonly T _result;

        public IdentifierValueConverter(String identifier, T result)
        {
            _identifier = identifier;
            _result = result;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return value.Is(_identifier) ? 
                new IdentifierValue(_identifier, _result) : null;
        }

        sealed class IdentifierValue : IPropertyValue
        {
            readonly String _identifier;
            readonly T _value;

            public IdentifierValue(String identifier, T value)
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

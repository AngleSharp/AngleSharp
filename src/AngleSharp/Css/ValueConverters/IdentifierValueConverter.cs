namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

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
            return result != null ? new IdentifierValue(result, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<IdentifierValue>();
        }

        sealed class IdentifierValue : IPropertyValue
        {
            readonly String _identifier;
            readonly CssValue _value;

            public IdentifierValue(String identifier, IEnumerable<CssToken> tokens)
            {
                _identifier = identifier;
                _value = new CssValue(tokens);
            }

            public String CssText
            {
                get { return _identifier; }
            }

            public CssValue Original
            {
                get { return _value; }
            }

            public CssValue ExtractFor(String name)
            {
                return _value;
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
            return value.Is(_identifier) ? new IdentifierValue(_identifier, _result, value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<IdentifierValue>();
        }

        sealed class IdentifierValue : IPropertyValue
        {
            readonly String _identifier;
            readonly T _value;
            readonly CssValue _original;

            public IdentifierValue(String identifier, T value, IEnumerable<CssToken> tokens)
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

namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    sealed class FunctionValueConverter : IValueConverter
    {
        readonly String _name;
        readonly IValueConverter _arguments;

        public FunctionValueConverter(String name, IValueConverter arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            var function = value.OnlyOrDefault() as CssFunctionToken;

            if (Check(function))
            {
                var args = _arguments.Convert(function.ArgumentTokens);
                return args != null ? new FunctionValue(_name, args, value) : null;
            }

            return null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return properties.Guard<FunctionValue>();
        }

        Boolean Check(CssFunctionToken function)
        {
            return function != null && function.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
        }

        sealed class FunctionValue : IPropertyValue
        {
            readonly String _name;
            readonly IPropertyValue _arguments;
            readonly CssValue _value;

            public FunctionValue(String name, IPropertyValue arguments, IEnumerable<CssToken> tokens)
            {
                _name = name;
                _arguments = arguments;
                _value = new CssValue(tokens);
            }

            public String CssText
            {
                get { return _name.CssFunction(_arguments.CssText); }
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
}

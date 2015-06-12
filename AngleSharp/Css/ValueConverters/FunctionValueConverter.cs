namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

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
                return args != null ? new FunctionValue(_name, args) : null;
            }

            return null;
        }

        Boolean Check(CssFunctionToken function)
        {
            return function != null && function.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
        }

        sealed class FunctionValue : IPropertyValue
        {
            readonly String _name;
            readonly IPropertyValue _arguments;

            public FunctionValue(String name, IPropertyValue arguments)
            {
                _name = name;
                _arguments = arguments;
            }

            public String CssText
            {
                get { return String.Concat(_name, "(", _arguments.CssText, ")"); }
            }
        }
    }
}

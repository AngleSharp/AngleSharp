namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Extensions;
    using AngleSharp.Parser.Css;

    sealed class FunctionValueConverter<T> : IValueConverter<T>
    {
        readonly String _name;
        readonly IValueConverter<T> _arguments;

        public FunctionValueConverter(String name, IValueConverter<T> arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        public Boolean TryConvert(IEnumerable<CssToken> value, Action<T> setResult)
        {
            var function = value.OnlyOrDefault() as CssFunctionToken;
            return Check(function) && _arguments.TryConvert(function.ArgumentTokens, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var function = value.OnlyOrDefault() as CssFunctionToken;
            return Check(function) && _arguments.Validate(function.ArgumentTokens);
        }

        Boolean Check(CssFunctionToken function)
        {
            return function != null && function.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
        }
    }
}

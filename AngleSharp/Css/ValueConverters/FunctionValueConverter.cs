namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
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
            var f = value as CssValue;

            if (f == null || f.Count < 2 || f[0].Type != CssTokenType.Function || f[0].Data.Equals(_name, StringComparison.OrdinalIgnoreCase) == false || f[f.Count - 1].Type != CssTokenType.RoundBracketClose)
                return false;

            var parameters = new CssValue(f.Skip(1).Take(f.Count - 2));
            return _arguments.TryConvert(parameters, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var f = value as CssValue;
            return f != null && f.Count >= 2 && 
                f[0].Type == CssTokenType.Function && 
                f[0].Data.Equals(_name, StringComparison.OrdinalIgnoreCase) && 
                f[f.Count - 1].Type == CssTokenType.RoundBracketClose &&
                _arguments.Validate(new CssValue(f.Skip(1).Take(f.Count - 2)));
        }
    }
}

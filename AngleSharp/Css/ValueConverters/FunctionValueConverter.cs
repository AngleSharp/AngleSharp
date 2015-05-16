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
            if (value.Count() >= 2 && IsFunction(value.First()) && value.Last().Type == CssTokenType.RoundBracketClose)
            {
                return _arguments.TryConvert(value.Skip(1).Take(value.Count() - 2), setResult);
            }

            return false;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return value.Count() >= 2 && IsFunction(value.First()) && 
                value.Last().Type == CssTokenType.RoundBracketClose &&
                _arguments.Validate(value.Skip(1).Take(value.Count() - 2));
        }

        Boolean IsFunction(CssToken value)
        {
            return value.Type == CssTokenType.Function && value.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
        }
    }
}

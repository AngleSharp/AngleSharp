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
            var args = ExtractArguments(value);
            return args != null && _arguments.TryConvert(args, setResult);
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            var args = ExtractArguments(value);
            return args != null && _arguments.Validate(args);
        }

        List<CssToken> ExtractArguments(IEnumerable<CssToken> value)
        {
            var iter = value.GetEnumerator();

            if (iter.MoveNext() && IsFunction(iter.Current))
            {
                var tokens = new List<CssToken>();

                while (iter.MoveNext())
                {
                    tokens.Add(iter.Current);
                }

                if (tokens.Count > 0 && tokens[tokens.Count - 1].Type == CssTokenType.RoundBracketClose)
                {
                    return tokens;
                }
            }

            return null;
        }

        Boolean IsFunction(CssToken value)
        {
            return value.Type == CssTokenType.Function && value.Data.Equals(_name, StringComparison.OrdinalIgnoreCase);
        }
    }
}

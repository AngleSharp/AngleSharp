namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    sealed class FunctionValueConverter<T> : IValueConverter<T>
    {
        readonly String _name;
        readonly IValueConverter<T> _arguments;

        public FunctionValueConverter(String name, IValueConverter<T> arguments)
        {
            _name = name;
            _arguments = arguments;
        }

        public Boolean TryConvert(ICssValue value, Action<T> setResult)
        {
            var f = value as CSSFunction;

            if (f == null || !f.Name.Equals(_name, StringComparison.OrdinalIgnoreCase))
                return false;

            return _arguments.TryConvert(Transform(f.Arguments), setResult);
        }

        public Boolean Validate(ICssValue value)
        {
            var f = value as CSSFunction;
            return f != null && f.Name.Equals(_name, StringComparison.OrdinalIgnoreCase) && _arguments.Validate(Transform(f.Arguments));
        }

        static ICssValue Transform(List<ICssValue> arguments)
        {
            return arguments.Count == 1 ? arguments[0] : new CSSValueList(arguments);
        }
    }
}

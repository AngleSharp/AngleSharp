namespace AngleSharp.Css.ValueConverters
{
    using System;
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class OrValueConverter<T> : IValueConverter<T>
    {
        readonly IValueConverter<T> _previous;
        readonly IValueConverter<T> _next;

        public OrValueConverter(IValueConverter<T> previous, IValueConverter<T> next)
        {
            _previous = previous;
            _next = next;
        }

        public Boolean Validate(IEnumerable<CssToken> value)
        {
            return _previous.Validate(value) || _next.Validate(value);
        }
    }
}

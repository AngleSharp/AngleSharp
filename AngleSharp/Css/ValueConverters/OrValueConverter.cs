namespace AngleSharp.Css.ValueConverters
{
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    sealed class OrValueConverter : IValueConverter
    {
        readonly IValueConverter _previous;
        readonly IValueConverter _next;

        public OrValueConverter(IValueConverter previous, IValueConverter next)
        {
            _previous = previous;
            _next = next;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return _previous.Convert(value) ?? _next.Convert(value);
        }
    }
}

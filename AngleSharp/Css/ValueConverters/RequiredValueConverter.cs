namespace AngleSharp.Css.ValueConverters
{
    using System.Collections.Generic;
    using System.Linq;
    using AngleSharp.Dom.Css;
    using AngleSharp.Parser.Css;

    sealed class RequiredValueConverter : IValueConverter
    {
        readonly IValueConverter _converter;

        public RequiredValueConverter(IValueConverter converter)
        {
            _converter = converter;
        }

        public IPropertyValue Convert(IEnumerable<CssToken> value)
        {
            return value.Any() ? _converter.Convert(value) : null;
        }

        public IPropertyValue Construct(CssProperty[] properties)
        {
            return _converter.Construct(properties);
        }
    }
}

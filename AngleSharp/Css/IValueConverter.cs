namespace AngleSharp.Css
{
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;

    interface IValueConverter
    {
        IPropertyValue Convert(IEnumerable<CssToken> value);
    }
}

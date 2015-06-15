namespace AngleSharp.Css
{
    using System.Collections.Generic;
    using AngleSharp.Parser.Css;
    using AngleSharp.Dom.Css;

    interface IValueConverter
    {
        IPropertyValue Convert(IEnumerable<CssToken> value);

        IPropertyValue Construct(CssProperty[] properties);
    }

    static class PropertyExtensions
    {
        public static IPropertyValue Guard<T>(this CssProperty[] properties)
        {
            if (properties.Length == 1)
            {
                var value = properties[0].DeclaredValue;
                return value is T ? properties[0].DeclaredValue : null;
            }

            return null;
        }
    }
}

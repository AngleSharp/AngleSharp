namespace AngleSharp.Css
{
    using AngleSharp.Dom.Css;
    using System;

    interface IPropertyValue
    {
        String CssText { get; }

        CssValue Original { get; }

        CssValue ExtractFor(String name);
    }
}

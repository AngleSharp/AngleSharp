namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a margin CSS rule (e.g. in an @page at-rule).
    /// </summary>
    [DomName("CSSMarginRule")]
    public interface ICssMarginRule : ICssRule
    {
        /// <summary>
        /// Gets the name of the margin rule.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the style object for the margin at-rule.
        /// </summary>
        [DomName("style")]
        [DomPutForwards("cssText")]
        ICssStyleDeclaration Style { get; }
    }
}

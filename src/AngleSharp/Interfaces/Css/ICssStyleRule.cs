namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a general CSS styling rule.
    /// </summary>
    [DomName("CSSStyleRule")]
    public interface ICssStyleRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the textual representation of the selector for this rule, e.g. "h1,h2".
        /// </summary>
        [DomName("selectorText")]
        String SelectorText { get; set; }

        /// <summary>
        /// Gets the CSSStyleDeclaration object for the rule.
        /// </summary>
        [DomName("style")]
        [DomPutForwards("cssText")]
        ICssStyleDeclaration Style { get; }

        /// <summary>
        /// Gets or sets the selector for matching elements.
        /// </summary>
        ISelector Selector { get; set; }
    }
}

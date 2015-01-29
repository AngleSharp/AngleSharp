namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the base for any CSS value.
    /// </summary>
    [DomName("CSSValue")]
    public interface ICssValue
    {
        /// <summary>
        /// Gets a code defining the type of the value as defined above.
        /// </summary>
        [DomName("cssValueType")]
        CssValueType Type { get; }

        /// <summary>
        /// Gets or sets a string representation of the current value.
        /// </summary>
        [DomName("cssText")]
        String CssText { get; }
    }
}

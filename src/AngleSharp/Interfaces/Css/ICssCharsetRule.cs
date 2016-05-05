namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @charset CSS rule.
    /// </summary>
    [DomName("CSSCharsetRule")]
    public interface ICssCharsetRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the character set to use.
        /// </summary>
        [DomName("encoding")]
        String CharacterSet { get; set; }
    }
}

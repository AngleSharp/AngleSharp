namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @font-face CSS rule.
    /// </summary>
    [DomName("CSSFontFaceRule")]
    public interface ICssFontFaceRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the font-family.
        /// </summary>
        [DomName("family")]
        String Family { get; set; }

        /// <summary>
        /// Gets or sets the source of the font.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets or sets the style of the font.
        /// </summary>
        [DomName("style")]
        String Style { get; set; }

        /// <summary>
        /// Gets or sets the weight of the font.
        /// </summary>
        [DomName("weight")]
        String Weight { get; set; }

        /// <summary>
        /// Gets or sets the stretch value of the font.
        /// </summary>
        [DomName("stretch")]
        String Stretch { get; set; }

        /// <summary>
        /// Gets or sets the unicode range of the font.
        /// </summary>
        [DomName("unicodeRange")]
        String Range { get; set; }

        /// <summary>
        /// Gets or sets the variant of the font.
        /// </summary>
        [DomName("variant")]
        String Variant { get; set; }

        /// <summary>
        /// Gets or sets the feature settings of the font.
        /// </summary>
        [DomName("featureSettings")]
        String Features { get; set; }
    }
}

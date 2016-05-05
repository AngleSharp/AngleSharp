namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a @font-feature-values CSS rule.
    /// </summary>
    [DomName("CSSFontFeatureValuesRule")]
    public interface ICssFontFeatureValuesRule : ICssRule
    {
        /// <summary>
        /// Gets or sets the list of one or more font families for
        /// which a given set of feature values is defined.
        /// </summary>
        [DomName("fontFamily")]
        String Family { get; set; }

        //More information available at:
        //http://dev.w3.org/csswg/css-fonts/#om-fontfeaturevalues
    }
}

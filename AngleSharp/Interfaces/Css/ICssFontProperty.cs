namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS font-family property.
    /// </summary>
    public interface ICssFontFamilyProperty : ICssProperty
    {
        /// <summary>
        /// Gets an enumeration over all font names.
        /// </summary>
        IEnumerable<String> Families { get; }
    }

    /// <summary>
    /// Represents the CSS font-variant property.
    /// </summary>
    public interface ICssFontVariantProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font variant transformation, if any.
        /// </summary>
        FontVariant Variant { get; }
    }

    /// <summary>
    /// Represents the CSS font-style property.
    /// </summary>
    public interface ICssFontStyleProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font style.
        /// </summary>
        FontStyle Style { get; }
    }
    
    /// <summary>
    /// Represents the CSS font-stretch property.
    /// </summary>
    public interface ICssFontStretchProperty : ICssProperty
    {
        /// <summary>
        /// Gets the selected font stretch setting.
        /// </summary>
        FontStretch Stretch { get; }
    }

    /// <summary>
    /// Represents the CSS font-size property.
    /// </summary>
    public interface ICssFontSizeProperty : ICssProperty
    {
        /// <summary>
        /// Gets the set font-size mode.
        /// </summary>
        FontSize SizingMode { get; }

        /// <summary>
        /// Gets the font-size.
        /// </summary>
        IDistance Size { get; }
    }

    /// <summary>
    /// Represents the CSS font-weight property.
    /// </summary>
    public interface ICssFontWeightProperty : ICssProperty
    {
        /// <summary>
        /// Numeric font weights for fonts that provide more than just normal and bold. If the exact
        /// weight given is unavailable, then 600-900 use the closest available darker weight
        /// (or, if there is none, the closest available lighter weight), and 100-500 use the closest
        /// available lighter weight (or, if there is none, the closest available darker weight). This
        /// means that for fonts that provide only normal and bold, 100-500 are normal, and 600-900 are
        /// bold.
        /// </summary>
        Int32 Weight { get; }

        /// <summary>
        /// Gets if the given value should be considered relative to the current one.
        /// </summary>
        Boolean IsRelative { get; }
    }

    /// <summary>
    /// Represents the CSS line-height property.
    /// </summary>
    public interface ICssLineHeightProperty : ICssProperty
    {
    }

    /// <summary>
    /// Represents the CSS font shorthand property.
    /// </summary>
    public interface ICssFontProperty : ICssProperty, ICssFontStyleProperty, ICssFontVariantProperty, ICssFontWeightProperty, ICssFontStretchProperty, ICssFontSizeProperty, ICssFontFamilyProperty, ICssLineHeightProperty
    {
    }
}

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
        /// Gets the font-size mode.
        /// </summary>
        FontSize Mode { get; }

        /// <summary>
        /// Gets the custom set font-size, if any.
        /// </summary>
        IDistance Size { get; }
    }

    /// <summary>
    /// Represents the CSS font-weight property.
    /// </summary>
    public interface ICssFontWeightProperty : ICssProperty
    {
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

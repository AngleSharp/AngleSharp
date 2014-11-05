namespace AngleSharp.Css
{
    using System;

    /// <summary>
    /// Defines some properties of a CSS property.
    /// Exclusive maximum is 0x100.
    /// Inclusive maximum is 0x0FF.
    /// </summary>
    [Flags]
    enum PropertyFlags : byte
    {
        /// <summary>
        /// No special properties.
        /// </summary>
        None = 0x0,
        /// <summary>
        /// The property can be inherited.
        /// </summary>
        Inherited = 0x1,
        /// <summary>
        /// The property supports hashless colors in quirks mode.
        /// </summary>
        Hashless = 0x2,
        /// <summary>
        /// The property supports unitless lengths in quirks mode.
        /// </summary>
        Unitless = 0x4,
        /// <summary>
        /// The property is supported in animations.
        /// </summary>
        Animatable = 0x8,
        /// <summary>
        /// The property is actually a shorthand.
        /// </summary>
        Shorthand = 0x10
    }
}

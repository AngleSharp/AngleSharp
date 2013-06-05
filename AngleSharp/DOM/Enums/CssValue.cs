using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// This enumeration is indicating which type of unit applies to the value.
    /// </summary>
    public enum CssValue : ushort
    {
        /// <summary>
        /// The value is inherited and the CssText contains "inherit".
        /// </summary>
        Inherit = 0,
        /// <summary>
        /// The value is a primitive value and an instance of the CSSPrimitiveValue.
        /// </summary>
        PrimitiveValue = 1,
        /// <summary>
        /// The value is a CSSValue list and an instance of the CSSValueList.
        /// </summary>
        ValueList = 2,
        /// <summary>
        /// The value is a custom value.
        /// </summary>
        Custom = 3
    }
}

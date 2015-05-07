namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;

    /// <summary>
    /// This enumeration is indicating which type of unit applies to the value.
    /// </summary>
    public enum CssValueType : ushort
    {
        /// <summary>
        /// The value is inherited and the CssText contains "inherit".
        /// </summary>
        [DomName("CSS_INHERIT")]
        Inherit = 0,
        /// <summary>
        /// The value is a primitive value.
        /// </summary>
        [DomName("CSS_PRIMITIVE_VALUE")]
        Primitive = 1,
        /// <summary>
        /// The value is a list of values.
        /// </summary>
        [DomName("CSS_VALUE_LIST")]
        List = 2,
        /// <summary>
        /// The value is a custom value.
        /// </summary>
        [DomName("CSS_CUSTOM")]
        Custom = 3,
        /// <summary>
        /// The value is in its initial state and might be inherited, if applicable.
        /// </summary>
        Initial = 4
    }
}

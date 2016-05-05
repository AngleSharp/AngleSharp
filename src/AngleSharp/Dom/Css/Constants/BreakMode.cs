namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration over all possible break modes.
    /// </summary>
    public enum BreakMode : ushort
    {
        /// <summary>
        /// Initial value. Allows, meaning neither forbid nor force, any break
        /// (either page, column or region) to be be inserted before (after, inside)
        /// the principle box.
        /// </summary>
        Auto,
        /// <summary>
        /// Always force page breaks before (after) the principle box. This is a
        /// synonym of page, it has been kept to facilitate transition from
        /// page-break-before which is subset of this property.
        /// </summary>
        Always,
        /// <summary>
        /// Prevent any break, either page, column or region, to be inserted right
        /// before (after, inside) the principle box.
        /// </summary>
        Avoid,
        /// <summary>
        /// Force one or two page breaks right before (after) the principle box so
        /// that the next page is formatted as a left page.
        /// </summary>
        Left,
        /// <summary>
        /// Force one or two page breaks right before (after) the principle box so
        /// that the next page is formatted as a right page.
        /// </summary>
        Right,
        /// <summary>
        /// Always force one page break right before (after) the principle box.
        /// </summary>
        Page,
        /// <summary>
        /// Always force one column break right before (after) the principle box.
        /// </summary>
        Column,
        /// <summary>
        /// Avoid any page break right before (after, inside) the principle box.
        /// </summary>
        AvoidPage,
        /// <summary>
        /// Avoid any column break right before (after, inside) the principle box.
        /// </summary>
        AvoidColumn,
        /// <summary>
        /// Avoid a region break before (after, inside) the generated box.
        /// </summary>
        AvoidRegion
    }
}

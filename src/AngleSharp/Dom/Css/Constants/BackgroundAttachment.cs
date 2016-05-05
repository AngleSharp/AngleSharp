namespace AngleSharp.Dom.Css
{
    /// <summary>
    /// An enumeration with all background attachment settings.
    /// </summary>
    public enum BackgroundAttachment : ushort
    {
        /// <summary>
        /// This keyword means that the background is fixed with regard to the viewport.
        /// Even if an element has a scrolling mechanism, a ‘fixed’ background doesn't
        /// move with the element.
        /// </summary>
        Fixed,
        /// <summary>
        /// This keyword means that the background is fixed with regard to the element's
        /// contents: if the element has a scrolling mechanism, the background scrolls
        /// with the element's contents, and the background painting area and background
        /// positioning area are relative to the scrollable area of the element rather
        /// than to the border framing them.
        /// </summary>
        Local,
        /// <summary>
        /// This keyword means that the background is fixed with regard to the element
        /// itself and does not scroll with its contents. (It is effectively attached
        /// to the element's border.)
        /// </summary>
        Scroll
    }
}

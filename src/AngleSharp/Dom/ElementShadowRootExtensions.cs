namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;

    /// <summary>
    /// The shadow root of an element as scripts see it.
    /// </summary>
    [DomExposed("Element")]
    public static class ElementShadowRootExtensions
    {
        /// <summary>
        /// Gets the shadow root of the element if it is open: a closed one is hidden from the
        /// outside (DOM 4.9.2).
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The open shadow root, if any.</returns>
        [DomName("shadowRoot")]
        [DomAccessor(Accessors.Getter)]
        public static IShadowRoot? GetShadowRoot(this IElement element) =>
            element.ShadowRoot is { Mode: ShadowRootMode.Open } shadowRoot ? shadowRoot : null;
    }
}

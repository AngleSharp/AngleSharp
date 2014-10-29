namespace AngleSharp.DOM.Html
{
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLAppletElement : HTMLElement
    {
        internal HTMLAppletElement()
            : base(Tags.Applet, NodeFlags.Special | NodeFlags.Scoped)
        {
        }
    }
}

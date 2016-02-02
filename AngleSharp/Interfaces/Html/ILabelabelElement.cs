namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;

    /// <summary>
    /// This interface marks elements that allow to have an attached label.
    /// </summary>
    [DomNoInterfaceObject]
    public interface ILabelabelElement
    {
        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DomName("labels")]
        INodeList Labels { get; }
    }
}

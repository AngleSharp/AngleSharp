namespace AngleSharp.Browser
{
    using AngleSharp.Html.Dom;

    /// <summary>
    /// Defines the interface to be used for handling meta data.
    /// </summary>
    public interface IMetaHandler
    {
        /// <summary>
        /// Handles the content of the given HTML meta element.
        /// </summary>
        /// <param name="element">The meta element.</param>
        void HandleContent(IHtmlMetaElement element);
    }
}

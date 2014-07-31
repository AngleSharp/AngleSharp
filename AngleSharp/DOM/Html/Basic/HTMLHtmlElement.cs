namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML html element.
    /// </summary>
    sealed class HTMLHtmlElement : HTMLElement, IHtmlHtmlElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML html tag.
        /// </summary>
        internal HTMLHtmlElement()
            : base(Tags.Html, NodeFlags.Special | NodeFlags.ImplicitelyClosed | NodeFlags.Scoped | NodeFlags.HtmlTableScoped | NodeFlags.HtmlTableSectionScoped)
        {
        }

        #endregion

        #region Internal Methods

        internal void ApplyManifest()
        {
            //TODO
            //If the Document is being loaded as part of navigation of a browsing context, then:
            //  if the newly created element has a manifest attribute whose value is not the empty string,
            //    then resolve the value of that attribute to an absolute URL, relative to the newly created element,
            //    and if that is successful, run the application cache selection algorithm with the result of applying
            //    the URL serializer algorithm to the resulting parsed URL with the exclude fragment flag set;
            //  otherwise, if there is no such attribute, or its value is the empty string, or resolving its value fails,
            //    run the application cache selection algorithm with no manifest. The algorithm must be passed the Document object.
        }

        #endregion
    }
}

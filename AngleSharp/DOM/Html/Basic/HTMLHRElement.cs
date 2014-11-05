namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HTMLHRElement : HTMLElement, IHtmlHrElement
    {
        #region ctor

        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        public HTMLHRElement()
            : base(Tags.Hr, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}

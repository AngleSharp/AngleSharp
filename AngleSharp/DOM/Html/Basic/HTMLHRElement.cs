namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the hr element.
    /// </summary>
    sealed class HTMLHRElement : HTMLElement, IHtmlHrElement
    {
        #region ctor

        /// <summary>
        /// Creates a new hr element.
        /// </summary>
        internal HTMLHRElement()
            : base(Tags.Hr, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion
    }
}

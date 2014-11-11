namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;

    /// <summary>
    /// Represents the HTML head element.
    /// </summary>
    sealed class HTMLHeadElement : HTMLElement, IHtmlHeadElement
    {
        #region ctor

        public HTMLHeadElement()
            : base(Tags.Head, NodeFlags.Special)
        {
        }

        #endregion

        #region Methods

        internal override void Close()
        {
            base.Close();
            Owner.WaitForReady();
        }

        #endregion
    }
}
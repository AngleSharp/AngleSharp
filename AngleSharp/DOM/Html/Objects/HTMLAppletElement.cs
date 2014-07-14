namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML applet element.
    /// </summary>
    [DomHistorical]
    sealed class HTMLAppletElement : HTMLElement, IScopeElement
    {
        internal HTMLAppletElement()
        {
            _name = Tags.Applet;
        }

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }
    }
}

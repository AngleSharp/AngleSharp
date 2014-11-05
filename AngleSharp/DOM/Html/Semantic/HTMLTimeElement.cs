namespace AngleSharp.DOM.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// The time HTML element.
    /// </summary>
    sealed class HTMLTimeElement : HTMLElement, IHtmlTimeElement
    {
        #region ctor

        internal HTMLTimeElement()
            : base(Tags.Time, NodeFlags.Special)
        {
        }

        #endregion

        #region Properties

        public String DateTime
        {
            get { return GetAttribute("datetime"); }
            set { SetAttribute("datetime", value); }
        }

        #endregion
    }
}

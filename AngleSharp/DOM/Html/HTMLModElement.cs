namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML modifier (ins / del) element.
    /// </summary>
    sealed class HTMLModElement : HTMLElement, IHtmlModElement
    {
        #region ctor

        public HTMLModElement(Document owner, String name)
            : base(name)
        {
            Owner = owner;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value that contains a URI of a resource
        /// explaining the change.
        /// </summary>
        public String Citation
        {
            get { return GetAttribute("cite"); }
            set { SetAttribute("cite", value); }
        }

        /// <summary>
        /// Gets or sets the value that contains date-and-time string
        /// representing a timestamp for the change.
        /// </summary>
        public String DateTime
        {
            get { return GetAttribute("datetime"); }
            set { SetAttribute("datetime", value); }
        }

        #endregion
    }
}

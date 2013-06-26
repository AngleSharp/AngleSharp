using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML modifier (ins / del) element.
    /// </summary>
    [DOM("HTMLModElement")]
    public sealed class HTMLModElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The ins tag.
        /// </summary>
        internal const String InsTag = "ins";

        /// <summary>
        /// The del tag.
        /// </summary>
        internal const String DelTag = "del";

        #endregion

        #region ctor

        internal HTMLModElement()
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value that contains a URI of a resource
        /// explaining the change.
        /// </summary>
        [DOM("cite")]
        public String Cite
        {
            get { return GetAttribute("cite"); }
            set { SetAttribute("cite", value); }
        }

        /// <summary>
        /// Gets or sets the value that contains date-and-time string
        /// representing a timestamp for the change.
        /// </summary>
        [DOM("datetime")]
        public String Datetime
        {
            get { return GetAttribute("datetime"); }
            set { SetAttribute("datetime", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return false; }
        }

        #endregion
    }
}

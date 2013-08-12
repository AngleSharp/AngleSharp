using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the base class for all HTML form control elements.
    /// </summary>
    public abstract class HTMLFormControlElement : HTMLElement, ILabelabelElement
    {
        #region Members

        NodeList labels;

        #endregion

        #region ctor

        internal HTMLFormControlElement()
        {
            labels = new NodeList();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if labels are supported.
        /// </summary>
        [DOM("supportsLabels")]
        public Boolean SupportsLabels
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the list of assigned labels.
        /// </summary>
        [DOM("labels")]
        public NodeList Labels
        {
            get { return labels; }
        }

        #endregion
    }
}

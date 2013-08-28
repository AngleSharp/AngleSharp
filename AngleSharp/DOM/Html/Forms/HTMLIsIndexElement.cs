using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML isindex element.
    /// </summary>
    [DOM("HTMLIsIndexElement")]
    public sealed class HTMLIsIndexElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new isindex element.
        /// </summary>
        internal HTMLIsIndexElement()
        {
            _name = Tags.ISINDEX;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the form element containing this control or null if this control is not within the context of a form.
        /// </summary>
        [DOM("form")]
        public HTMLFormElement Form
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the prompt message.
        /// </summary>
        [DOM("prompt")]
        public String Prompt
        {
            get { return GetAttribute("prompt"); }
            set { SetAttribute("prompt", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override Boolean IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}

using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML isindex element.
    /// </summary>
    public class HTMLIsIndexElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The isindex tag.
        /// </summary>
        public const string Tag = "isindex";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new isindex element.
        /// </summary>
        public HTMLIsIndexElement()
        {
            NodeName = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the form element containing this control or null if this control is not within the context of a form.
        /// </summary>
        public HTMLFormElement Form
        {
            get;
            internal set;
        }

        /// <summary>
        /// Gets or sets the prompt message.
        /// </summary>
        public string Prompt
        {
            get { return GetAttribute("prompt"); }
            set { SetAttribute("prompt", value); }
        }

        #endregion

        #region Internal properties

        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}

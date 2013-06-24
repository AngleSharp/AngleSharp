using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML label element.
    /// </summary>
    public sealed class HTMLLabelElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The label tag.
        /// </summary>
        internal const string Tag = "label";

        #endregion

        #region ctor

        internal HTMLLabelElement()
        {
            _name = Tag;
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return false; }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the control that the label is assigned for if any.
        /// </summary>
        public ILabelabelElement Control
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the form element that the label is assigned for if
        /// any.
        /// </summary>
        public HTMLFormElement Form
        {
            get;
            private set;
        }

        #endregion
    }
}

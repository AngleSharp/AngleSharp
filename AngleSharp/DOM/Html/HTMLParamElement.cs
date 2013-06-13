using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents a param element.
    /// </summary>
    public sealed class HTMLParamElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The param tag.
        /// </summary>
        internal const string Tag = "param";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML param element.
        /// </summary>
        internal HTMLParamElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value attribute.
        /// </summary>
        public string Value
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        #endregion

        #region Internal properties

        /// <summary>
        /// Gets if the node is in the special category.
        /// </summary>
        protected internal override bool IsSpecial
        {
            get { return true; }
        }

        #endregion
    }
}

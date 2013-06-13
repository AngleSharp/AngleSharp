using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    public sealed class HTMLOptGroupElement : HTMLElement
    {
        #region Constants

        /// <summary>
        /// The optgroup tag.
        /// </summary>
        internal const string Tag = "optgroup";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML optgroup element.
        /// </summary>
        internal HTMLOptGroupElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
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
    }
}

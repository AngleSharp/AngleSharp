using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the HTML optgroup element.
    /// </summary>
    [DOM("HTMLOptGroupElement")]
    public sealed class HTMLOptGroupElement : HTMLElement, ISelectScopeElement, IImpliedEnd
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML optgroup element.
        /// </summary>
        internal HTMLOptGroupElement()
        {
            _name = Tags.Optgroup;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [DOM("label")]
        public String Label
        {
            get { return GetAttribute("label"); }
            set { SetAttribute("label", value); }
        }

        /// <summary>
        /// Gets or sets if the optgroup is enabled or disabled.
        /// </summary>
        [DOM("disabled")]
        public Boolean Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? String.Empty : null); }
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

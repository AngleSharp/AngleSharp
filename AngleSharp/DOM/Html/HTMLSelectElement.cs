using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents the select element.
    /// </summary>
    public class HTMLSelectElement : HTMLElement
    {
        #region Constant

        /// <summary>
        /// The select tag.
        /// </summary>
        public const string Tag = "select";

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML select element.
        /// </summary>
        public HTMLSelectElement()
        {
            _name = Tag;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public string Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
        }

        /// <summary>
        /// Gets or sets if the select element is enabled or disabled.
        /// </summary>
        public bool Disabled
        {
            get { return GetAttribute("disabled") != null; }
            set { SetAttribute("disabled", value ? string.Empty : null); }
        }

        /// <summary>
        /// Gets or sets if the select element field is required.
        /// </summary>
        public bool Required
        {
            get { return GetAttribute("required") != null; }
            set { SetAttribute("required", value ? string.Empty : null); }
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

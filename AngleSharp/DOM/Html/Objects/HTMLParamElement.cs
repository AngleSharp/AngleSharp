namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents a param element.
    /// </summary>
    [DomName("HTMLParamElement")]
    public sealed class HTMLParamElement : HTMLElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML param element.
        /// </summary>
        internal HTMLParamElement()
        {
            _name = Tags.Param;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value attribute.
        /// </summary>
        [DomName("value")]
        public String Value
        {
            get { return GetAttribute("value"); }
            set { SetAttribute("value", value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        [DomName("name")]
        public String Name
        {
            get { return GetAttribute("name"); }
            set { SetAttribute("name", value); }
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

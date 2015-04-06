namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a param element.
    /// </summary>
    sealed class HtmlParamElement : HtmlElement, IHtmlParamElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML param element.
        /// </summary>
        public HtmlParamElement(Document owner, String prefix = null)
            : base(owner, Tags.Param, prefix, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value attribute.
        /// </summary>
        public String Value
        {
            get { return GetOwnAttribute(AttributeNames.Value); }
            set { SetOwnAttribute(AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        #endregion
    }
}

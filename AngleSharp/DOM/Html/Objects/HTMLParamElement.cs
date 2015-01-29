namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents a param element.
    /// </summary>
    sealed class HTMLParamElement : HtmlElement, IHtmlParamElement
    {
        #region ctor

        /// <summary>
        /// Creates a new HTML param element.
        /// </summary>
        public HTMLParamElement(Document owner)
            : base(owner, Tags.Param, NodeFlags.Special | NodeFlags.SelfClosing)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the value attribute.
        /// </summary>
        public String Value
        {
            get { return GetAttribute(AttributeNames.Value); }
            set { SetAttribute(AttributeNames.Value, value); }
        }

        /// <summary>
        /// Gets or sets the value of the name attribute.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        #endregion
    }
}

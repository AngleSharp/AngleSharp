namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    sealed class HTMLObjectElement : HTMLFormControlElement, IScopeElement, IHtmlObjectElement
    {
        #region Fields

        IDocument _contentDocument;
        IWindowProxy _contentWindow;
        UInt32 _objWidth;
        UInt32 _objHeight;

        #endregion

        #region ctor

        internal HTMLObjectElement()
        {
            _name = Tags.Object;
            _contentDocument = null;
            _contentWindow = null;

            //TODO
            _objHeight = 0;
            _objWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the address of the resource.
        /// </summary>
        public String Source
        {
            get { return GetAttribute(AttributeNames.Data); }
            set { SetAttribute(AttributeNames.Data, value); }
        }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute must be a valid MIME type.
        /// </summary>
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets an attribute whose presence indicates that the resource specified by the data
        /// attribute is only to be used if the value of the type attribute and the Content-Type of the
        /// aforementioned resource match.
        /// </summary>
        public Boolean TypeMustMatch
        {
            get { return GetAttribute(AttributeNames.TypeMustMatch) != null; }
            set { SetAttribute(AttributeNames.TypeMustMatch, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the associated image map of the object if the object element represents an image.
        /// </summary>
        public String UseMap
        {
            get { return GetAttribute(AttributeNames.UseMap); }
            set { SetAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the width of the object element.
        /// </summary>
        public UInt32 DisplayWidth
        {
            get { return ToInteger(GetAttribute(AttributeNames.Width), _objWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height of the object element.
        /// </summary>
        public UInt32 DisplayHeight
        {
            get { return ToInteger(GetAttribute(AttributeNames.Height), _objHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the active document of the object element's nested browsing context, if it has one;
        /// otherwise returns null.
        /// </summary>
        public IDocument ContentDocument
        {
            get { return _contentDocument; }
        }

        /// <summary>
        /// Gets the object element's nested browsing context, if it has one; otherwise returns null.
        /// </summary>
        public IWindowProxy ContentWindow //TODO Object is WindowProxy (or IWindow to be more specific)
        {
            get { return _contentWindow; }
        }

        #endregion

        #region Internal Properties

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

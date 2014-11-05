namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    sealed class HTMLObjectElement : HTMLFormControlElement, IHtmlObjectElement
    {
        #region Fields

        IDocument _contentDocument;
        IWindow _contentWindow;
        Int32 _objWidth;
        Int32 _objHeight;

        #endregion

        #region ctor

        internal HTMLObjectElement()
            : base(Tags.Object, NodeFlags.Scoped)
        {
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
        public Int32 DisplayWidth
        {
            get { return GetAttribute(AttributeNames.Width).ToInteger(_objWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height of the object element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(_objHeight); }
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
        public IWindow ContentWindow
        {
            get { return _contentWindow; }
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    [DomName("HTMLObjectElement")]
    public sealed class HTMLObjectElement : HTMLFormControlElement, IScopeElement
    {
        #region Fields

        Document _contentDocument;
        Object _contentWindow;
        UInt32 _objWidth;
        UInt32 _objHeight;

        #endregion

        #region ctor

        internal HTMLObjectElement()
        {
            _name = Tags.Object;

            //TODO
            _objHeight = 0;
            _objWidth = 0;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the address of the resource.
        /// </summary>
        [DomName("data")]
        public String Data
        {
            get { return GetAttribute("data"); }
            set { SetAttribute("data", value); }
        }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute must be a valid MIME type.
        /// </summary>
        [DomName("type")]
        public String Type
        {
            get { return GetAttribute("type"); }
            set { SetAttribute("type", value); }
        }

        /// <summary>
        /// Gets or sets an attribute whose presence indicates that the resource specified by the data
        /// attribute is only to be used if the value of the type attribute and the Content-Type of the
        /// aforementioned resource match.
        /// </summary>
        [DomName("typeMustMatch")]
        public Boolean TypeMustMatch
        {
            get { return GetAttribute("typemustmatch") != null; }
            set { SetAttribute("typemustmatch", value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the associated image map of the object if the object element represents an image.
        /// </summary>
        [DomName("useMap")]
        public String UseMap
        {
            get { return GetAttribute("usemap"); }
            set { SetAttribute("usemap", value); }
        }

        /// <summary>
        /// Gets or sets the width of the object element.
        /// </summary>
        [DomName("width")]
        public UInt32 Width
        {
            get { return ToInteger(GetAttribute("width"), _objWidth); }
            set { SetAttribute("width", value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height of the object element.
        /// </summary>
        [DomName("height")]
        public UInt32 Height
        {
            get { return ToInteger(GetAttribute("height"), _objHeight); }
            set { SetAttribute("height", value.ToString()); }
        }

        /// <summary>
        /// Gets the active document of the object element's nested browsing context, if it has one;
        /// otherwise returns null.
        /// </summary>
        [DomName("contentDocument")]
        public Document ContentDocument
        {
            get { return _contentDocument; }
        }

        /// <summary>
        /// Gets the object element's nested browsing context, if it has one; otherwise returns null.
        /// </summary>
        [DomName("contentWindow")]
        public Object ContentWindow //TODO Object is WindowProxy (or IWindow to be more specific)
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

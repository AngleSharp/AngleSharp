namespace AngleSharp.Dom.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;
    using System;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    sealed class HtmlObjectElement : HtmlFormControlElement, IHtmlObjectElement
    {
        #region Fields

        readonly BoundLocation _data;
        IObjectInfo _obj;

        #endregion

        #region ctor

        public HtmlObjectElement(Document owner, String prefix = null)
            : base(owner, Tags.Object, prefix, NodeFlags.Scoped)
        {
            _data = new BoundLocation(this, AttributeNames.Data);
            RegisterAttributeObserver(AttributeNames.Data, UpdateSource);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the address of the resource.
        /// </summary>
        public String Source
        {
            get { return _data.Href; }
            set { _data.Href = value; }
        }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute
        /// must be a valid MIME type.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
        }

        /// <summary>
        /// Gets or sets an attribute whose presence indicates that the
        /// resource specified by the data attribute is only to be used if the
        /// value of the type attribute and the Content-Type of the
        /// aforementioned resource match.
        /// </summary>
        public Boolean TypeMustMatch
        {
            get { return GetOwnAttribute(AttributeNames.TypeMustMatch) != null; }
            set { SetOwnAttribute(AttributeNames.TypeMustMatch, value ? String.Empty : null); }
        }

        /// <summary>
        /// Gets or sets the associated image map of the object if the object
        /// element represents an image.
        /// </summary>
        public String UseMap
        {
            get { return GetOwnAttribute(AttributeNames.UseMap); }
            set { SetOwnAttribute(AttributeNames.UseMap, value); }
        }

        /// <summary>
        /// Gets or sets the width of the object element.
        /// </summary>
        public Int32 DisplayWidth
        {
            get { return GetOwnAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetOwnAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height of the object element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetOwnAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetOwnAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the original width of the object.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _obj != null ? _obj.Width : 0; }
        }

        /// <summary>
        /// Gets the original height of the object.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _obj != null ? _obj.Height : 0; }
        }

        /// <summary>
        /// Gets the active document of the object element's nested browsing
        /// context, if it has one; otherwise returns null.
        /// </summary>
        public IDocument ContentDocument
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the object element's nested browsing context, if it has one;
        /// otherwise returns null.
        /// </summary>
        public IWindow ContentWindow
        {
            get { return null; }
        }

        #endregion

        #region Methods

        protected override Boolean CanBeValidated()
        {
            return false;
        }

        void UpdateSource(String value)
        {
            this.CancelTasks();

            if (!String.IsNullOrEmpty(value))
            {
                var url = new Url(Source);
                var request = this.CreateRequestFor(url);
                this.LoadResource<IObjectInfo>(request).ContinueWith(m =>
                {
                    _obj = m.Result;
                    this.FireLoadOrErrorEvent(m);
                });
            }
        }

        #endregion
    }
}

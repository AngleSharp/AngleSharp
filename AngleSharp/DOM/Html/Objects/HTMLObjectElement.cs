namespace AngleSharp.DOM.Html
{
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Services.Media;
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Represents the HTML object element.
    /// </summary>
    sealed class HTMLObjectElement : HTMLFormControlElement, IHtmlObjectElement
    {
        #region Fields

        IDocument _contentDocument;
        IWindow _contentWindow;
        Task<IObjectInfo> _resourceTask;

        #endregion

        #region ctor

        public HTMLObjectElement(Document owner)
            : base(owner, Tags.Object, NodeFlags.Scoped)
        {
            _contentDocument = null;
            _contentWindow = null;
            RegisterAttributeObserver(AttributeNames.Src, UpdateSource);
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
            get { return GetAttribute(AttributeNames.Width).ToInteger(OriginalWidth); }
            set { SetAttribute(AttributeNames.Width, value.ToString()); }
        }

        /// <summary>
        /// Gets or sets the height of the object element.
        /// </summary>
        public Int32 DisplayHeight
        {
            get { return GetAttribute(AttributeNames.Height).ToInteger(OriginalHeight); }
            set { SetAttribute(AttributeNames.Height, value.ToString()); }
        }

        /// <summary>
        /// Gets the original width of the object.
        /// </summary>
        public Int32 OriginalWidth
        {
            get { return _resourceTask != null ? (_resourceTask.IsCompleted && _resourceTask.Result != null ? _resourceTask.Result.Width : 0) : 0; }
        }

        /// <summary>
        /// Gets the original height of the object.
        /// </summary>
        public Int32 OriginalHeight
        {
            get { return _resourceTask != null ? (_resourceTask.IsCompleted && _resourceTask.Result != null ? _resourceTask.Result.Height : 0) : 0; }
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

        #region Methods

        protected override Boolean CanBeValidated()
        {
            return false;
        }

        void UpdateSource(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var url = this.HyperReference(value);
                _resourceTask = Owner.Options.LoadResource<IObjectInfo>(url);
                _resourceTask.ContinueWith(_ => this.FireSimpleEvent(EventNames.Load));
            }
        }

        #endregion
    }
}

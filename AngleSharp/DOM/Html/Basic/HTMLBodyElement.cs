namespace AngleSharp.DOM.Html
{
    using System;

    /// <summary>
    /// Represents the HTML body element.
    /// </summary>
    sealed class HTMLBodyElement : HTMLElement, IHtmlBodyElement
    {
        #region ctor

        /// <summary>
        /// Creates a HTML body element.
        /// </summary>
        public HTMLBodyElement()
            : base(Tags.Body, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of active links (after mouse-button down, but before mouse-button up). 
        /// </summary>
        public String ALink
        {
            get { return GetAttribute(AttributeNames.Alink); }
            set { SetAttribute(AttributeNames.Alink, value); }
        }

        /// <summary>
        /// Gets or sets the URI of the background texture tile image.
        /// </summary>
        public String Background
        {
            get { return GetAttribute(AttributeNames.Background); }
            set { SetAttribute(AttributeNames.Background, value); }
        }

        /// <summary>
        /// Gets or sets the document background color.
        /// </summary>
        public String BgColor
        {
            get { return GetAttribute(AttributeNames.BgColor); }
            set { SetAttribute(AttributeNames.BgColor, value); }
        }

        /// <summary>
        /// Gets or sets color of links that are not active and unvisited.
        /// </summary>
        public String Link
        {
            get { return GetAttribute(AttributeNames.Link); }
            set { SetAttribute(AttributeNames.Link, value); }
        }

        /// <summary>
        /// Gets or sets document text color.
        /// </summary>
        public String Text
        {
            get { return GetAttribute(AttributeNames.Text); }
            set { SetAttribute(AttributeNames.Text, value); }
        }

        /// <summary>
        /// Gets or sets color of links that have been visited by the user.
        /// </summary>
        public String VLink
        {
            get { return GetAttribute(AttributeNames.Vlink); }
            set { SetAttribute(AttributeNames.Vlink, value); }
        }

        #endregion

        #region Events from IDL

        public event EventListener Printed
        {
            add { AddEventListener(EventNames.AfterPrint, value); }
            remove { RemoveEventListener(EventNames.AfterPrint, value); }
        }

        public event EventListener Printing
        {
            add { AddEventListener(EventNames.BeforePrint, value); }
            remove { RemoveEventListener(EventNames.BeforePrint, value); }
        }

        public event EventListener Unloading
        {
            add { AddEventListener(EventNames.Unloading, value); }
            remove { RemoveEventListener(EventNames.Unloading, value); }
        }

        public event EventListener HashChanged
        {
            add { AddEventListener(EventNames.HashChange, value); }
            remove { RemoveEventListener(EventNames.HashChange, value); }
        }

        public event EventListener MessageReceived
        {
            add { AddEventListener(EventNames.Message, value); }
            remove { RemoveEventListener(EventNames.Message, value); }
        }

        public event EventListener WentOffline
        {
            add { AddEventListener(EventNames.Offline, value); }
            remove { RemoveEventListener(EventNames.Offline, value); }
        }

        public event EventListener WentOnline
        {
            add { AddEventListener(EventNames.Online, value); }
            remove { RemoveEventListener(EventNames.Online, value); }
        }

        public event EventListener PageHidden
        {
            add { AddEventListener(EventNames.PageHide, value); }
            remove { RemoveEventListener(EventNames.PageHide, value); }
        }

        public event EventListener PageShown
        {
            add { AddEventListener(EventNames.PageShow, value); }
            remove { RemoveEventListener(EventNames.PageShow, value); }
        }

        public event EventListener PopState
        {
            add { AddEventListener(EventNames.PopState, value); }
            remove { RemoveEventListener(EventNames.PopState, value); }
        }

        public event EventListener Storage
        {
            add { AddEventListener(EventNames.Storage, value); }
            remove { RemoveEventListener(EventNames.Storage, value); }
        }

        public event EventListener Unloaded
        {
            add { AddEventListener(EventNames.Unload, value); }
            remove { RemoveEventListener(EventNames.Unload, value); }
        }

        #endregion
    }
}

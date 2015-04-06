namespace AngleSharp.Dom.Html
{
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the HTML body element.
    /// </summary>
    sealed class HtmlBodyElement : HtmlElement, IHtmlBodyElement
    {
        #region ctor

        /// <summary>
        /// Creates a HTML body element.
        /// </summary>
        public HtmlBodyElement(Document owner, String prefix = null)
            : base(owner, Tags.Body, prefix, NodeFlags.Special | NodeFlags.ImplicitelyClosed)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the color of active links (after mouse-button down, but before mouse-button up). 
        /// </summary>
        public String ALink
        {
            get { return GetOwnAttribute(AttributeNames.Alink); }
            set { SetOwnAttribute(AttributeNames.Alink, value); }
        }

        /// <summary>
        /// Gets or sets the URI of the background texture tile image.
        /// </summary>
        public String Background
        {
            get { return GetOwnAttribute(AttributeNames.Background); }
            set { SetOwnAttribute(AttributeNames.Background, value); }
        }

        /// <summary>
        /// Gets or sets the document background color.
        /// </summary>
        public String BgColor
        {
            get { return GetOwnAttribute(AttributeNames.BgColor); }
            set { SetOwnAttribute(AttributeNames.BgColor, value); }
        }

        /// <summary>
        /// Gets or sets color of links that are not active and unvisited.
        /// </summary>
        public String Link
        {
            get { return GetOwnAttribute(AttributeNames.Link); }
            set { SetOwnAttribute(AttributeNames.Link, value); }
        }

        /// <summary>
        /// Gets or sets document text color.
        /// </summary>
        public String Text
        {
            get { return GetOwnAttribute(AttributeNames.Text); }
            set { SetOwnAttribute(AttributeNames.Text, value); }
        }

        /// <summary>
        /// Gets or sets color of links that have been visited by the user.
        /// </summary>
        public String VLink
        {
            get { return GetOwnAttribute(AttributeNames.Vlink); }
            set { SetOwnAttribute(AttributeNames.Vlink, value); }
        }

        #endregion

        #region Events from IDL

        public event DomEventHandler Printed
        {
            add { AddEventListener(EventNames.AfterPrint, value); }
            remove { RemoveEventListener(EventNames.AfterPrint, value); }
        }

        public event DomEventHandler Printing
        {
            add { AddEventListener(EventNames.BeforePrint, value); }
            remove { RemoveEventListener(EventNames.BeforePrint, value); }
        }

        public event DomEventHandler Unloading
        {
            add { AddEventListener(EventNames.Unloading, value); }
            remove { RemoveEventListener(EventNames.Unloading, value); }
        }

        public event DomEventHandler HashChanged
        {
            add { AddEventListener(EventNames.HashChange, value); }
            remove { RemoveEventListener(EventNames.HashChange, value); }
        }

        public event DomEventHandler MessageReceived
        {
            add { AddEventListener(EventNames.Message, value); }
            remove { RemoveEventListener(EventNames.Message, value); }
        }

        public event DomEventHandler WentOffline
        {
            add { AddEventListener(EventNames.Offline, value); }
            remove { RemoveEventListener(EventNames.Offline, value); }
        }

        public event DomEventHandler WentOnline
        {
            add { AddEventListener(EventNames.Online, value); }
            remove { RemoveEventListener(EventNames.Online, value); }
        }

        public event DomEventHandler PageHidden
        {
            add { AddEventListener(EventNames.PageHide, value); }
            remove { RemoveEventListener(EventNames.PageHide, value); }
        }

        public event DomEventHandler PageShown
        {
            add { AddEventListener(EventNames.PageShow, value); }
            remove { RemoveEventListener(EventNames.PageShow, value); }
        }

        public event DomEventHandler PopState
        {
            add { AddEventListener(EventNames.PopState, value); }
            remove { RemoveEventListener(EventNames.PopState, value); }
        }

        public event DomEventHandler Storage
        {
            add { AddEventListener(EventNames.Storage, value); }
            remove { RemoveEventListener(EventNames.Storage, value); }
        }

        public event DomEventHandler Unloaded
        {
            add { AddEventListener(EventNames.Unload, value); }
            remove { RemoveEventListener(EventNames.Unload, value); }
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// Represents the area element.
    /// </summary>
    [DOM("HTMLAreaElement")]
    public sealed class HTMLAreaElement : HTMLElement
    {
        #region Fields

        DOMTokenList rellist;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new area element.
        /// </summary>
        internal HTMLAreaElement()
        {
            _name = Tags.Area;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return GetAttribute(AttributeNames.Href); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the language of the linked resource.
        /// </summary>
        [DOM("hreflang")]
        public String HrefLang
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the target media of the linked resource.
        /// </summary>
        [DOM("media")]
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the value indicating relationships of the
        /// current document to the linked resource.
        /// </summary>
        [DOM("rel")]
        public String Rel
        {
            get { return GetAttribute(AttributeNames.Rel); }
            set { SetAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets or sets the value indicating relationships of the current
        /// document to the linked resource, as a list of tokens.
        /// </summary>
        [DOM("relList")]
        public DOMTokenList RelList
        {
            get { return rellist ?? (rellist = new DOMTokenList(this, AttributeNames.Rel)); }
        }

        /// <summary>
        /// Gets or sets the alternative text for the element.
        /// </summary>
        [DOM("alt")]
        public String Alt
        {
            get { return GetAttribute(AttributeNames.Alt); }
            set { SetAttribute(AttributeNames.Alt, value); }
        }

        /// <summary>
        /// Gets or sets a single character that switches input focus to the control.
        /// </summary>
        [DOM("accessKey")]
        public String AccessKey
        {
            get { return GetAttribute(AttributeNames.AccessKey); }
            set { SetAttribute(AttributeNames.AccessKey, value); }
        }

        /// <summary>
        /// Gets or sets the coordinates to define the hot-spot region.
        /// </summary>
        [DOM("coords")]
        public String Coords
        {
            get { return GetAttribute(AttributeNames.Coords); }
            set { SetAttribute(AttributeNames.Coords, value); }
        }

        /// <summary>
        /// Gets or sets the shape of the hot-spot, limited to known values.
        /// </summary>
        [DOM("shape")]
        public String Shape
        {
            get { return GetAttribute(AttributeNames.Shape); }
            set { SetAttribute(AttributeNames.Shape, value); }
        }

        /// <summary>
        /// Gets or sets the browsing context in which to open the linked resource.
        /// </summary>
        [DOM("target")]
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the MIME type of the linked resource.
        /// </summary>
        [DOM("type")]
        public String Type
        {
            get { return GetAttribute(AttributeNames.Type); }
            set { SetAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Design properties

        /// <summary>
        /// Gets or sets if the link has been visited.
        /// </summary>
        internal Boolean IsVisited
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets if the link is currently active.
        /// </summary>
        internal Boolean IsActive
        {
            get;
            set;
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

        #region Helpers

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Rel, StringComparison.Ordinal))
                RelList.Update(Rel);
            else
                base.OnAttributeChanged(name);
        }

        #endregion
    }
}

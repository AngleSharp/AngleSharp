namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the base for a / area elements.
    /// </summary>
    abstract class HtmlUrlBaseElement : HtmlElement, IUrlUtilities
    {
        #region Fields

        TokenList _relList;
        SettableTokenList _ping;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new element.
        /// </summary>
        public HtmlUrlBaseElement(Document owner, String name, String prefix, NodeFlags flags)
            : base(owner, name, prefix, flags)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the linked resource is intended to be downloaded rather
        /// than displayed. The value represent the proposed name of the file.
        /// If the name is not a valid filename of the underlying OS, the
        /// navigator will adapt it.
        /// </summary>
        public String Download
        {
            get { return GetOwnAttribute(AttributeNames.Download); }
            set { SetOwnAttribute(AttributeNames.Download, value); }
        }

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Href
        {
            get { return GetUrlAttribute(AttributeNames.Href); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the fragment identifier, including the leading hash
        /// mark ('#'), if any, in the referenced URL.
        /// </summary>
        public String Hash
        {
            get { return GetLocationPart(m => m.Hash); }
            set { SetLocationPart(m => m.Hash = value); }
        }

        /// <summary>
        /// Gets or sets the hostname and port (if it's not the default port) in
        /// the referenced URL.
        /// </summary>
        public String Host
        {
            get { return GetLocationPart(m => m.Host); }
            set { SetLocationPart(m => m.Host = value); }
        }

        /// <summary>
        /// Gets or sets the hostname in the referenced URL.
        /// </summary>
        public String HostName
        {
            get { return GetLocationPart(m => m.HostName); }
            set { SetLocationPart(m => m.HostName = value); }
        }

        /// <summary>
        /// Gets or sets the path name component, if any, of the referenced URL.
        /// </summary>
        public String PathName
        {
            get { return GetLocationPart(m => m.PathName); }
            set { SetLocationPart(m => m.PathName = value); }
        }

        /// <summary>
        /// Gets or sets the port component, if any, of the referenced URL.
        /// </summary>
        public String Port
        {
            get { return GetLocationPart(m => m.Port); }
            set { SetLocationPart(m => m.Port = value); }
        }

        /// <summary>
        /// Gets or sets the protocol component, including trailing colon (':'),
        /// of the referenced URL.
        /// </summary>
        public String Protocol
        {
            get { return GetLocationPart(m => m.Protocol); }
            set { SetLocationPart(m => m.Protocol = value); }
        }

        /// <summary>
        /// Gets or sets the URL's username.
        /// </summary>
        public String UserName
        {
            get { return GetLocationPart(m => m.UserName); }
            set { SetLocationPart(m => m.UserName = value); }
        }

        /// <summary>
        /// Gets or sets the URL's password.
        /// </summary>
        public String Password
        {
            get { return GetLocationPart(m => m.Password); }
            set { SetLocationPart(m => m.Password = value); }
        }

        /// <summary>
        /// Gets or sets the search element, including leading question mark
        /// ('?'), if any, of the referenced URL.
        /// </summary>
        public String Search
        {
            get { return GetLocationPart(m => m.Search); }
            set { SetLocationPart(m => m.Search = value); }
        }

        /// <summary>
        /// Get's the URL's origin.
        /// </summary>
        public String Origin
        {
            get { return GetLocationPart(m => m.Origin); }
        }

        /// <summary>
        /// Gets or sets the language of the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return GetOwnAttribute(AttributeNames.HrefLang); }
            set { SetOwnAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the target media of the linked resource.
        /// </summary>
        public String Media
        {
            get { return GetOwnAttribute(AttributeNames.Media); }
            set { SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the value indicating relationships of the current
        /// document to the linked resource.
        /// </summary>
        public String Relation
        {
            get { return GetOwnAttribute(AttributeNames.Rel); }
            set { SetOwnAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets the value indicating relationships of the current document to
        /// the linked resource, as a list of tokens.
        /// </summary>
        public ITokenList RelationList
        {
            get
            {
                if (_relList == null)
                {
                    _relList = new TokenList(GetOwnAttribute(AttributeNames.Rel));
                    CreateBindings(_relList, AttributeNames.Rel);
                }

                return _relList;
            }
        }

        /// <summary>
        /// Gets the ping HTML attribute, as a settable list of tokens.
        /// </summary>
        public ISettableTokenList Ping
        {
            get
            {
                if (_ping == null)
                {
                    _ping = new SettableTokenList(GetOwnAttribute(AttributeNames.Ping));
                    CreateBindings(_ping, AttributeNames.Ping);
                }

                return _ping;
            }
        }

        /// <summary>
        /// Gets or sets the browsing context in which to open the linked
        /// resource.
        /// </summary>
        public String Target
        {
            get { return GetOwnAttribute(AttributeNames.Target); }
            set { SetOwnAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the MIME type of the linked resource.
        /// </summary>
        public String Type
        {
            get { return GetOwnAttribute(AttributeNames.Type); }
            set { SetOwnAttribute(AttributeNames.Type, value); }
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

        #region Helpers

        String GetLocationPart(Func<ILocation, String> getter)
        {
            var href = GetOwnAttribute(AttributeNames.Href);
            var url = href != null ? new Url(BaseUrl, href) : null;

            if (url == null || url.IsInvalid)
                return String.Empty;

            var location = new Location(url);
            return getter(location);
        }

        void SetLocationPart(Action<ILocation> setter)
        {
            var href = GetOwnAttribute(AttributeNames.Href);
            var url = href != null ? new Url(BaseUrl, href) : null;

            if (url == null || url.IsInvalid)
                url = new Url(BaseUrl);

            var location = new Location(url);
            setter(location);
            SetOwnAttribute(AttributeNames.Href, location.Href);
        }

        #endregion
    }
}

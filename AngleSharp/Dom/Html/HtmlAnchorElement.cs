namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    sealed class HtmlAnchorElement : HtmlElement, IHtmlAnchorElement
    {
        #region Fields

        readonly BoundLocation _location;
        TokenList _relList;
        SettableTokenList _ping;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        public HtmlAnchorElement(Document owner, String prefix = null)
            : base(owner, Tags.A, prefix, NodeFlags.HtmlFormatting)
        {
            _location = new BoundLocation(this, AttributeNames.Href);
            RegisterAttributeObserver(AttributeNames.Rel, UpdateRelList);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public String Charset
        {
            get { return GetOwnAttribute(AttributeNames.Charset); }
            set { SetOwnAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the linked resource is intended to be downloaded rather than displayed.
        /// The value represent the proposed name of the file. If the name is not a valid filename of the
        /// underlying OS, the navigator will adapt it.
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
            get { return _location.Href; }
            set { _location.Href = value; }
        }

        /// <summary>
        /// Gets or sets the fragment identifier, including the leading hash
        /// mark ('#'), if any, in the referenced URL.
        /// </summary>
        public String Hash
        {
            get { return _location.Hash; }
            set { _location.Hash = value; }
        }

        /// <summary>
        /// Gets or sets the hostname and port (if it's not the default port)
        /// in the referenced URL.
        /// </summary>
        public String Host
        {
            get { return _location.Host; }
            set { _location.Host = value; }
        }

        /// <summary>
        /// Gets or sets the hostname in the referenced URL.
        /// </summary>
        public String HostName
        {
            get { return _location.HostName; }
            set { _location.HostName = value; }
        }

        /// <summary>
        /// Gets or sets the path name component, if any, of the
        /// referenced URL.
        /// </summary>
        public String PathName
        {
            get { return _location.PathName; }
            set { _location.PathName = value; }
        }

        /// <summary>
        /// Gets or sets the port component, if any, of the referenced URL.
        /// </summary>
        public String Port
        {
            get { return _location.Port; }
            set { _location.Port = value; }
        }

        /// <summary>
        /// Gets or sets the protocol component, including trailing
        /// colon (':'), of the referenced URL.
        /// </summary>
        public String Protocol
        {
            get { return _location.Protocol; }
            set { _location.Protocol = value; }
        }

        /// <summary>
        /// Gets or sets the URL's username.
        /// </summary>
        public String UserName
        {
            get { return _location.UserName; }
            set { _location.UserName = value; }
        }

        /// <summary>
        /// Gets or sets the URL's password.
        /// </summary>
        public String Password
        {
            get { return _location.Password; }
            set { _location.Password = value; }
        }

        /// <summary>
        /// Gets or sets the search element, including leading question
        /// mark ('?'), if any, of the referenced URL.
        /// </summary>
        public String Search
        {
            get { return _location.Search; }
            set { _location.Search = value; }
        }

        /// <summary>
        /// Get's the URL's origin.
        /// </summary>
        public String Origin
        {
            get { return _location.Origin; }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return GetOwnAttribute(AttributeNames.HrefLang); }
            set { SetOwnAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the media HTML attribute, indicating the intended
        /// media for the linked resource.
        /// </summary>
        public String Media
        {
            get { return GetOwnAttribute(AttributeNames.Media); }
            set { SetOwnAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the anchor name.
        /// </summary>
        public String Name
        {
            get { return GetOwnAttribute(AttributeNames.Name); }
            set { SetOwnAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the rel HTML attribute, specifying the relationship
        /// of the target object to the link object.
        /// </summary>
        public String Relation
        {
            get { return GetOwnAttribute(AttributeNames.Rel); }
            set { SetOwnAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets the rel HTML attribute, as a list of tokens.
        /// </summary>
        public ITokenList RelationList
        {
            get 
            { 
                if (_relList == null)
                {
                    _relList = new TokenList(GetOwnAttribute(AttributeNames.Rel));
                    _relList.Changed += (s, ev) => UpdateAttribute(AttributeNames.Rel, _relList.ToString());
                }

                return _relList; 
            }
        }

        /// <summary>
        /// Gets the ping HTML attribute, as a settable list of otkens.
        /// </summary>
        public ISettableTokenList Ping
        {
            get
            { 
                if (_ping == null)
                {
                    _ping = new SettableTokenList(GetOwnAttribute(AttributeNames.Ping));
                    _ping.Changed += (s, ev) => UpdateAttribute(AttributeNames.Ping, _ping.Value);
                }

                return _ping; 
            }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        public String Target
        {
            get { return GetOwnAttribute(AttributeNames.Target); }
            set { SetOwnAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the text of the anchor tag (same as TextContent).
        /// </summary>
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute must be a valid MIME type.
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

        public override void DoFocus()
        {
            if (GetOwnAttribute(AttributeNames.Href) != null)
                IsFocused = true;
        }

        void UpdateRelList(String value)
        {
            if (_relList != null)
                _relList.Update(value);
        }

        #endregion
    }
}

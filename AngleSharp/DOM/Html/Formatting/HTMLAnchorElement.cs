namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    sealed class HTMLAnchorElement : HTMLElement, IHtmlAnchorElement
    {
        #region Fields

        TokenList _relList;
        SettableTokenList _ping;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        public HTMLAnchorElement()
            : base(Tags.A, NodeFlags.HtmlFormatting)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        public String Charset
        {
            get { return GetAttribute(AttributeNames.Charset); }
            set { SetAttribute(AttributeNames.Charset, value); }
        }

        /// <summary>
        /// Gets or sets the linked resource is intended to be downloaded rather than displayed.
        /// The value represent the proposed name of the file. If the name is not a valid filename of the
        /// underlying OS, the navigator will adapt it.
        /// </summary>
        public String Download
        {
            get { return GetAttribute(AttributeNames.Download); }
            set { SetAttribute(AttributeNames.Download, value); }
        }

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        public String Href
        {
            get { return this.HyperRef(GetAttribute(AttributeNames.Href)).Href; }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the fragment identifier, including the leading hash
        /// mark ('#'), if any, in the referenced URL.
        /// </summary>
        public String Hash
        {
            get { return new Location(Href).Hash; }
            set
            {
                var loc = new Location(Href);
                loc.Hash = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the hostname and port (if it's not the default port)
        /// in the referenced URL.
        /// </summary>
        public String Host
        {
            get { return new Location(Href).Host; }
            set
            {
                var loc = new Location(Href);
                loc.Host = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the hostname in the referenced URL.
        /// </summary>
        public String HostName
        {
            get { return new Location(Href).HostName; }
            set
            {
                var loc = new Location(Href);
                loc.HostName = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the path name component, if any, of the
        /// referenced URL.
        /// </summary>
        public String PathName
        {
            get { return new Location(Href).PathName; }
            set
            {
                var loc = new Location(Href);
                loc.PathName = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the port component, if any, of the referenced URL.
        /// </summary>
        public String Port
        {
            get { return new Location(Href).Port; }
            set
            {
                var loc = new Location(Href);
                loc.Port = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the protocol component, including trailing
        /// colon (':'), of the referenced URL.
        /// </summary>
        public String Protocol
        {
            get { return new Location(Href).Protocol; }
            set
            {
                var loc = new Location(Href);
                loc.Protocol = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the URL's username.
        /// </summary>
        public String UserName
        {
            get { return new Location(Href).UserName; }
            set
            {
                var loc = new Location(Href);
                loc.UserName = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the URL's password.
        /// </summary>
        public String Password
        {
            get { return new Location(Href).Password; }
            set
            {
                var loc = new Location(Href);
                loc.Password = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Gets or sets the search element, including leading question
        /// mark ('?'), if any, of the referenced URL.
        /// </summary>
        public String Search
        {
            get { return new Location(Href).Search; }
            set
            {
                var loc = new Location(Href);
                loc.Search = value;
                Href = loc.Href;
            }
        }

        /// <summary>
        /// Get's the URL's origin.
        /// </summary>
        public String Origin
        {
            get { return new Location(Href).Origin; }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        public String TargetLanguage
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the media HTML attribute, indicating the intended
        /// media for the linked resource.
        /// </summary>
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the anchor name.
        /// </summary>
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the rel HTML attribute, specifying the relationship
        /// of the target object to the link object.
        /// </summary>
        public String Relation
        {
            get { return GetAttribute(AttributeNames.Rel); }
            set { SetAttribute(AttributeNames.Rel, value); }
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
                    _relList = new TokenList(GetAttribute(AttributeNames.Rel));
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
                    _ping = new SettableTokenList(GetAttribute(AttributeNames.Ping));
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
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
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

        #region Helpers

        public override void DoFocus()
        {
            if (GetAttribute(AttributeNames.Href) != null)
                IsFocused = true;
        }

        internal override void Close()
        {
            base.Close();
            RegisterAttributeHandler(AttributeNames.Rel, value =>
            {
                if (_relList != null)
                    _relList.Update(value);
            });
        }

        #endregion
    }
}

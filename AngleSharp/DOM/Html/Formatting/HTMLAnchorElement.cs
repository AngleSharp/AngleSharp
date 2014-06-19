using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM.Html
{
    /// <summary>
    /// Represents an anchor element.
    /// </summary>
    [DomName("HTMLAnchorElement")]
    public sealed class HTMLAnchorElement : HTMLElement, IFormatting
    {
        #region Members

        TokenList _relList;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new anchor element.
        /// </summary>
        internal HTMLAnchorElement()
        {
            _name = Tags.A;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the character encoding for the target resource.
        /// </summary>
        [DomName("charset")]
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
        [DomName("download")]
        public String Download
        {
            get { return GetAttribute(AttributeNames.Download); }
            set { SetAttribute(AttributeNames.Download, value); }
        }

        /// <summary>
        /// Gets or sets the value of the href attribute.
        /// </summary>
        [DomName("href")]
        public String Href
        {
            get { return HyperRef(GetAttribute(AttributeNames.Href)); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        /// <summary>
        /// Gets or sets the language code for the linked resource.
        /// </summary>
        [DomName("hreflang")]
        public String Hreflang
        {
            get { return GetAttribute(AttributeNames.HrefLang); }
            set { SetAttribute(AttributeNames.HrefLang, value); }
        }

        /// <summary>
        /// Gets or sets the media HTML attribute, indicating the intended
        /// media for the linked resource.
        /// </summary>
        [DomName("media")]
        public String Media
        {
            get { return GetAttribute(AttributeNames.Media); }
            set { SetAttribute(AttributeNames.Media, value); }
        }

        /// <summary>
        /// Gets or sets the anchor name.
        /// </summary>
        [DomName("name")]
        public String Name
        {
            get { return GetAttribute(AttributeNames.Name); }
            set { SetAttribute(AttributeNames.Name, value); }
        }

        /// <summary>
        /// Gets or sets the rel HTML attribute, specifying the relationship
        /// of the target object to the link object.
        /// </summary>
        [DomName("rel")]
        public String Rel
        {
            get { return GetAttribute(AttributeNames.Rel); }
            set { SetAttribute(AttributeNames.Rel, value); }
        }

        /// <summary>
        /// Gets or sets the fragment identifier, including the leading hash
        /// mark ('#'), if any, in the referenced URL.
        /// </summary>
        [DomName("hash")]
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
        [DomName("host")]
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
        [DomName("hostname")]
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
        [DomName("pathname")]
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
        [DomName("port")]
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
        [DomName("protocol")]
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
        /// Gets or sets the search element, including leading question
        /// mark ('?'), if any, of the referenced URL.
        /// </summary>
        [DomName("search")]
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
        /// Gets the rel HTML attribute, as a list of tokens.
        /// </summary>
        [DomName("relList")]
        public ITokenList RelList
        {
            get { return _relList ?? (_relList = new TokenList(this, AttributeNames.Rel)); }
        }

        /// <summary>
        /// Gets or sets the name of the target frame to which the resource applies.
        /// </summary>
        [DomName("target")]
        public String Target
        {
            get { return GetAttribute(AttributeNames.Target); }
            set { SetAttribute(AttributeNames.Target, value); }
        }

        /// <summary>
        /// Gets or sets the text of the anchor tag (same as TextContent).
        /// </summary>
        [DomName("text")]
        public String Text
        {
            get { return TextContent; }
            set { TextContent = value; }
        }

        /// <summary>
        /// Gets or sets the type of the resource. If present, the attribute must be a valid MIME type.
        /// </summary>
        [DomName("type")]
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

        /// <summary>
        /// Entry point for attributes to notify about a change (modified, added, removed).
        /// </summary>
        /// <param name="name">The name of the attribute that has been changed.</param>
        internal override void OnAttributeChanged(String name)
        {
            if (name.Equals(AttributeNames.Rel, StringComparison.Ordinal))
            {
                if (_relList != null)
                    _relList.Update(Rel);
            }
            
            base.OnAttributeChanged(name);
        }

        #endregion
    }
}

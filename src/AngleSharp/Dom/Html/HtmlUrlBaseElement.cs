namespace AngleSharp.Dom.Html
{
    using AngleSharp.Dom.Collections;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using System;

    /// <summary>
    /// Represents the base for a / area elements.
    /// </summary>
    abstract class HtmlUrlBaseElement : HtmlElement, IUrlUtilities
    {
        #region Fields

        private TokenList _relList;
        private SettableTokenList _ping;

        #endregion

        #region ctor

        static HtmlUrlBaseElement()
        {
            RegisterCallback<HtmlUrlBaseElement>(AttributeNames.Rel, (element, value) => element._relList?.Update(value));
            RegisterCallback<HtmlUrlBaseElement>(AttributeNames.Ping, (element, value) => element._ping?.Update(value));
        }

        public HtmlUrlBaseElement(Document owner, String name, String prefix, NodeFlags flags)
            : base(owner, name, prefix, flags)
        {
        }

        #endregion

        #region Properties

        public String Download
        {
            get { return this.GetOwnAttribute(AttributeNames.Download); }
            set { this.SetOwnAttribute(AttributeNames.Download, value); }
        }

        public String Href
        {
            get { return this.GetUrlAttribute(AttributeNames.Href); }
            set { SetAttribute(AttributeNames.Href, value); }
        }

        public String Hash
        {
            get { return GetLocationPart(m => m.Hash); }
            set { SetLocationPart(m => m.Hash = value); }
        }

        public String Host
        {
            get { return GetLocationPart(m => m.Host); }
            set { SetLocationPart(m => m.Host = value); }
        }

        public String HostName
        {
            get { return GetLocationPart(m => m.HostName); }
            set { SetLocationPart(m => m.HostName = value); }
        }

        public String PathName
        {
            get { return GetLocationPart(m => m.PathName); }
            set { SetLocationPart(m => m.PathName = value); }
        }

        public String Port
        {
            get { return GetLocationPart(m => m.Port); }
            set { SetLocationPart(m => m.Port = value); }
        }

        public String Protocol
        {
            get { return GetLocationPart(m => m.Protocol); }
            set { SetLocationPart(m => m.Protocol = value); }
        }

        public String UserName
        {
            get { return GetLocationPart(m => m.UserName); }
            set { SetLocationPart(m => m.UserName = value); }
        }

        public String Password
        {
            get { return GetLocationPart(m => m.Password); }
            set { SetLocationPart(m => m.Password = value); }
        }

        public String Search
        {
            get { return GetLocationPart(m => m.Search); }
            set { SetLocationPart(m => m.Search = value); }
        }

        public String Origin
        {
            get { return GetLocationPart(m => m.Origin); }
        }

        public String TargetLanguage
        {
            get { return this.GetOwnAttribute(AttributeNames.HrefLang); }
            set { this.SetOwnAttribute(AttributeNames.HrefLang, value); }
        }

        public String Media
        {
            get { return this.GetOwnAttribute(AttributeNames.Media); }
            set { this.SetOwnAttribute(AttributeNames.Media, value); }
        }

        public String Relation
        {
            get { return this.GetOwnAttribute(AttributeNames.Rel); }
            set { this.SetOwnAttribute(AttributeNames.Rel, value); }
        }

        public ITokenList RelationList
        {
            get
            {
                if (_relList == null)
                {
                    _relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
                    _relList.Changed += value => UpdateAttribute(AttributeNames.Rel, value);
                }

                return _relList;
            }
        }

        public ISettableTokenList Ping
        {
            get
            {
                if (_ping == null)
                {
                    _ping = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Ping));
                    _ping.Changed += value => UpdateAttribute(AttributeNames.Ping, value);
                }

                return _ping;
            }
        }

        public String Target
        {
            get { return this.GetOwnAttribute(AttributeNames.Target); }
            set { this.SetOwnAttribute(AttributeNames.Target, value); }
        }

        public String Type
        {
            get { return this.GetOwnAttribute(AttributeNames.Type); }
            set { this.SetOwnAttribute(AttributeNames.Type, value); }
        }

        #endregion

        #region Design properties

        internal Boolean IsVisited
        {
            get;
            set;
        }

        internal Boolean IsActive
        {
            get;
            set;
        }

        #endregion

        #region Helpers

        private String GetLocationPart(Func<ILocation, String> getter)
        {
            var href = this.GetOwnAttribute(AttributeNames.Href);
            var url = href != null ? new Url(BaseUrl, href) : null;

            if (url != null && !url.IsInvalid)
            {
                var location = new Location(url);
                return getter.Invoke(location);
            }

            return String.Empty;
        }

        private void SetLocationPart(Action<ILocation> setter)
        {
            var href = this.GetOwnAttribute(AttributeNames.Href);
            var url = href != null ? new Url(BaseUrl, href) : null;

            if (url == null || url.IsInvalid)
            {
                url = new Url(BaseUrl);
            }

            var location = new Location(url);
            setter.Invoke(location);
            this.SetOwnAttribute(AttributeNames.Href, location.Href);
        }

        #endregion
    }
}

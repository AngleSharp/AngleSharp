namespace AngleSharp.DOM
{
    using AngleSharp.Html;
    using System;

    sealed class ElementLocation : ILocation
    {
        #region Fields

        readonly IElement _parent;
        Location _location;
        Url _baseUrl;
        String _value;

        #endregion

        #region ctor

        public ElementLocation(IElement parent)
        {
            _parent = parent;
        }

        #endregion

        #region Properties

        public Location Location
        {
            get
            {
                var value = _parent.GetAttribute(AttributeNames.Href) ?? String.Empty;
                var baseUrl = _parent.BaseUrl;

                if (_location == null || !baseUrl.Equals(_baseUrl) || !value.Equals(_value, StringComparison.Ordinal))
                {
                    var url = new Url(baseUrl, value);
                    _baseUrl = baseUrl;
                    _value = value;
                    _location = new Location(url);
                }

                return _location;
            }
        }

        public String Href
        {
            get { return Location.Href; }
            set { Location.Href = value; Assign(value); }
        }

        public String Protocol
        {
            get { return Location.Protocol; }
            set { Location.Protocol = value; Reload(); }
        }

        public String Host
        {
            get { return Location.Host; }
            set { Location.Host = value; Reload(); }
        }

        public String HostName
        {
            get { return Location.HostName; }
            set { Location.HostName = value; Reload(); }
        }

        public String Port
        {
            get { return Location.Port; }
            set { Location.Port = value; Reload(); }
        }

        public String PathName
        {
            get { return Location.PathName; }
            set { Location.PathName = value; Reload(); }
        }

        public String Search
        {
            get { return Location.Search; }
            set { Location.Search = value; Reload(); }
        }

        public String Hash
        {
            get { return Location.Hash; }
            set { Location.Hash = value; Reload(); }
        }

        public String UserName
        {
            get { return Location.UserName; }
            set { Location.UserName = value; Reload(); }
        }

        public String Password
        {
            get { return Location.Password; }
            set { Location.Password = value; Reload(); }
        }

        public String Origin
        {
            get { return Location.Origin; }
        }

        #endregion

        #region Methods

        public void Assign(String url)
        {
            _parent.SetAttribute(AttributeNames.Href, url);
            _value = url;
        }

        public void Replace(String url)
        {
            Href = url;
        }

        public void Reload()
        {
            Assign(Href);
        }

        #endregion
    }
}

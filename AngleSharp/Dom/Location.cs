namespace AngleSharp.Dom
{
    using System;

    /// <summary>
    /// A location object with information about a Url.
    /// </summary>
    sealed class Location : ILocation
    {
        #region Fields

        readonly Url _url;

        #endregion

        #region Events

        /// <summary>
        /// Fired when the address is changed.
        /// </summary>
        public event EventHandler<LocationChangedEventArgs> Changed;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new location with no URL.
        /// </summary>
        internal Location()
            : this(String.Empty)
        {
        }

        /// <summary>
        /// Creates a new location based on the given URL.
        /// </summary>
        /// <param name="url">The URL to represent.</param>
        internal Location(String url)
            : this(new Url(url))
        {
        }

        /// <summary>
        /// Creates a new location based on the given URL.
        /// </summary>
        /// <param name="url">The URL to wrap.</param>
        internal Location(Url url)
        {
            _url = url ?? new Url(String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the original url object.
        /// </summary>
        internal Url Original
        {
            get { return _url; }
        }

        /// <summary>
        /// Gets the origin of the url.
        /// </summary>
        public String Origin
        {
            get { return _url.Origin; }
        }

        /// <summary>
        /// Gets if the stored location is relative and requires a base URL.
        /// </summary>
        public Boolean IsRelative
        {
            get { return _url.IsRelative; }
        }

        /// <summary>
        /// Gets or sets the username for authorization.
        /// </summary>
        public String UserName
        {
            get { return _url.UserName; }
            set { _url.UserName = value; }
        }

        /// <summary>
        /// Gets or sets the password for authorization.
        /// </summary>
        public String Password
        {
            get { return _url.Password; }
            set { _url.Password = value; }
        }

        /// <summary>
        /// Gets or sets the hash, e.g.  "#myhash".
        /// </summary>
        public String Hash
        {
            get { return NonEmptyPrefix(_url.Fragment, "#"); }
            set 
            {
                var old = _url.Href;

                if (value != null)
                {
                    if (value.Length > 0 && value[0] == Symbols.Num)
                        value = value.Substring(1);
                    else if (value.Length == 0)
                        value = null;
                }

                if (value != _url.Fragment)
                { 
                    _url.Fragment = value; 
                    RaiseChanged(old, true);
                } 
            }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        public String Host
        {
            get { return _url.Host; }
            set 
            {
                var old = _url.Href;

                if (value != _url.Host)
                {
                    _url.Host = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        public String HostName
        {
            get { return _url.HostName; }
            set 
            {
                var old = _url.Href;

                if (value != _url.HostName)
                {
                    _url.HostName = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        public String Href
        {
            get { return _url.Href; }
            set 
            {
                var old = _url.Href;

                if (value != _url.Href)
                {
                    _url.Href = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        public String PathName
        {
            get
            {
                var data = _url.Data;
                return String.IsNullOrEmpty(data) ? "/" + _url.Path : data;
            }
            set 
            {
                var old = _url.Href;

                if (value != _url.Path)
                {
                    _url.Path = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800"
        /// </summary>
        public String Port
        {
            get { return _url.Port; }
            set 
            {
                var old = _url.Href;

                if (value != _url.Port)
                {
                    _url.Port = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        public String Protocol
        {
            get { return NonEmptyPostfix(_url.Scheme, ":"); }
            set 
            {
                var old = _url.Href;

                if (value != _url.Scheme)
                {
                    _url.Scheme = value;
                    RaiseChanged(old, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the query, e.g. "?id=...".
        /// </summary>
        public String Search
        {
            get { return NonEmptyPrefix(_url.Query, "?"); }
            set 
            {
                var old = _url.Href;

                if (value != _url.Query)
                {
                    _url.Query = value;
                    RaiseChanged(old, false);
                }
            }
        }

        #endregion

        #region Methods

        public void Assign(String url)
        {
            _url.Href = url;
        }

        public void Replace(String url)
        {
            _url.Href = url;
        }

        public void Reload()
        {
            _url.Href = Href;
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        public override String ToString()
        {
            return _url.Href;
        }

        #endregion

        #region Helpers

        void RaiseChanged(String oldAddress, Boolean hashChanged)
        {
            if (Changed != null)
                Changed(this, new LocationChangedEventArgs(hashChanged, oldAddress, _url.Href));
        }

        static String NonEmptyPrefix(String check, String prefix)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return String.Concat(prefix, check);
        }

        static String NonEmptyPostfix(String check, String postfix)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return String.Concat(check, postfix);
        }

        #endregion

        #region Event Arguments

        /// <summary>
        /// Event Arguments for the location changed event.
        /// </summary>
        public sealed class LocationChangedEventArgs : EventArgs
        {
            public LocationChangedEventArgs(Boolean hashChanged, String previousLocation, String currentLocation)
            {
                IsHashChanged = hashChanged;
                PreviousLocation = previousLocation;
                CurrentLocation = currentLocation;
            }

            public Boolean IsHashChanged
            {
                get;
                private set;
            }

            public String PreviousLocation
            {
                get;
                private set;
            }

            public String CurrentLocation
            {
                get;
                private set;
            }
        }

        #endregion
    }
}

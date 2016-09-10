namespace AngleSharp.Dom
{
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// A location object with information about a Url.
    /// </summary>
    sealed class Location : ILocation
    {
        #region Fields

        private readonly Url _url;

        #endregion

        #region Events

        public event EventHandler<LocationChangedEventArgs> Changed;

        #endregion

        #region ctor

        internal Location()
            : this(String.Empty)
        {
        }

        internal Location(String url)
            : this(new Url(url))
        {
        }

        internal Location(Url url)
        {
            _url = url ?? new Url(String.Empty);
        }

        #endregion

        #region Properties

        public Url Original
        {
            get { return _url; }
        }

        public String Origin
        {
            get { return _url.Origin; }
        }

        public Boolean IsRelative
        {
            get { return _url.IsRelative; }
        }

        public String UserName
        {
            get { return _url.UserName; }
            set { _url.UserName = value; }
        }

        public String Password
        {
            get { return _url.Password; }
            set { _url.Password = value; }
        }

        public String Hash
        {
            get { return NonEmptyPrefix(_url.Fragment, "#"); }
            set 
            {
                var old = _url.Href;

                if (value != null)
                {
                    if (value.Has(Symbols.Num))
                    {
                        value = value.Substring(1);
                    }
                    else if (value.Length == 0)
                    {
                        value = null;
                    }
                }

                if (value != _url.Fragment)
                { 
                    _url.Fragment = value; 
                    RaiseChanged(old, true);
                } 
            }
        }

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

        public override String ToString()
        {
            return _url.Href;
        }

        #endregion

        #region Helpers

        private void RaiseChanged(String oldAddress, Boolean hashChanged)
        {
            Changed?.Invoke(this, new LocationChangedEventArgs(hashChanged, oldAddress, _url.Href));
        }

        private static String NonEmptyPrefix(String check, String prefix)
        {
            return String.IsNullOrEmpty(check) ? String.Empty : String.Concat(prefix, check);
        }

        private static String NonEmptyPostfix(String check, String postfix)
        {
            return String.IsNullOrEmpty(check) ? String.Empty : String.Concat(check, postfix);
        }

        #endregion

        #region Event Arguments

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

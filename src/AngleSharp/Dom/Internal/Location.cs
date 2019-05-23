namespace AngleSharp.Dom
{
    using AngleSharp.Text;
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

        public event EventHandler<ChangedEventArgs> Changed;

        #endregion

        #region ctor

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

        public Url Original => _url;

        public String Origin => _url.Origin;

        public Boolean IsRelative => _url.IsRelative;

        public String UserName
        {
            get => _url.UserName;
            set => _url.UserName = value;
        }

        public String Password
        {
            get => _url.Password;
            set => _url.Password = value;
        }

        public String Hash
        {
            get => NonEmptyPrefix(_url.Fragment, "#");
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

                if (!value.Is(_url.Fragment))
                {
                    _url.Fragment = value;
                    RaiseHashChanged(old);
                }
            }
        }

        public String Host
        {
            get => _url.Host;
            set
            {
                var old = _url.Href;

                if (!value.Isi(_url.Host))
                {
                    _url.Host = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String HostName
        {
            get => _url.HostName;
            set
            {
                var old = _url.Href;

                if (!value.Isi(_url.HostName))
                {
                    _url.HostName = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Href
        {
            get => _url.Href;
            set
            {
                var old = _url.Href;

                if (!value.Is(_url.Href))
                {
                    _url.Href = value;
                    RaiseLocationChanged(old);
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

                if (!value.Is(_url.Path))
                {
                    _url.Path = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Port
        {
            get => _url.Port;
            set
            {
                var old = _url.Href;

                if (!value.Isi(_url.Port))
                {
                    _url.Port = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Protocol
        {
            get => NonEmptyPostfix(_url.Scheme, ":");
            set
            {
                var old = _url.Href;

                if (!value.Isi(_url.Scheme))
                {
                    _url.Scheme = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Search
        {
            get => NonEmptyPrefix(_url.Query, "?");
            set
            {
                var old = _url.Href;

                if (!value.Is(_url.Query))
                {
                    _url.Query = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        #endregion

        #region Methods

        public void Assign(String url)
        {
            var old = _url.Href;

            if (!old.Is(url))
            {
                _url.Href = url;
                RaiseLocationChanged(old);
            }
        }

        public void Replace(String url)
        {
            var old = _url.Href;

            if (!old.Is(url))
            {
                _url.Href = url;
                RaiseLocationChanged(old);
            }
        }

        public void Reload() => Changed?.Invoke(this, new ChangedEventArgs(false, _url.Href, _url.Href));

        public override String ToString() => _url.Href;

        #endregion

        #region Helpers

        private void RaiseHashChanged(String oldAddress) =>
            Changed?.Invoke(this, new ChangedEventArgs(true, oldAddress, _url.Href));

        private void RaiseLocationChanged(String oldAddress) =>
            Changed?.Invoke(this, new ChangedEventArgs(false, oldAddress, _url.Href));

        private static String NonEmptyPrefix(String check, String prefix) =>
            String.IsNullOrEmpty(check) ? String.Empty : String.Concat(prefix, check);

        private static String NonEmptyPostfix(String check, String postfix) =>
            String.IsNullOrEmpty(check) ? String.Empty : String.Concat(check, postfix);

        #endregion

        #region Event Arguments

        public sealed class ChangedEventArgs : EventArgs
        {
            public ChangedEventArgs(Boolean hashChanged, String previousLocation, String currentLocation)
            {
                IsHashChanged = hashChanged;
                PreviousLocation = previousLocation;
                CurrentLocation = currentLocation;
            }

            public Boolean IsReloaded => PreviousLocation.Is(CurrentLocation);

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

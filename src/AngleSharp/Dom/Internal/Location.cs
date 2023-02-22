namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Diagnostics.CodeAnalysis;

    /// <summary>
    /// A location object with information about a Url.
    /// </summary>
    sealed class Location : ILocation
    {
        #region Fields

        #endregion

        #region Events

        public event EventHandler<ChangedEventArgs>? Changed;

        #endregion

        #region ctor

        internal Location(String url)
            : this(new Url(url))
        {
        }

        internal Location(Url url)
        {
            Original = url ?? new Url(String.Empty);
        }

        #endregion

        #region Properties

        public Url Original { get; }

        public String? Origin => Original.Origin;

        public Boolean IsRelative => Original.IsRelative;

        public String? UserName
        {
            get => Original.UserName;
            set => Original.UserName = value;
        }

        public String? Password
        {
            get => Original.Password;
            set => Original.Password = value;
        }

        [AllowNull]
        public String Hash
        {
            get => NonEmptyPrefix(Original.Fragment, "#");
            set
            {
                var old = Original.Href;

                if (value != null)
                {
                    if (value.Has(Symbols.Num))
                    {
                        value = value.Substring(1);
                    }
                    else if (value.Length == 0)
                    {
                        value = null!;
                    }
                }

                if (!value.Is(Original.Fragment))
                {
                    Original.Fragment = value;
                    RaiseHashChanged(old);
                }
            }
        }

        public String Host
        {
            get => Original.Host;
            set
            {
                var old = Original.Href;

                if (!value.Isi(Original.Host))
                {
                    Original.Host = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String HostName
        {
            get => Original.HostName;
            set
            {
                var old = Original.Href;

                if (!value.Isi(Original.HostName))
                {
                    Original.HostName = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Href
        {
            get => Original.Href;
            set
            {
                var old = Original.Href;

                if (!value.Is(Original.Href))
                {
                    Original.Href = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String PathName
        {
            get
            {
                var data = Original.Data;
                return String.IsNullOrEmpty(data) ? "/" + Original.Path : data;
            }
            set
            {
                var old = Original.Href;

                if (!value.Is(Original.Path))
                {
                    Original.Path = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Port
        {
            get => Original.Port;
            set
            {
                var old = Original.Href;

                if (!value.Isi(Original.Port))
                {
                    Original.Port = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Protocol
        {
            get => NonEmptyPostfix(Original.Scheme, ":");
            set
            {
                var old = Original.Href;

                if (!value.Isi(Original.Scheme))
                {
                    Original.Scheme = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        public String Search
        {
            get => NonEmptyPrefix(Original.Query, "?");
            set
            {
                var old = Original.Href;

                if (!value.Is(Original.Query))
                {
                    Original.Query = value;
                    RaiseLocationChanged(old);
                }
            }
        }

        #endregion

        #region Methods

        public void Assign(String url)
        {
            var old = Original.Href;

            if (!old.Is(url))
            {
                Original.Href = url;
                RaiseLocationChanged(old);
            }
        }

        public void Replace(String url)
        {
            var old = Original.Href;

            if (!old.Is(url))
            {
                Original.Href = url;
                RaiseLocationChanged(old);
            }
        }

        public void Reload() => Changed?.Invoke(this, new ChangedEventArgs(false, Original.Href, Original.Href));

        public override String ToString() => Original.Href;

        #endregion

        #region Helpers

        private void RaiseHashChanged(String oldAddress) =>
            Changed?.Invoke(this, new ChangedEventArgs(true, oldAddress, Original.Href));

        private void RaiseLocationChanged(String oldAddress) =>
            Changed?.Invoke(this, new ChangedEventArgs(false, oldAddress, Original.Href));

        private static String NonEmptyPrefix(String? check, String prefix) =>
            String.IsNullOrEmpty(check) ? String.Empty : String.Concat(prefix, check);

        private static String NonEmptyPostfix(String? check, String postfix) =>
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

            public Boolean IsHashChanged { get; }

            public String PreviousLocation { get; }

            public String CurrentLocation { get; }
        }

        #endregion
    }
}

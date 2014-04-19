namespace AngleSharp.DOM
{
    using System;
    using System.Text.RegularExpressions;

    /// <summary>
    /// A location object with information about a URL.
    /// More information is available at:
    /// http://url.spec.whatwg.org/
    /// </summary>
    [DOM("Location")]
    public sealed class Location : ICssObject
    {
        #region Regular expression

        static readonly Regex parser = new Regex(@"^(?:([A-Za-z]+):)?(\/{0,3})([0-9.\-A-Za-z]+)(?::(\d+))?(?:\/([^?#]*))?(?:\?([^#]*))?(?:#(.*))?$");

        #endregion

        #region Fields

        String _url;
        String _scheme;
        String _slash;
        String _host;
        String _port;
        String _path;
        String _query;
        String _hash;

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
        {
            ChangeTo(url ?? String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hash, e.g.  "#myhash".
        /// </summary>
        [DOM("hash")]
        public String Hash
        {
            get { return NonEmpty(_hash, "#"); }
            set { _hash = Tolerate(value, "#"); TryRebuild(); }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        [DOM("host")]
        public String Host
        {
            get { return HostName + NonEmpty(_port, ":"); }
            set 
            {
                var index = value.IndexOf(':');

                if (index != -1)
                {
                    Port = value.Substring(index);
                    HostName = value.Substring(0, index);
                }
                else
                {
                    Port = String.Empty;
                    HostName = value;
                }

                TryRebuild();
            }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        [DOM("hostname")]
        public String HostName
        {
            get { return _host; }
            set { _host = value; TryRebuild(); }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return _url; }
            set { ChangeTo(value ?? String.Empty); }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        [DOM("pathname")]
        public String PathName
        {
            get { return "/" + _path; }
            set { _path = Tolerate(value, "/"); TryRebuild(); }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800"
        /// </summary>
        [DOM("port")]
        public String Port
        {
            get { return _port; }
            set { _port = value; TryRebuild(); }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        [DOM("protocol")]
        public String Protocol
        {
            get { return NonEmpty(_scheme, postfix : ":"); }
            set { _scheme = Tolerate(value, postfix: ":"); TryRebuild(); }
        }

        /// <summary>
        /// Gets or sets the query, e.g. "?id=...".
        /// </summary>
        [DOM("search")]
        public String Search
        {
            get { return NonEmpty(_query, "?"); }
            set { _query = Tolerate(value, "?"); TryRebuild(); }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns an Uri representation of the currently stored location.
        /// </summary>
        /// <returns>The uri instance.</returns>
        public Uri ToUri()
        {
            return new Uri(_url);
        }

        /// <summary>
        /// Returns the CSS representation of the given URL.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Url, String.Concat("'", _url, "'"));
        }

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        public override String ToString()
        {
            return _url;
        }

        #endregion

        #region Helpers

        static String NonEmpty(String check, String prefix = null, String postfix = null)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return (prefix ?? String.Empty) + check + (postfix ?? String.Empty);
        }

        static String Tolerate(String value, String prefix = null, String postfix = null)
        {
            if (prefix != null && value.StartsWith(prefix))
                return value.Substring(prefix.Length);
            else if (postfix != null && value.EndsWith(postfix))
                return value.Substring(0, value.Length - postfix.Length);

            return value;
        }

        static String Get(GroupCollection groups, Int32 index)
        {
            if (groups.Count > index)
                return groups[index].Value;

            return null;
        }

        void TryRebuild()
        {
            var url = Protocol + _slash + Host + PathName + Search + Hash;
            ChangeTo(url);
        }

        void ChangeTo(String value)
        {
            var m = parser.Match(value);

            if (m.Success)
            {
                _url = Get(m.Groups, 0);
                _scheme = Get(m.Groups, 1);
                _slash = Get(m.Groups, 2);
                _host = Get(m.Groups, 3);
                _port = Get(m.Groups, 4);
                _path = Get(m.Groups, 5);
                _query = Get(m.Groups, 6);
                _hash = Get(m.Groups, 7);
            }
        }

        #endregion

        #region Internal Helpers

        /// <summary>
        /// Checks if the given URL is an absolute URI.
        /// </summary>
        /// <param name="url">The given URL.</param>
        /// <returns>True if the url is absolute otherwise false.</returns>
        internal static Boolean IsAbsolute(String url)
        {
            Uri result;
            return Uri.TryCreate(url, UriKind.Absolute, out result);
        }

        /// <summary>
        /// Creates an absolute URI out of the given baseURI and (relative) URL.
        /// </summary>
        /// <param name="basePath">The baseURI of the page or element.</param>
        /// <param name="relativePath">The relative path for the URL creation.</param>
        /// <returns>THe absolute URI created out of the baseURI and pointing to the relative path.</returns>
        internal static String MakeAbsolute(String basePath, String relativePath)
        {
            Uri baseUri;
            
            if (Uri.TryCreate(basePath, UriKind.Absolute, out baseUri))
                return new Uri(baseUri, relativePath).ToString();

            return relativePath;
        }

        #endregion
    }
}

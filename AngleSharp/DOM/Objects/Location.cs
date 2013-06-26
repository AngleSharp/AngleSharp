using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// A location object with information about a URL.
    /// </summary>
    [DOM("Location")]
    public sealed class Location
    {
        #region Members

        string _host;
        string _protocol;
        string _hash;
        string _path;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new location based on the given URL.
        /// </summary>
        /// <param name="url">The URL to represent.</param>
        internal Location(string url)
        {
            ChangeTo(url);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the hash, e.g.  "#myhash".
        /// </summary>
        [DOM("hash")]
        public String Hash
        {
            get { return _hash; }
            set { _hash = ValidateHash(value); }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        [DOM("host")]
        public String Host
        {
            get { return _host; }
            set 
            {
                var index = value.IndexOf(':');

                if (index != -1)
                {
                    var port = value.Substring(index);
                    value = value.Substring(0, index);
                    Port = port;
                    HostName = value;
                }
                else
                    _host = ValidateHostName(value, _host); 
            }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        [DOM("hostname")]
        public String HostName
        {
            get { return _host.Contains(':') ? _host.Substring(0, _host.IndexOf(':')) : _host; }
            set { _host = ValidateHostName(value, HostName) + (String.IsNullOrEmpty(Port) ? String.Empty : (":" + Port)); }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return String.Format("{0}//{1}{2}{3}", Protocol, Host, PathName, Hash); }
            set { ChangeTo(value); }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        [DOM("pathname")]
        public String PathName
        {
            get { return _path; }
            set { _path = ValidatePath(value); }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800"
        /// </summary>
        [DOM("port")]
        public String Port
        {
            get { return _host.Contains(':') ? _host.Substring(_host.IndexOf(':') + 1) : String.Empty; }
            set { _host = HostName + ValidatePort(value); }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        [DOM("protocol")]
        public String Protocol
        {
            get { return _protocol; }
            set { _protocol = ValidateProtocol(value, _protocol); }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        public override String ToString()
        {
            return Href;
        }

        #endregion

        #region Helpers

        String ValidateProtocol(String value, String original)
        {
            if (String.IsNullOrEmpty(value))
                return "http:";

            value = value.ToLower();

            if (value.Length != 0 && value[value.Length - 1] != ':')
                value += ":";

            switch (value)
            {
                case "ftp:":
                case "http:":
                case "https:":
                    return value;

                default:
                    return original;
            }
        }

        String ValidateHash(String value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            if (value[0] != '#')
                return "#" + value;

            return value;
        }

        String ValidateHostName(String value, String original)
        {
            if (Uri.CheckHostName(value) == UriHostNameType.Unknown)
                return original;

            return value;
        }

        String ValidatePath(String value)
        {
            if (value.Length == 0)
                return "/";

            if (value[0] != '/')
                value = "/" + value;

            var index = value.IndexOf('#');

            if (index != -1)
            {
                _hash = value.Substring(index);
                value = value.Substring(0, index);
            }

            return value;
        }

        String ValidatePort(String value)
        {
            if (String.IsNullOrEmpty(value))
                return String.Empty;

            var port = value;

            if (value[0] != ':')
                value = ":" + value;
            else
                port = value.Substring(1);

            if (port == "80")
                return String.Empty;

            int prt;

            if (Int32.TryParse(port, out prt))
                return value;

            return String.Empty;
        }

        void ChangeTo(String value)
        {
            _protocol = String.Empty;
            _host = String.Empty;
            _hash = String.Empty;
            _path = String.Empty;

            if (!String.IsNullOrEmpty(value))
            {
                var index = value.IndexOf("//");

                if (index != -1)
                {
                    Protocol = value.Substring(0, index);
                    value = value.Substring(index + 2);
                }

                index = value.IndexOf('/');

                if (index != -1)
                {
                    Host = value.Substring(0, index);
                    PathName = value.Substring(index);
                }
                else
                    Host = value;
            }
        }

        #endregion
    }
}

using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// A location object with information about a URL.
    /// </summary>
    public class Location
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
        /// Gets the hash, e.g.  "#myhash".
        /// </summary>
        public string Hash
        {
            get { return _hash; }
            set { _hash = ValidateHash(value); }
        }

        /// <summary>
        /// Gets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        public string Host
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
        /// Gets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        public string HostName
        {
            get { return _host.Contains(':') ? _host.Substring(0, _host.IndexOf(':')) : _host; }
            set { _host = ValidateHostName(value, HostName) + (string.IsNullOrEmpty(Port) ? string.Empty : (":" + Port)); }
        }

        /// <summary>
        /// Gets the hyper reference, i.e. the full path.
        /// </summary>
        public string Href
        {
            get { return string.Format("{0}//{1}{2}{3}", Protocol, Host, PathName, Hash); }
            set { ChangeTo(value); }
        }

        /// <summary>
        /// Gets the pathname, e.g. "/mypath".
        /// </summary>
        public string PathName
        {
            get { return _path; }
            set { _path = ValidatePath(value); }
        }

        /// <summary>
        /// Gets the port, e.g. "8800"
        /// </summary>
        public string Port
        {
            get { return _host.Contains(':') ? _host.Substring(_host.IndexOf(':') + 1) : string.Empty; }
            set { _host = HostName + ValidatePort(value); }
        }

        /// <summary>
        /// Gets the protocol, e.g. "http:".
        /// </summary>
        public string Protocol
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
        public override string ToString()
        {
            return Href;
        }

        #endregion

        #region Helpers

        string ValidateProtocol(string value, string original)
        {
            if (string.IsNullOrEmpty(value))
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

        string ValidateHash(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            if (value[0] != '#')
                return "#" + value;

            return value;
        }

        string ValidateHostName(string value, string original)
        {
            if (Uri.CheckHostName(value) == UriHostNameType.Unknown)
                return original;

            return value;
        }

        string ValidatePath(string value)
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

        string ValidatePort(string value)
        {
            if (string.IsNullOrEmpty(value))
                return string.Empty;

            var port = value;

            if (value[0] != ':')
                value = ":" + value;
            else
                port = value.Substring(1);

            if (port == "80")
                return string.Empty;

            int prt;

            if (int.TryParse(port, out prt))
                return value;

            return string.Empty;
        }

        void ChangeTo(string value)
        {
            _protocol = string.Empty;
            _host = string.Empty;
            _hash = string.Empty;
            _path = string.Empty;

            if (!string.IsNullOrEmpty(value))
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

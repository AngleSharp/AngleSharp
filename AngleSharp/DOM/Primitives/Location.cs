namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// A location object with information about a URL.
    /// More information is available at:
    /// http://url.spec.whatwg.org/
    /// </summary>
    [DOM("Location")]
    public sealed class Location : ICssObject
    {
        #region Fields

        Boolean _relative;
        String _username;
        String _password;
        String _scheme;
        String _schemeData;
        String _host;
        String _port;
        String _path;
        String _query;
        String _fragment;

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
            _relative = false;
            _username = null;
            _password = null;
            _scheme = String.Empty;
            _schemeData = String.Empty;
            _host = String.Empty;
            _port = String.Empty;
            _path = String.Empty;
            _query = null;
            _fragment = null;
            ChangeTo(url ?? String.Empty);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the stored location is relative and requires
        /// a base URL.
        /// </summary>
        public Boolean IsRelative
        {
            get { return _relative && String.IsNullOrEmpty(_scheme); }
        }

        /// <summary>
        /// Gets or sets the username for authorization.
        /// </summary>
        public String UserName
        {
            get { return _username; }
            set { _username = value; }
        }

        /// <summary>
        /// Gets or sets the password for authorization.
        /// </summary>
        public String Password
        {
            get { return _password; }
            set { _password = value; }
        }

        /// <summary>
        /// Gets the additional stored data of the URL.
        /// This is data that could not be assigned.
        /// </summary>
        public String Data
        {
            get { return _schemeData; }
        }

        /// <summary>
        /// Gets or sets the hash, e.g.  "#myhash".
        /// </summary>
        [DOM("hash")]
        public String Hash
        {
            get { return NonEmpty(_fragment, "#"); }
            set { ParseFragment(value ?? String.Empty, 0); }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        [DOM("host")]
        public String Host
        {
            get { return HostName + NonEmpty(_port, ":"); }
            set { ParseHostName(value ?? String.Empty, 0, false, true); }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        [DOM("hostname")]
        public String HostName
        {
            get { return _host; }
            set { ParseHostName(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        [DOM("href")]
        public String Href
        {
            get { return ToString(); }
            set { ChangeTo(value ?? String.Empty); }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        [DOM("pathname")]
        public String PathName
        {
            get { return "/" + _path; }
            set { ParsePath(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800"
        /// </summary>
        [DOM("port")]
        public String Port
        {
            get { return _port; }
            set { ParsePort(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        [DOM("protocol")]
        public String Protocol
        {
            get { return NonEmpty(_scheme, postfix : ":"); }
            set { ParseScheme(value ?? String.Empty, true); }
        }

        /// <summary>
        /// Gets or sets the query, e.g. "?id=...".
        /// </summary>
        [DOM("search")]
        public String Search
        {
            get { return NonEmpty(_query, "?"); }
            set { ParseQuery(value ?? String.Empty, 0, true); }
        }

        #endregion

        #region String representation

        /// <summary>
        /// Returns the CSS representation of the given URL.
        /// </summary>
        /// <returns>The CSS value string.</returns>
        public String ToCss()
        {
            return FunctionNames.Build(FunctionNames.Url, String.Concat("'", ToString(), "'"));
        }

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        public override String ToString()
        {
            var output = Pool.NewStringBuilder();

            if (!String.IsNullOrEmpty(_scheme))
                output.Append(_scheme).Append(Specification.Colon);

            if (_relative)
            {
                if (!String.IsNullOrEmpty(_host) || !String.IsNullOrEmpty(_scheme))
                {
                    output.Append(Specification.Solidus).Append(Specification.Solidus);

                    if (!String.IsNullOrEmpty(_username))
                        output.Append(_username);

                    if (!String.IsNullOrEmpty(_password))
                        output.Append(Specification.Colon).Append(_password);

                    if (!String.IsNullOrEmpty(_username) || !String.IsNullOrEmpty(_password))
                        output.Append(Specification.At);

                    output.Append(_host);

                    if (!String.IsNullOrEmpty(_port))
                        output.Append(Specification.Colon).Append(_port);

                    output.Append(Specification.Solidus);
                }

                output.Append(_path);
            }
            else
                output.Append(_schemeData);

            if (!String.IsNullOrEmpty(_query))
                output.Append(Specification.QuestionMark).Append(_query);

            if (!String.IsNullOrEmpty(_fragment))
                output.Append(Specification.Num).Append(_fragment);

            return output.ToPool();
        }

        #endregion

        #region Helpers

        static String NonEmpty(String check, String prefix = null, String postfix = null)
        {
            if (String.IsNullOrEmpty(check))
                return String.Empty;

            return String.Concat(prefix ?? String.Empty, check, postfix ?? String.Empty);
        }

        /// <summary>
        /// This tries to match the specification of RFC 3986
        /// http://tools.ietf.org/html/rfc3986
        /// </summary>
        /// <param name="url">The url to parse.</param>
        void ChangeTo(String url)
        {
            url = url.Trim();
            ParseScheme(url.Trim());
        }

        #endregion

        #region Parsing

        void ParseUrl(String input, Location baseUrl = null)
        {
            input = input.Trim();

            if (baseUrl != null)
            {
                _scheme = baseUrl._scheme;
                _host = baseUrl._host;
                _path = baseUrl._path;
                _port = baseUrl._port;
            }

            ParseScheme(input.Trim());
        }

        Boolean ParseScheme(String input, Boolean onlyScheme = false)
        {
            if (input.Length > 0 && input[0].IsLetter())
            {
                var index = 1;

                while (index < input.Length)
                {
                    var c = input[index];

                    if (c.IsAlphanumericAscii() || c == Specification.Plus || c == Specification.Minus || c == Specification.Dot)
                        index++;
                    else if (c == Specification.Colon)
                    {
                        var originalScheme = _scheme;
                        _scheme = input.Substring(0, index).ToLower();

                        if (onlyScheme)
                            return true;

                        _relative = KnownProtocols.IsRelative(_scheme);

                        if (_scheme == KnownProtocols.File)
                            return RelativeState(input, index + 1);
                        else if (!_relative)
                            return ParseSchemeData(input, index + 1);
                        else if (originalScheme == _scheme)
                        {
                            c = input[++index];

                            if (c == Specification.Solidus && index + 2 < input.Length && input[index + 1] == Specification.Solidus)
                                return IgnoreSlashesState(input, index + 2);

                            return RelativeState(input, index);
                        }

                        if (input[++index] == Specification.Solidus && ++index < input.Length && input[index] == Specification.Solidus)
                            return IgnoreSlashesState(input, index + 1);

                        return IgnoreSlashesState(input, index);
                    }
                    else
                        break;
                }
            }

            if (onlyScheme)
                return false;

            return RelativeState(input, 0);
        }

        Boolean ParseSchemeData(String input, Int32 index)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Specification.QuestionMark)
                {
                    _schemeData = buffer.ToPool();
                    return ParseQuery(input, index + 1);
                }
                else if (c == Specification.Num)
                {
                    _schemeData = buffer.ToPool();
                    return ParseFragment(input, index + 1);
                }
                else if (c == Specification.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c.IsInRange(0x20, 0x7e))
                    buffer.Append(c);
                else
                    buffer.Append(Specification.Percent).Append(((Byte)input[index]).ToString("X2"));

                index++;
            }

            _schemeData = buffer.ToPool();
            return true;
        }

        Boolean RelativeState(String input, Int32 index)
        {
            _relative = true;

            if (index == input.Length)
                return true;

            switch (input[index])
            {
                case Specification.QuestionMark:
                    return ParseQuery(input, index + 1);
            
                case Specification.Num:
                    return ParseFragment(input, index + 1);

                case Specification.Solidus:
                case Specification.ReverseSolidus:
                    if (index == input.Length - 1)
                        return ParsePath(input, index);

                    var c = input[++index];

                    if (c == Specification.Solidus || c == Specification.ReverseSolidus)
                    {
                        if (_scheme == KnownProtocols.File)
                            return ParseFileHost(input, index + 1);

                        return IgnoreSlashesState(input, index + 1);
                    }
                    else if (_scheme == KnownProtocols.File)
                    {
                        _host = String.Empty;
                        _port = String.Empty;
                    }

                    return ParsePath(input, index);
            }

            if (input[index].IsLetter() && _scheme == "file" && index + 1 < input.Length && (input[index + 1] == Specification.Colon || input[index + 1] == Specification.Solidus) &&
                (index + 2 >= input.Length || input[index + 2] == Specification.Solidus || input[index + 2] == Specification.ReverseSolidus || input[index + 2] == Specification.Num || input[index + 2] == Specification.QuestionMark))
            {
                _host = String.Empty;
                _path = String.Empty;
                _port = String.Empty;
            }

            return ParsePath(input, index);
        }

        Boolean IgnoreSlashesState(String input, Int32 index)
        {
            while (index < input.Length)
            {
                if (input[index] != Specification.ReverseSolidus && input[index] != Specification.Solidus)
                    return ParseAuthority(input, index);

                index++;
            }

            return false;
        }

        Boolean ParseAuthority(String input, Int32 index)
        {
            var start = index;
            var buffer = Pool.NewStringBuilder();
            String user = null;
            String pass = null;

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Specification.At)
                {
                    if (user == null)
                    {
                        user = buffer.ToString();
                        buffer.Clear();
                    }
                    
                    pass = buffer.ToString();
                    start = index + 1;
                    _username = user;
                    _password = pass;
                    break;
                }
                else if (c == Specification.Colon)
                {
                    user = buffer.ToString();
                    pass = String.Empty;
                    buffer.Clear();
                }
                else if (c == Specification.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                    buffer.Append(input[index++]).Append(input[index++]).Append(input[index]);
                else if (c == Specification.Solidus || c == Specification.ReverseSolidus || c == Specification.Num || c == Specification.QuestionMark)
                    break;
                else if (c.IsInRange(0x20, 0x7e) && c != Specification.Space && c != Specification.DoubleQuote && c != Specification.CurvedQuote && c != Specification.LessThan && c != Specification.GreaterThan)
                    buffer.Append(c);
                else
                    buffer.Append(Specification.Percent).Append(((Byte)c).ToString("X2"));

                index++;
            }

            buffer.ToPool();
            return ParseHostName(input, start);
        }

        Boolean ParseFileHost(String input, Int32 index)
        {
            var start = index;

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Specification.Solidus || c == Specification.ReverseSolidus || c == Specification.Num || c == Specification.QuestionMark)
                    break;

                index++;
            }

            var length = index - start;

            if (length == 2 && input[index - 2].IsLetter() && (input[index - 1] == Specification.Pipe || input[index - 1] == Specification.Colon))
                return ParsePath(input, index);
            else if (length != 0)
                _host = input.Substring(start, length);

            return ParsePath(input, index);
        }

        Boolean ParseHostName(String input, Int32 index, Boolean onlyHost = false, Boolean onlyPort = false)
        {
            var inBracket = false;
            var start = index;

            while (index < input.Length)
            {
                var c = input[index];

                switch (c)
                {
                    case Specification.SquareBracketClose:
                        inBracket = false;
                        break;

                    case Specification.SquareBracketOpen:
                        inBracket = true;
                        break;

                    case Specification.Colon:
                        if (inBracket)
                            break;

                        _host = input.Substring(start, index - start);

                        if (onlyHost)
                            return true;

                        return ParsePort(input, index + 1, onlyPort);

                    case Specification.Solidus:
                    case Specification.ReverseSolidus:
                    case Specification.Num:
                    case Specification.QuestionMark:
                        _host = input.Substring(start, index - start);

                        if (onlyHost)
                            return true;

                        return ParsePath(input, index);
                }

                index++;
            }

            _host = input.Substring(start, index - start);

            if (!onlyHost)
            {
                _query = String.Empty;
                _path = String.Empty;
                _fragment = String.Empty;
            }

            return true;
        }

        Boolean ParsePort(String input, Int32 index, Boolean onlyPort = false)
        {
            var start = index;

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Specification.QuestionMark || c == Specification.Solidus || c == Specification.ReverseSolidus || c == Specification.Num)
                    break;
                else if (!c.IsDigit())
                    return false;

                index++;
            }

            _port = input.Substring(start, index - start).TrimStart(new[] { '0' });

            if (DefaultPorts.GetDefaultPort(_scheme) == _port)
                _port = String.Empty;

            if (onlyPort)
                return true;

            return ParsePath(input, index);
        }

        Boolean ParsePath(String input, Int32 index, Boolean onlyPath = false)
        {
            while (index < input.Length && (input[index] == Specification.Solidus || input[index] == Specification.ReverseSolidus))
                index++;

            var paths = new List<String>();
            var buffer = Pool.NewStringBuilder();

            while (index <= input.Length)
            {
                var c = index == input.Length ? Specification.EndOfFile : input[index];
                var breakNow = !onlyPath && (c == Specification.Num || c == Specification.QuestionMark);

                if (c == Specification.EndOfFile || c == Specification.Solidus || c == Specification.ReverseSolidus || breakNow)
                {
                    var path = buffer.ToString();
                    buffer.Clear();

                    if (path.Equals("%2e", StringComparison.OrdinalIgnoreCase))
                        path = ".";
                    else if (path.Equals(".%2e", StringComparison.OrdinalIgnoreCase) || path.Equals("%2e.", StringComparison.OrdinalIgnoreCase) || path.Equals("%2e%2e", StringComparison.OrdinalIgnoreCase))
                        path = "..";

                    if (path.Equals(".."))
                    {
                        if (paths.Count > 0)
                            paths.RemoveAt(paths.Count - 1);
                    }
                    else if (!path.Equals("."))
                    {
                        if (_scheme == KnownProtocols.File && paths.Count == 0 && path.Length == 2 && path[0].IsLetter() && path[1] == Specification.Pipe)
                            path = path.Replace(Specification.Pipe, Specification.Colon);

                        paths.Add(path);
                    }

                    if (breakNow)
                        break;
                }
                else if (c == Specification.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c.IsInRange(0x20, 0x7e) && c != Specification.Space && c != Specification.DoubleQuote && c != Specification.CurvedQuote &&
                    c != Specification.Num && c != Specification.LessThan && c != Specification.GreaterThan && c != Specification.QuestionMark)
                    buffer.Append(c);
                else
                    buffer.Append(Specification.Percent).Append(((Byte)c).ToString("X2"));

                index++;
            }

            buffer.ToPool();
            _path = String.Join("/", paths);

            if (index < input.Length)
            {
                if (input[index] == Specification.QuestionMark)
                    return ParseQuery(input, index + 1);

                return ParseFragment(input, index + 1);
            }

            return true;
        }

        Boolean ParseQuery(String input, Int32 index, Boolean onlyQuery = false)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < input.Length)
            {
                var c = input[index];

                if (!onlyQuery && input[index] == Specification.Num)
                    break;

                if (c.IsInRange(0x21, 0x7e) && c != Specification.DoubleQuote && c != Specification.Num && c != Specification.LessThan && c != Specification.GreaterThan && c != Specification.CurvedQuote)
                    buffer.Append(c);
                else
                    buffer.Append(Specification.Percent).Append(((Byte)c).ToString("X2"));

                index++;
            }

            _query = buffer.ToPool();

            if (onlyQuery)
                return true;

            return ParseFragment(input, index + 1);
        }

        Boolean ParseFragment(String input, Int32 index)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Specification.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c.IsUrlCodePoint())
                {
                    if (c.IsInRange(0x20, 0x7e))
                        buffer.Append(c);
                    else
                        buffer.Append(Specification.Percent).Append(((Byte)c).ToString("X2"));
                }

                index++;
            }

            _fragment = buffer.ToPool();
            return true;
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
            return new Location(url).IsRelative == false;
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

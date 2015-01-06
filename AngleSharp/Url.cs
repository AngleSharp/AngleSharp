namespace AngleSharp
{
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents an Url class according to RFC3986.
    /// This is the base for all internal Url manipulation.
    /// </summary>
    public class Url : IEquatable<Url>
    {
        #region Fields

        String _fragment;
        String _query;
        String _path;
        String _scheme;
        String _port;
        String _host;
        String _username;
        String _password;
        Boolean _relative;
        String _schemeData;
        Boolean _error;

        #endregion

        #region ctor

        private Url()
        {
            _relative = false;
            _scheme = String.Empty;
            _schemeData = String.Empty;
            _host = String.Empty;
            _port = String.Empty;
            _path = String.Empty;
        }

        private Url(String scheme, String host, String port)
            : this()
        {
            _scheme = scheme;
            _host = host;
            _port = port;
        }

        /// <summary>
        /// Creates a new Url from the given string.
        /// </summary>
        /// <param name="address">The address to represent.</param>
        public Url(String address)
            : this()
        {
            ParseUrl(address);
        }

        /// <summary>
        /// Creates a new absolute Url from the relative Url with the given base address.
        /// </summary>
        /// <param name="baseAddress">The base address to use.</param>
        /// <param name="relativeAddress">The relative address to represent.</param>
        public Url(Url baseAddress, String relativeAddress)
            : this()
        {
            ParseUrl(relativeAddress, baseAddress);
        }

        /// <summary>
        /// Creates a new Url by copying the other Url.
        /// </summary>
        /// <param name="address">The address to copy.</param>
        public Url(Url address)
        {
            _fragment = address._fragment;
            _query = address._query;
            _path = address._path;
            _scheme = address._scheme;
            _port = address._port;
            _host = address._host;
            _username = address._username;
            _password = address._password;
             _relative = address._relative;
            _schemeData = address._schemeData;;
        }

        /// <summary>
        /// Creates a new Url using the original string of the given Uri.
        /// </summary>
        /// <param name="address">The address to represent.</param>
        public Url(Uri address)
            : this(address.OriginalString)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the origin of the stored url.
        /// </summary>
        public Url Origin
        {
            get
            {
                if (_scheme.Equals(KnownProtocols.Blob))
                {
                    var url = new Url(_schemeData);

                    if (!url.IsInvalid)
                        return url.Origin;
                }
                else if (KnownProtocols.IsOriginable(_scheme))
                {
                    var port = String.IsNullOrEmpty(_port) ? DefaultPorts.GetDefaultPort(_scheme) : _port;
                    return new Url(_scheme, _host, port);
                }

                return null;
            }
        }

        /// <summary>
        /// Gets if the URL parsing resulted in an error.
        /// </summary>
        public Boolean IsInvalid
        {
            get { return _error; }
        }

        /// <summary>
        /// Gets if the stored url is relative.
        /// </summary>
        public Boolean IsRelative
        {
            get { return _relative && String.IsNullOrEmpty(_scheme); }
        }

        /// <summary>
        /// Gets if the stored url is absolute.
        /// </summary>
        public Boolean IsAbsolute
        {
            get { return !IsRelative; }
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
        /// Gets or sets the fragment.
        /// </summary>
        public String Fragment
        {
            get { return _fragment; }
            set { ParseFragment(value ?? String.Empty, 0); }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        public String Host
        {
            get { return HostName + (String.IsNullOrEmpty(_port) ? String.Empty : ":" + _port); }
            set { ParseHostName(value ?? String.Empty, 0, false, true); }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        public String HostName
        {
            get { return _host; }
            set { ParseHostName(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        public String Href
        {
            get { return Serialize(); }
            set { ParseUrl(value ?? String.Empty); }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "mypath".
        /// </summary>
        public String Path
        {
            get { return _path; }
            set { ParsePath(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800".
        /// </summary>
        public String Port
        {
            get { return _port; }
            set { ParsePort(value ?? String.Empty, 0, true); }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http".
        /// </summary>
        public String Scheme
        {
            get { return _scheme; }
            set { ParseScheme(value ?? String.Empty, true); }
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        public String Query
        {
            get { return _query; }
            set { ParseQuery(value ?? String.Empty, 0, true); }
        }

        #endregion

        #region Equality

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current url.</returns>
        public override Int32 GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>True if the object is equal to the current object, otherwise false.</returns>
        public override Boolean Equals(Object obj)
        {
            var url = obj as Url;

            if (url != null)
                return Equals(url);

            return false;
        }

        /// <summary>
        /// Determines whether the specified url is equal to the current object.
        /// </summary>
        /// <param name="other">The url to compare with the current one.</param>
        /// <returns>True if the given url is equal to the current url, otherwise false.</returns>
        public Boolean Equals(Url other)
        {
            return _fragment.Equals(other._fragment, StringComparison.Ordinal) &&
                _query.Equals(other._query, StringComparison.Ordinal) &&
                _path.Equals(other._path, StringComparison.Ordinal) &&
                _scheme.Equals(other._scheme, StringComparison.OrdinalIgnoreCase) &&
                _port.Equals(other._port, StringComparison.Ordinal) &&
                _host.Equals(other._host, StringComparison.OrdinalIgnoreCase) &&
                _username.Equals(other._username, StringComparison.Ordinal) &&
                _password.Equals(other._password, StringComparison.Ordinal) &&
                _schemeData.Equals(other._schemeData, StringComparison.Ordinal);
        }

        #endregion

        #region Conversion

        /// <summary>
        /// Converts the given Url to an Uri.
        /// </summary>
        /// <param name="value">The Url to convert.</param>
        /// <returns>The Uri instance.</returns>
        public static implicit operator Uri(Url value)
        {
            return new Uri(value.Serialize(), value.IsRelative ? UriKind.Relative : UriKind.Absolute);
        }

        #endregion

        #region Serialization

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        String Serialize()
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

        #region Parsing

        void ParseUrl(String input, Url baseUrl = null)
        {
            if (baseUrl != null)
            {
                _scheme = baseUrl._scheme;
                _host = baseUrl._host;
                _path = baseUrl._path;
                _port = baseUrl._port;
            }

            _error = ParseScheme(input.Trim());
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
                        _scheme = input.Substring(0, index).ToLowerInvariant();

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
                        _host = SanatizeHost(input.Substring(start, index - start));
                        
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
                return false;
            }

            return true;
        }

        static String SanatizeHost(String hostName)
        {
            if (hostName.Length > 1 && hostName[0] == Specification.SquareBracketOpen && hostName[hostName.Length - 1] == Specification.SquareBracketClose)
                return hostName;

            var chars = new Char[hostName.Length];
            var count = 0;

            for (var i = 0; i < hostName.Length; i++)
            {
                switch (hostName[i])
                {
                    // U+0000, U+0009, U+000A, U+000D, U+0020, "#", "%", "/", ":", "?", "@", "[", "\", and "]"
                    case Specification.Null:
                    case Specification.Tab:
                    case Specification.Space:
                    case Specification.LineFeed:
                    case Specification.CarriageReturn:
                    case Specification.Num:
                    case Specification.Percent:
                    case Specification.Solidus:
                    case Specification.Colon:
                    case Specification.QuestionMark:
                    case Specification.At:
                    case Specification.SquareBracketOpen:
                    case Specification.SquareBracketClose:
                    case Specification.ReverseSolidus:
                        break;
                    default:
                        chars[count++] = hostName[i];
                        break;
                }
            }

            return new String(chars, 0, count);
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
    }
}

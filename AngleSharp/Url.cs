namespace AngleSharp
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using AngleSharp.Extensions;
    using AngleSharp.Network;

    /// <summary>
    /// Represents an Url class according to RFC3986. This is the base for all
    /// internal Url manipulation.
    /// </summary>
    public class Url : IEquatable<Url>
    {
        #region Fields

        static readonly String currentDirectory = ".";
        static readonly String upperDirectory = "..";
        static readonly Url DefaultBase = new Url(String.Empty, String.Empty, String.Empty);

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

        private Url(String scheme, String host, String port)
        {
            _schemeData = String.Empty;
            _path = String.Empty;
            _scheme = scheme;
            _host = host;
            _port = port;
            _relative = KnownProtocols.IsRelative(_scheme);
        }

        /// <summary>
        /// Creates a new Url from the given string.
        /// </summary>
        /// <param name="address">The address to represent.</param>
        public Url(String address)
        {
            ParseUrl(address);
        }

        /// <summary>
        /// Creates a new absolute Url from the relative Url with the given
        /// base address.
        /// </summary>
        /// <param name="baseAddress">The base address to use.</param>
        /// <param name="relativeAddress">
        /// The relative address to represent.
        /// </param>
        public Url(Url baseAddress, String relativeAddress)
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

        #endregion

        #region Creators

        /// <summary>
        /// Creates an Url from an absolute url transported in a string.
        /// </summary>
        /// <param name="address">The address to use.</param>
        /// <returns>The new Url.</returns>
        public static Url Create(String address)
        {
            return new Url(address);
        }

        /// <summary>
        /// Creates an Url from an url transported in an Uri.
        /// </summary>
        /// <param name="uri">The url to use.</param>
        /// <returns>The new Url.</returns>
        public static Url Convert(Uri uri)
        {
            return new Url(uri.OriginalString);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the origin of the stored url.
        /// </summary>
        public String Origin
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
                    var output = Pool.NewStringBuilder();

                    if (!String.IsNullOrEmpty(_host))
                    {
                        if (!String.IsNullOrEmpty(_scheme))
                            output.Append(_scheme).Append(Symbols.Colon);

                        output.Append(Symbols.Solidus).Append(Symbols.Solidus).Append(_host);

                        if (!String.IsNullOrEmpty(_port))
                            output.Append(Symbols.Colon).Append(_port);
                    }

                    return output.ToPool();
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
        /// Gets the additional stored data of the URL. This is data that could
        /// not be assigned.
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
            set
            {
                if (value == null)
                    _fragment = null;
                else
                    ParseFragment(value, 0);
            }
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
        /// Determines whether the specified object is equal to the current
        /// object.
        /// </summary>
        /// <param name="obj">
        /// The object to compare with the current object.
        /// </param>
        /// <returns>
        /// True if the object is equal to the current object, otherwise false.
        /// </returns>
        public override Boolean Equals(Object obj)
        {
            var url = obj as Url;

            if (url != null)
                return Equals(url);

            return false;
        }

        /// <summary>
        /// Determines whether the specified url is equal to the current
        /// object.
        /// </summary>
        /// <param name="other">
        /// The url to compare with the current one.
        /// </param>
        /// <returns>
        /// True if the given url is equal to the current url, otherwise false.
        /// </returns>
        public Boolean Equals(Url other)
        {
            return Compare(_fragment, other._fragment) &&
                Compare(_query, other._query) &&
                _path.Equals(other._path, StringComparison.Ordinal) &&
                _scheme.Equals(other._scheme, StringComparison.OrdinalIgnoreCase) &&
                _port.Equals(other._port, StringComparison.Ordinal) &&
                _host.Equals(other._host, StringComparison.OrdinalIgnoreCase) &&
                Compare(_username, other._username) &&
                Compare(_password, other._password) &&
                _schemeData.Equals(other._schemeData, StringComparison.Ordinal);
        }

        static Boolean Compare(String a, String b)
        {
            return Object.ReferenceEquals(a, b) || (a != null && b != null && a.Equals(b, StringComparison.Ordinal));
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
        /// Returns a string that represents the current url.
        /// </summary>
        /// <returns>The currently stored url.</returns>
        public override String ToString()
        {
            return Serialize();
        }

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        String Serialize()
        {
            var output = Pool.NewStringBuilder();

            if (!String.IsNullOrEmpty(_scheme))
                output.Append(_scheme).Append(Symbols.Colon);

            if (_relative)
            {
                if (!String.IsNullOrEmpty(_host) || !String.IsNullOrEmpty(_scheme))
                {
                    output.Append(Symbols.Solidus).Append(Symbols.Solidus);

                    if (String.IsNullOrEmpty(_username) == false || _password != null)
                    {
                        output.Append(_username);

                        if (_password != null)
                            output.Append(Symbols.Colon).Append(_password);

                        output.Append(Symbols.At);
                    }

                    output.Append(_host);

                    if (!String.IsNullOrEmpty(_port))
                        output.Append(Symbols.Colon).Append(_port);

                    output.Append(Symbols.Solidus);
                }

                output.Append(_path);
            }
            else
                output.Append(_schemeData);

            if (_query != null)
                output.Append(Symbols.QuestionMark).Append(_query);

            if (_fragment != null)
                output.Append(Symbols.Num).Append(_fragment);

            return output.ToPool();
        }

        #endregion

        #region Parsing

        void ParseUrl(String input, Url baseUrl = null)
        {
            Reset(baseUrl ?? DefaultBase);
            _error = ParseScheme(input.Trim()) == false;
        }

        void Reset(Url baseUrl)
        {
            _schemeData = String.Empty;
            _scheme = baseUrl._scheme;
            _host = baseUrl._host;
            _path = baseUrl._path;
            _port = baseUrl._port;
            _relative = KnownProtocols.IsRelative(_scheme);
        }

        Boolean ParseScheme(String input, Boolean onlyScheme = false)
        {
            if (input.Length > 0 && input[0].IsLetter())
            {
                var index = 1;

                while (index < input.Length)
                {
                    var c = input[index];

                    if (c.IsAlphanumericAscii() || c == Symbols.Plus || c == Symbols.Minus || c == Symbols.Dot)
                        index++;
                    else if (c == Symbols.Colon)
                    {
                        var originalScheme = _scheme;
                        _scheme = input.Substring(0, index).ToLowerInvariant();

                        if (onlyScheme)
                            return true;

                        _relative = KnownProtocols.IsRelative(_scheme);

                        if (_scheme == KnownProtocols.File)
                        {
                            _host = String.Empty;
                            _port = String.Empty;
                            return RelativeState(input, index + 1);
                        }
                        else if (!_relative)
                        {
                            _host = String.Empty;
                            _port = String.Empty;
                            _path = String.Empty;
                            return ParseSchemeData(input, index + 1);
                        }
                        else if (originalScheme == _scheme)
                        {
                            c = input[++index];

                            if (c == Symbols.Solidus && index + 2 < input.Length && input[index + 1] == Symbols.Solidus)
                                return IgnoreSlashesState(input, index + 2);

                            return RelativeState(input, index);
                        }

                        if (input[++index] == Symbols.Solidus && ++index < input.Length && input[index] == Symbols.Solidus)
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

                if (c == Symbols.QuestionMark)
                {
                    _schemeData = buffer.ToPool();
                    return ParseQuery(input, index + 1);
                }
                else if (c == Symbols.Num)
                {
                    _schemeData = buffer.ToPool();
                    return ParseFragment(input, index + 1);
                }
                else if (c == Symbols.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c.IsInRange(0x20, 0x7e))
                    buffer.Append(c);
                else if (c != Symbols.Tab && c != Symbols.LineFeed && c != Symbols.CarriageReturn)
                    index += Utf8PercentEncode(buffer, input, index);

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
                case Symbols.QuestionMark:
                    return ParseQuery(input, index + 1);

                case Symbols.Num:
                    return ParseFragment(input, index + 1);

                case Symbols.Solidus:
                case Symbols.ReverseSolidus:
                    if (index == input.Length - 1)
                        return ParsePath(input, index);

                    var c = input[++index];

                    if (c == Symbols.Solidus || c == Symbols.ReverseSolidus)
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

                    return ParsePath(input, index - 1);
            }

            if (input[index].IsLetter() && _scheme == KnownProtocols.File && index + 1 < input.Length && (input[index + 1] == Symbols.Colon || input[index + 1] == Symbols.Solidus) &&
                (index + 2 >= input.Length || input[index + 2] == Symbols.Solidus || input[index + 2] == Symbols.ReverseSolidus || input[index + 2] == Symbols.Num || input[index + 2] == Symbols.QuestionMark))
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
                if (input[index] != Symbols.ReverseSolidus && input[index] != Symbols.Solidus)
                    return ParseAuthority(input, index);

                index++;
            }

            return false;
        }

        Boolean ParseAuthority(String input, Int32 index)
        {
            var start = index;
            var buffer = Pool.NewStringBuilder();
            var user = default(String);
            var pass = default(String);

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Symbols.At)
                {
                    if (user == null)
                        user = buffer.ToString();
                    else
                        pass = buffer.ToString();

                    _username = user;
                    _password = pass;
                    buffer.Append("%40");
                    start = index + 1;
                }
                else if (c == Symbols.Colon && user == null)
                {
                    user = buffer.ToString();
                    pass = String.Empty;
                    buffer.Clear();
                }
                else if (c == Symbols.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]).Append(input[index++]).Append(input[index]);
                }
                else if (c == Symbols.Tab || c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                {
                    // Parse Error
                }
                else if (c == Symbols.Solidus || c == Symbols.ReverseSolidus || c == Symbols.Num || c == Symbols.QuestionMark)
                {
                    break;
                }
                else if (c != Symbols.Colon && (c == Symbols.Num || c == Symbols.QuestionMark || c.IsNormalPathCharacter()))
                {
                    buffer.Append(c);
                }
                else
                {
                    index += Utf8PercentEncode(buffer, input, index);
                }

                index++;
            }

            buffer.ToPool();
            return ParseHostName(input, start);
        }

        Boolean ParseFileHost(String input, Int32 index)
        {
            var start = index;
            _path = String.Empty;

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Symbols.Solidus || c == Symbols.ReverseSolidus || c == Symbols.Num || c == Symbols.QuestionMark)
                    break;

                index++;
            }

            var length = index - start;

            if (length == 2 && input[index - 2].IsLetter() && (input[index - 1] == Symbols.Pipe || input[index - 1] == Symbols.Colon))
                return ParsePath(input, index - 2);
            else if (length != 0)
                _host = SanatizeHost(input, start, length);

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
                    case Symbols.SquareBracketClose:
                        inBracket = false;
                        break;

                    case Symbols.SquareBracketOpen:
                        inBracket = true;
                        break;

                    case Symbols.Colon:
                        if (inBracket)
                            break;

                        _host = SanatizeHost(input, start, index - start);

                        if (onlyHost)
                            return true;

                        return ParsePort(input, index + 1, onlyPort);

                    case Symbols.Solidus:
                    case Symbols.ReverseSolidus:
                    case Symbols.Num:
                    case Symbols.QuestionMark:
                        _host = SanatizeHost(input, start, index - start);
                        var error = String.IsNullOrEmpty(_host);

                        if (onlyHost)
                            return !error;

                        return ParsePath(input, index) && !error;
                }

                index++;
            }

            _host = SanatizeHost(input, start, index - start);

            if (!onlyHost)
            {
                _path = String.Empty;
                _query = null;
                _fragment = null;
            }

            return true;
        }

        Boolean ParsePort(String input, Int32 index, Boolean onlyPort = false)
        {
            var start = index;

            while (index < input.Length)
            {
                var c = input[index];

                if (c == Symbols.QuestionMark || c == Symbols.Solidus || c == Symbols.ReverseSolidus || c == Symbols.Num)
                    break;
                else if (c.IsDigit() || c == Symbols.Tab || c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                    index++;
                else
                    return false;
            }

            _port = SanatizePort(input, start, index - start);

            if (PortNumbers.GetDefaultPort(_scheme) == _port)
                _port = String.Empty;

            if (onlyPort)
                return true;

            _path = String.Empty;
            return ParsePath(input, index);
        }

        Boolean ParsePath(String input, Int32 index, Boolean onlyPath = false)
        {
            var init = index;

            if (index < input.Length && (input[index] == Symbols.Solidus || input[index] == Symbols.ReverseSolidus))
                index++;

            var paths = new List<String>();

            if (!onlyPath && !String.IsNullOrEmpty(_path) && index - init == 0)
            {
                var split = _path.Split(Symbols.Solidus);

                if (split.Length > 1)
                {
                    paths.AddRange(split);
                    paths.RemoveAt(split.Length - 1);
                }
            }

            var originalCount = paths.Count;
            var buffer = Pool.NewStringBuilder();

            while (index <= input.Length)
            {
                var c = index == input.Length ? Symbols.EndOfFile : input[index];
                var breakNow = !onlyPath && (c == Symbols.Num || c == Symbols.QuestionMark);

                if (c == Symbols.EndOfFile || c == Symbols.Solidus || c == Symbols.ReverseSolidus || breakNow)
                {
                    var path = buffer.ToString();
                    var close = false;
                    buffer.Clear();

                    if (path.Equals("%2e", StringComparison.OrdinalIgnoreCase))
                        path = currentDirectory;
                    else if (path.Equals(".%2e", StringComparison.OrdinalIgnoreCase) || path.Equals("%2e.", StringComparison.OrdinalIgnoreCase) || path.Equals("%2e%2e", StringComparison.OrdinalIgnoreCase))
                        path = upperDirectory;

                    if (path.Equals(upperDirectory))
                    {
                        if (paths.Count > 0)
                            paths.RemoveAt(paths.Count - 1);

                        close = true;
                    }
                    else if (!path.Equals(currentDirectory))
                    {
                        if (_scheme == KnownProtocols.File && paths.Count == originalCount && path.Length == 2 && path[0].IsLetter() && path[1] == Symbols.Pipe)
                        {
                            path = path.Replace(Symbols.Pipe, Symbols.Colon);
                            paths.Clear();
                        }

                        paths.Add(path);
                    }
                    else
                        close = true;

                    if (close && c != Symbols.Solidus && c != Symbols.ReverseSolidus)
                        paths.Add(String.Empty);

                    if (breakNow)
                        break;
                }
                else if (c == Symbols.Percent && index + 2 < input.Length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c == Symbols.Tab || c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                {
                    // Parse Error
                }
                else if (c.IsNormalPathCharacter())
                {
                    buffer.Append(c);
                }
                else
                {
                    index += Utf8PercentEncode(buffer, input, index);
                }

                index++;
            }

            buffer.ToPool();
            _path = String.Join("/", paths);

            if (index < input.Length)
            {
                if (input[index] == Symbols.QuestionMark)
                    return ParseQuery(input, index + 1);

                return ParseFragment(input, index + 1);
            }

            return true;
        }

        Boolean ParseQuery(String input, Int32 index, Boolean onlyQuery = false)
        {
            var buffer = Pool.NewStringBuilder();
            var fragment = false;

            while (index < input.Length)
            {
                var c = input[index];
                fragment = !onlyQuery && input[index] == Symbols.Num;

                if (fragment)
                    break;

                if (c.IsNormalQueryCharacter())
                    buffer.Append(c);
                else
                    buffer.Append(Symbols.Percent).Append(((Byte)c).ToString("X2"));

                index++;
            }

            _query = buffer.ToPool();
            return fragment ? ParseFragment(input, index + 1) : true;
        }

        Boolean ParseFragment(String input, Int32 index)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < input.Length)
            {
                var c = input[index];

                switch (c)
                {
                    case Symbols.EndOfFile:
                    case Symbols.Null:
                    case Symbols.Tab:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                        break;
                    default:
                        buffer.Append(c);
                        break;
                }

                index++;
            }

            _fragment = buffer.ToPool();
            return true;
        }

        #endregion

        #region Helpers

        static Int32 Utf8PercentEncode(StringBuilder buffer, String source, Int32 index)
        {
            var length = Char.IsSurrogatePair(source, index) ? 2 : 1;
            var bytes = TextEncoding.Utf8.GetBytes(source.Substring(index, length));

            for (var i = 0; i < bytes.Length; i++)
                buffer.Append(Symbols.Percent).Append(bytes[i].ToString("X2"));

            return length - 1;
        }

        static String SanatizeHost(String hostName, Int32 start, Int32 length)
        {
            if (length > 1 && hostName[start] == Symbols.SquareBracketOpen && hostName[start + length - 1] == Symbols.SquareBracketClose)
                return hostName.Substring(start, length);

            var chars = new Byte[4 * length];
            var count = 0;

            for (int i = start, n = start + length; i < n; i++)
            {
                switch (hostName[i])
                {
                    // U+0000, U+0009, U+000A, U+000D, U+0020, "#", "%", "/", ":", "?", "@", "[", "\", and "]"
                    case Symbols.Null:
                    case Symbols.Tab:
                    case Symbols.Space:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                    case Symbols.Num:
                    case Symbols.Solidus:
                    case Symbols.Colon:
                    case Symbols.QuestionMark:
                    case Symbols.At:
                    case Symbols.SquareBracketOpen:
                    case Symbols.SquareBracketClose:
                    case Symbols.ReverseSolidus:
                        break;
                    case Symbols.Dot:
                        chars[count++] = (Byte)hostName[i];
                        break;
                    case Symbols.Percent:
                        if (i + 2 < n && hostName[i + 1].IsHex() && hostName[i + 2].IsHex())
                        {
                            var weight = hostName[i + 1].FromHex() * 16 + hostName[i + 2].FromHex();
                            chars[count++] = (Byte)weight;
                            i += 2;
                        }
                        else
                        {
                            chars[count++] = (Byte)Symbols.Percent;
                        }

                        break;
                    default:
                        var chr = Symbols.Null;

                        if (Symbols.Punycode.TryGetValue(hostName[i], out chr))
                        {
                            chars[count++] = (Byte)chr;
                        }
                        else if (hostName[i].IsAlphanumericAscii() == false)
                        {
                            var l = i + 1 < n && Char.IsSurrogatePair(hostName, i) ? 2 : 1;

                            if (l == 1 && Char.IsLetterOrDigit(hostName[i]) == false)
                                break;

                            var bytes = TextEncoding.Utf8.GetBytes(hostName.Substring(i, l));

                            for (var j = 0; j < bytes.Length; j++)
                                chars[count++] = bytes[j];

                            i += (l - 1);
                        }
                        else
                        {
                            chars[count++] = (Byte)Char.ToLowerInvariant(hostName[i]);
                        }

                        break;
                }
            }

            //TODO finish with
            //https://url.spec.whatwg.org/#concept-host-parser
            //missing IPv6/4 parsing, Punycode [no normalization in WP app.]
            return TextEncoding.Utf8.GetString(chars, 0, count);
        }

        static String SanatizePort(String port, Int32 start, Int32 length)
        {
            var chars = new Char[length];
            var count = 0;

            for (int i = start, n = start + length; i < n; i++)
            {
                switch (port[i])
                {
                    case Symbols.Tab:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                        break;
                    default:
                        if (count == 1 && chars[0] == '0')
                            chars[0] = port[i];
                        else
                            chars[count++] = port[i];

                        break;
                }
            }

            return new String(chars, 0, count);
        }

        #endregion
    }
}

namespace AngleSharp
{
    using AngleSharp.Extensions;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Represents an Url class according to RFC3986. This is the base for all
    /// internal Url manipulation.
    /// </summary>
    public sealed class Url : IEquatable<Url>
    {
        #region Fields

        private static readonly String currentDirectory = ".";
        private static readonly String currentDirectoryAlternative = "%2e";
        private static readonly String upperDirectory = "..";
        private static readonly String[] upperDirectoryAlternatives = new[] { "%2e%2e", ".%2e", "%2e." };
        private static readonly Url DefaultBase = new Url(String.Empty, String.Empty, String.Empty);

        private String _fragment;
        private String _query;
        private String _path;
        private String _scheme;
        private String _port;
        private String _host;
        private String _username;
        private String _password;
        private Boolean _relative;
        private String _schemeData;
        private Boolean _error;

        #endregion

        #region ctor

        private Url(String scheme, String host, String port)
        {
            _schemeData = String.Empty;
            _path = String.Empty;
            _scheme = scheme;
            _host = host;
            _port = port;
            _relative = ProtocolNames.IsRelative(_scheme);
        }

        /// <summary>
        /// Creates a new Url from the given string.
        /// </summary>
        /// <param name="address">The address to represent.</param>
        public Url(String address)
        {
            _error = ParseUrl(address);
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
            _error = ParseUrl(relativeAddress, baseAddress);
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
                if (_scheme.Is(ProtocolNames.Blob))
                {
                    var url = new Url(_schemeData);

                    if (!url.IsInvalid)
                    {
                        return url.Origin;
                    }
                }
                else if (ProtocolNames.IsOriginable(_scheme))
                {
                    var output = Pool.NewStringBuilder();

                    if (!String.IsNullOrEmpty(_host))
                    {
                        if (!String.IsNullOrEmpty(_scheme))
                        {
                            output.Append(_scheme).Append(Symbols.Colon);
                        }

                        output.Append(Symbols.Solidus).Append(Symbols.Solidus).Append(_host);

                        if (!String.IsNullOrEmpty(_port))
                        {
                            output.Append(Symbols.Colon).Append(_port);
                        }
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
                {
                    _fragment = null;
                }
                else
                {
                    ParseFragment(value, 0, value.Length);
                }
            }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        public String Host
        {
            get { return HostName + (String.IsNullOrEmpty(_port) ? String.Empty : ":" + _port); }
            set
            {
                var input = value ?? String.Empty;
                ParseHostName(input, 0, input.Length, false, true);
            }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        public String HostName
        {
            get { return _host; }
            set
            {
                var input = value ?? String.Empty;
                ParseHostName(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full path.
        /// </summary>
        public String Href
        {
            get { return Serialize(); }
            set { _error = ParseUrl(value ?? String.Empty); }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "mypath".
        /// </summary>
        public String Path
        {
            get { return _path; }
            set
            {
                var input = value ?? String.Empty;
                ParsePath(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800".
        /// </summary>
        public String Port
        {
            get { return _port; }
            set
            {
                var input = value ?? String.Empty;
                ParsePort(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http".
        /// </summary>
        public String Scheme
        {
            get { return _scheme; }
            set
            {
                var input = value ?? String.Empty;
                ParseScheme(input, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the query.
        /// </summary>
        public String Query
        {
            get { return _query; }
            set
            {
                var input = value ?? String.Empty;
                ParseQuery(input, 0, input.Length, true);
            }
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
            return url != null ? Equals(url) : false;
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
            return _fragment.Is(other._fragment) && _query.Is(other._query) &&
                _path.Is(other._path) && _scheme.Isi(other._scheme) &&
                _port.Is(other._port) && _host.Isi(other._host) &&
                _username.Is(other._username) && _password.Is(other._password) &&
                _schemeData.Is(other._schemeData);
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
        private String Serialize()
        {
            var output = Pool.NewStringBuilder();

            if (!String.IsNullOrEmpty(_scheme))
            {
                output.Append(_scheme).Append(Symbols.Colon);
            }

            if (_relative)
            {
                if (!String.IsNullOrEmpty(_host) || !String.IsNullOrEmpty(_scheme))
                {
                    output.Append(Symbols.Solidus).Append(Symbols.Solidus);

                    if (!String.IsNullOrEmpty(_username) || _password != null)
                    {
                        output.Append(_username);

                        if (_password != null)
                        {
                            output.Append(Symbols.Colon).Append(_password);
                        }

                        output.Append(Symbols.At);
                    }

                    output.Append(_host);

                    if (!String.IsNullOrEmpty(_port))
                    {
                        output.Append(Symbols.Colon).Append(_port);
                    }

                    output.Append(Symbols.Solidus);
                }

                output.Append(_path);
            }
            else
            {
                output.Append(_schemeData);
            }

            if (_query != null)
            {
                output.Append(Symbols.QuestionMark).Append(_query);
            }

            if (_fragment != null)
            {
                output.Append(Symbols.Num).Append(_fragment);
            }

            return output.ToPool();
        }

        #endregion

        #region Parsing

        private Boolean ParseUrl(String input, Url baseUrl = null)
        {
            Reset(baseUrl ?? DefaultBase);
            var normalizedInput = input.Trim();
            var length = normalizedInput.Length;
            return !ParseScheme(normalizedInput, length);
        }

        private void Reset(Url baseUrl)
        {
            _schemeData = String.Empty;
            _scheme = baseUrl._scheme;
            _host = baseUrl._host;
            _path = baseUrl._path;
            _port = baseUrl._port;
            _relative = ProtocolNames.IsRelative(_scheme);
        }

        private Boolean ParseScheme(String input, Int32 length, Boolean onlyScheme = false)
        {

            if (length > 0 && input[0].IsLetter())
            {
                var index = 1;

                while (index < length)
                {
                    var c = input[index];

                    if (c.IsAlphanumericAscii() || c == Symbols.Plus || c == Symbols.Minus || c == Symbols.Dot)
                    {
                        index++;
                    }
                    else if (c == Symbols.Colon)
                    {
                        var originalScheme = _scheme;
                        _scheme = input.Substring(0, index).ToLowerInvariant();

                        if (!onlyScheme)
                        {
                            _relative = ProtocolNames.IsRelative(_scheme);

                            if (_scheme.Is(ProtocolNames.File))
                            {
                                _host = String.Empty;
                                _port = String.Empty;
                                return RelativeState(input, index + 1, length);
                            }
                            else if (!_relative)
                            {
                                _host = String.Empty;
                                _port = String.Empty;
                                _path = String.Empty;
                                return ParseSchemeData(input, index + 1, length);
                            }
                            else if (_scheme.Is(originalScheme))
                            {
                                if (++index < length)
                                {
                                    c = input[index];

                                    if (c == Symbols.Solidus && index + 2 < length && input[index + 1] == Symbols.Solidus)
                                    {
                                        return IgnoreSlashesState(input, index + 2, length);
                                    }

                                    return RelativeState(input, index, length);
                                }

                                return false;
                            }
                            else if (index + 1 < length && input[++index] == Symbols.Solidus && ++index < length && input[index] == Symbols.Solidus)
                            {
                                index++;
                            }

                            return IgnoreSlashesState(input, index, length);
                        }

                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            
            return !onlyScheme && RelativeState(input, 0, length);
        }

        private Boolean ParseSchemeData(String input, Int32 index, Int32 length)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < length)
            {
                var c = input[index];

                if (c == Symbols.QuestionMark)
                {
                    _schemeData = buffer.ToPool();
                    return ParseQuery(input, index + 1, length);
                }
                else if (c == Symbols.Num)
                {
                    _schemeData = buffer.ToPool();
                    return ParseFragment(input, index + 1, length);
                }
                else if (c == Symbols.Percent && index + 2 < length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]);
                    buffer.Append(input[index++]);
                    buffer.Append(input[index]);
                }
                else if (c.IsInRange(0x20, 0x7e))
                {
                    buffer.Append(c);
                }
                else if (c != Symbols.Tab && c != Symbols.LineFeed && c != Symbols.CarriageReturn)
                {
                    index += Utf8PercentEncode(buffer, input, index);
                }

                index++;
            }

            _schemeData = buffer.ToPool();
            return true;
        }

        private Boolean RelativeState(String input, Int32 index, Int32 length)
        {
            _relative = true;

            if (index != length)
            {
                switch (input[index])
                {
                    case Symbols.QuestionMark:
                        return ParseQuery(input, index + 1, length);

                    case Symbols.Num:
                        return ParseFragment(input, index + 1, length);

                    case Symbols.Solidus:
                    case Symbols.ReverseSolidus:
                        if (index != length - 1)
                        {
                            var c = input[++index];

                            if (c.IsOneOf(Symbols.Solidus, Symbols.ReverseSolidus))
                            {
                                if (_scheme.Is(ProtocolNames.File))
                                {
                                    return ParseFileHost(input, index + 1, length);
                                }

                                return IgnoreSlashesState(input, index + 1, length);
                            }
                            else if (_scheme.Is(ProtocolNames.File))
                            {
                                _host = String.Empty;
                                _port = String.Empty;
                            }

                            return ParsePath(input, index - 1, length);
                        }

                        return ParsePath(input, index, length);
                }

                if (input[index].IsLetter() && _scheme.Is(ProtocolNames.File) && index + 1 < length &&
                   (input[index + 1].IsOneOf(Symbols.Colon, Symbols.Solidus)) &&
                   (index + 2 == length || input[index + 2].IsOneOf(Symbols.Solidus, Symbols.ReverseSolidus, Symbols.Num, Symbols.QuestionMark)))
                {
                    _host = String.Empty;
                    _path = String.Empty;
                    _port = String.Empty;
                }

                return ParsePath(input, index, length);
            }

            return true;
        }

        private Boolean IgnoreSlashesState(String input, Int32 index, Int32 length)
        {
            while (index < length)
            {
                if (!input[index].IsOneOf(Symbols.ReverseSolidus, Symbols.Solidus))
                {
                    return ParseAuthority(input, index, length);
                }

                index++;
            }

            return false;
        }

        private Boolean ParseAuthority(String input, Int32 index, Int32 length)
        {
            var start = index;
            var buffer = Pool.NewStringBuilder();
            var user = default(String);
            var pass = default(String);

            while (index < length)
            {
                var c = input[index];

                if (c == Symbols.At)
                {
                    if (user == null)
                    {
                        user = buffer.ToString();
                    }
                    else
                    {
                        pass = buffer.ToString();
                    }

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
                else if (c == Symbols.Percent && index + 2 < length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]).Append(input[index++]).Append(input[index]);
                }
                else if (c.IsOneOf(Symbols.Tab, Symbols.LineFeed, Symbols.CarriageReturn))
                {
                    // Parse Error
                }
                else if (c.IsOneOf(Symbols.Solidus, Symbols.ReverseSolidus, Symbols.Num, Symbols.QuestionMark))
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
            return ParseHostName(input, start, length);
        }

        private Boolean ParseFileHost(String input, Int32 index, Int32 length)
        {
            var start = index;
            _path = String.Empty;

            while (index < length)
            {
                var c = input[index];

                if (c == Symbols.Solidus || c == Symbols.ReverseSolidus || c == Symbols.Num || c == Symbols.QuestionMark)
                {
                    break;
                }

                index++;
            }

            var span = index - start;

            if (span == 2 && input[start].IsLetter() && input[start + 1].IsOneOf(Symbols.Pipe, Symbols.Colon))
            {
                return ParsePath(input, index - 2, length);
            }
            else if (span != 0)
            {
                _host = SanatizeHost(input, start, span);
            }

            return ParsePath(input, index, length);
        }

        private Boolean ParseHostName(String input, Int32 index, Int32 length, Boolean onlyHost = false, Boolean onlyPort = false)
        {
            var inBracket = false;
            var start = index;

            while (index < length)
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

                        if (!onlyHost)
                        {
                            return ParsePort(input, index + 1, length, onlyPort);
                        }

                        return true;

                    case Symbols.Solidus:
                    case Symbols.ReverseSolidus:
                    case Symbols.Num:
                    case Symbols.QuestionMark:
                        _host = SanatizeHost(input, start, index - start);
                        var error = String.IsNullOrEmpty(_host);

                        if (!onlyHost)
                        {
                            _port = String.Empty;
                            return ParsePath(input, index, length) && !error;
                        }

                        return !error;
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

        private Boolean ParsePort(String input, Int32 index, Int32 length, Boolean onlyPort = false)
        {
            var start = index;

            while (index < length)
            {
                var c = input[index];

                if (c == Symbols.QuestionMark || c == Symbols.Solidus || c == Symbols.ReverseSolidus || c == Symbols.Num)
                {
                    break;
                }
                else if (c.IsDigit() || c == Symbols.Tab || c == Symbols.LineFeed || c == Symbols.CarriageReturn)
                {
                    index++;
                }
                else
                {
                    return false;
                }
            }

            _port = SanatizePort(input, start, index - start);

            if (PortNumbers.GetDefaultPort(_scheme) == _port)
            {
                _port = String.Empty;
            }

            if (!onlyPort)
            {
                _path = String.Empty;
                return ParsePath(input, index, length);
            }

            return true;
        }

        private Boolean ParsePath(String input, Int32 index, Int32 length, Boolean onlyPath = false)
        {
            var init = index;

            if (index < length && (input[index] == Symbols.Solidus || input[index] == Symbols.ReverseSolidus))
            {
                index++;
            }

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

            while (index <= length)
            {
                var c = index == length ? Symbols.EndOfFile : input[index];
                var breakNow = !onlyPath && (c == Symbols.Num || c == Symbols.QuestionMark);

                if (c == Symbols.EndOfFile || c == Symbols.Solidus || c == Symbols.ReverseSolidus || breakNow)
                {
                    var path = buffer.ToString();
                    var close = false;
                    buffer.Clear();

                    if (path.Isi(currentDirectoryAlternative))
                    {
                        path = currentDirectory;
                    }
                    else if (path.Isi(upperDirectoryAlternatives[0]) || 
                             path.Isi(upperDirectoryAlternatives[1]) || 
                             path.Isi(upperDirectoryAlternatives[2]))
                    {
                        path = upperDirectory;
                    }

                    if (path.Is(upperDirectory))
                    {
                        if (paths.Count > 0)
                        {
                            paths.RemoveAt(paths.Count - 1);
                        }

                        close = true;
                    }
                    else if (!path.Is(currentDirectory))
                    {
                        if (_scheme.Is(ProtocolNames.File) && 
                            paths.Count == originalCount && 
                            path.Length == 2 && 
                            path[0].IsLetter() && 
                            path[1] == Symbols.Pipe)
                        {
                            path = path.Replace(Symbols.Pipe, Symbols.Colon);
                            paths.Clear();
                        }

                        paths.Add(path);
                    }
                    else
                    {
                        close = true;
                    }

                    if (close && c != Symbols.Solidus && c != Symbols.ReverseSolidus)
                    {
                        paths.Add(String.Empty);
                    }

                    if (breakNow)
                    {
                        break;
                    }
                }
                else if (c == Symbols.Percent && 
                         index + 2 < length && 
                         input[index + 1].IsHex() && 
                         input[index + 2].IsHex())
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

            if (index < length)
            {
                if (input[index] == Symbols.QuestionMark)
                {
                    return ParseQuery(input, index + 1, length);
                }

                return ParseFragment(input, index + 1, length);
            }

            return true;
        }

        private Boolean ParseQuery(String input, Int32 index, Int32 length, Boolean onlyQuery = false)
        {
            var buffer = Pool.NewStringBuilder();
            var fragment = false;

            while (index < length)
            {
                var c = input[index];
                fragment = !onlyQuery && input[index] == Symbols.Num;

                if (fragment)
                {
                    break;
                }

                if (c.IsNormalQueryCharacter())
                {
                    buffer.Append(c);
                }
                else
                {
                    index += Utf8PercentEncode(buffer, input, index);
                }

                index++;
            }

            _query = buffer.ToPool();
            return fragment ? ParseFragment(input, index + 1, length) : true;
        }

        private Boolean ParseFragment(String input, Int32 index, Int32 length)
        {
            var buffer = Pool.NewStringBuilder();

            while (index < length)
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

        private static Int32 Utf8PercentEncode(StringBuilder buffer, String source, Int32 index)
        {
            var length = Char.IsSurrogatePair(source, index) ? 2 : 1;
            var bytes = TextEncoding.Utf8.GetBytes(source.Substring(index, length));

            for (var i = 0; i < bytes.Length; i++)
            {
                buffer.Append(Symbols.Percent).Append(bytes[i].ToString("X2"));
            }

            return length - 1;
        }

        private static String SanatizeHost(String hostName, Int32 start, Int32 length)
        {
            if (length > 1 && hostName[start] == Symbols.SquareBracketOpen && hostName[start + length - 1] == Symbols.SquareBracketClose)
            {
                return hostName.Substring(start, length);
            }

            var chars = new Byte[4 * length];
            var count = 0;
            var n = start + length;

            for (var i = start; i < n; i++)
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

                            if (l == 1 && hostName[i] != Symbols.Minus && !Char.IsLetterOrDigit(hostName[i]))
                            {
                                break;
                            }

                            var bytes = TextEncoding.Utf8.GetBytes(hostName.Substring(i, l));

                            for (var j = 0; j < bytes.Length; j++)
                            {
                                chars[count++] = bytes[j];
                            }

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

        private static String SanatizePort(String port, Int32 start, Int32 length)
        {
            var chars = new Char[length];
            var count = 0;
            var n = start + length;

            for (var i = start; i < n; i++)
            {
                switch (port[i])
                {
                    case Symbols.Tab:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                        break;
                    default:
                        if (count == 1 && chars[0] == '0')
                        {
                            chars[0] = port[i];
                        }
                        else
                        {
                            chars[count++] = port[i];
                        }

                        break;
                }
            }

            return new String(chars, 0, count);
        }

        #endregion
    }
}

namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Globalization;
    using System.Net;
    using System.Net.Sockets;
    using System.Text;
    using Common;

    /// <summary>
    /// Represents an Url class according to RFC3986. This is the base for all
    /// internal Url manipulation.
    /// Specification for the API used from https://url.spec.whatwg.org/#api.
    /// </summary>
    [DomName("URL")]
    [DomExposed("Window")]
    [DomExposed("Worker")]
    public sealed class Url : IEquatable<Url>
    {
        #region Fields

        private static readonly String CurrentDirectory = ".";
        private static readonly String CurrentDirectoryAlternative = "%2e";
        private static readonly String UpperDirectory = "..";
        private static readonly String[] UpperDirectoryAlternatives = new[] { "%2e%2e", ".%2e", "%2e." };
        private static readonly Url DefaultBase = new(String.Empty, String.Empty, String.Empty);
        private static readonly Char[] C0ControlAndSpace =
            "\u0000\u0001\u0002\u0003\u0004\u0005\u0006\u0007\u0008\u0009\u000A\u000B\u000C\u000D\u000E\u000F\u0010\u0011\u0012\u0013\u0014\u0015\u0016\u0017\u0018\u0019\u001A\u001B\u001C\u001D\u001E\u001F\u0020".ToCharArray();

        // Remark: `UseStd3AsciiRules = false` is against spec
        // https://anglesharp.github.io/Specification-Url/#concept-domain-to-ascii
        // > UseSTD3ASCIIRules set to beStrict
        // But if UseStd3AsciiRules it set to true, _ (underscore) will be considered invalid in host name
        // Set to false here to do loose validation
        private static readonly IdnMapping DefaultIdnMapping = new() { AllowUnassigned = false, UseStd3AsciiRules = false };

        private String? _fragment;
        private String? _query;
        private String _path;
        private String _scheme;
        private String _port;
        private String _host;
        private String? _username;
        private String? _password;
        private Boolean _relative;
        private Boolean _hasPath;
        private String _schemeData;
        private UrlSearchParams? _params;
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

#nullable disable

        /// <summary>
        /// Creates a new Url from the given string.
        /// </summary>
        /// <param name="url">The address to represent.</param>
        /// <param name="baseAddress">The base address, if any.</param>
        [DomConstructor]
        public Url(String url, String baseAddress = null)
        {
            if (baseAddress is not null)
            {
                var baseUrl = new Url(baseAddress);
                _error = ParseUrl(url, baseUrl);
            }
            else
            {
                _error = ParseUrl(url);
            }
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

#nullable enable

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
            _hasPath = address._hasPath;
            _schemeData = address._schemeData;
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
        [DomName("origin")]
        public String? Origin
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
                    var output = StringBuilderPool.Obtain();

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
        public Boolean IsInvalid => _error;

        /// <summary>
        /// Gets if the stored url is relative.
        /// </summary>
        public Boolean IsRelative => _relative && String.IsNullOrEmpty(_scheme);

        /// <summary>
        /// Gets if the stored url is absolute.
        /// </summary>
        public Boolean IsAbsolute => !IsRelative;

        /// <summary>
        /// Gets or sets the username for authorization.
        /// </summary>
        [DomName("username")]
        public String? UserName
        {
            get => _username ?? String.Empty;
            set => _username = value;
        }

        /// <summary>
        /// Gets or sets the password for authorization.
        /// </summary>
        [DomName("password")]
        public String? Password
        {
            get => _password ?? String.Empty;
            set => _password = value;
        }

        /// <summary>
        /// Gets the additional stored data of the URL. This is data that could
        /// not be assigned.
        /// </summary>
        public String Data => _schemeData;

        /// <summary>
        /// Gets or sets the fragment, e.g., "first-section".
        /// </summary>
        public String? Fragment
        {
            get => _fragment;
            set
            {
                if (value is null)
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
        /// Gets or sets the hash, e.g., "#first-section".
        /// </summary>
        [DomName("hash")]
        public String Hash
        {
            get => String.IsNullOrEmpty(_fragment) ? String.Empty : $"#{_fragment}";
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Fragment = null;
                }
                else if (value[0] is Symbols.Num)
                {
                    Fragment = value.Substring(1);
                }
                else
                {
                    Fragment = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the host, e.g. "localhost:8800" or "www.w3.org".
        /// </summary>
        [DomName("host")]
        public String Host
        {
            get => HostName + (String.IsNullOrEmpty(_port) ? String.Empty : ":" + _port);
            set
            {
                var input = value ?? String.Empty;
                ParseHostName(input, 0, input.Length, false, true);
            }
        }

        /// <summary>
        /// Gets or sets the host name, e.g. "localhost" or "www.w3.org".
        /// </summary>
        [DomName("hostname")]
        public String HostName
        {
            get => _host;
            set
            {
                var input = value ?? String.Empty;
                ParseHostName(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the hyper reference, i.e. the full URL.
        /// </summary>
        [DomName("href")]
        public String Href
        {
            get => Serialize();
            set => _error = ParseUrl(value ?? String.Empty, this);
        }

        /// <summary>
        /// Gets or sets the path, e.g. "mypath".
        /// </summary>
        public String Path
        {
            get => _path;
            set
            {
                var input = value ?? String.Empty;
                ParsePath(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the pathname, e.g. "/mypath".
        /// </summary>
        [DomName("pathname")]
        public String PathName
        {
            get
            {
                if (!_relative)
                {
                    return _schemeData;
                }

                if (!_hasPath && !ProtocolNames.IsRelative(_scheme) && !String.IsNullOrEmpty(_scheme))
                {
                    return String.Empty;
                }

                return $"/{_path}";
            }
            set => Path = value;
        }

        /// <summary>
        /// Gets or sets the port, e.g. "8800".
        /// </summary>
        [DomName("port")]
        public String Port
        {
            get => _port;
            set
            {
                var input = value ?? String.Empty;
                ParsePort(input, 0, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the scheme, e.g. "http".
        /// </summary>
        public String Scheme
        {
            get => _scheme;
            set
            {
                var input = value ?? String.Empty;
                ParseScheme(input, input.Length, true);
            }
        }

        /// <summary>
        /// Gets or sets the protocol, e.g. "http:".
        /// </summary>
        [DomName("protocol")]
        public String Protocol
        {
            get => $"{_scheme}:";
            set => Scheme = value;
        }

        /// <summary>
        /// Gets or sets the query part, e.g., "foo=bar".
        /// </summary>
        public String? Query
        {
            get => _query;
            set
            {
                if (value == null)
                {
                    _query = null;
                    _params?.Reset();
                }
                else
                {
                    ParseQuery(value, 0, value.Length, true, false);
                }
            }
        }

        /// <summary>
        /// Gets or sets the search part, e.g., "?foo=bar".
        /// </summary>
        [DomName("search")]
        public String Search
        {
            get => String.IsNullOrEmpty(_query) ? String.Empty : $"?{_query}";
            set
            {
                if (String.IsNullOrEmpty(value))
                {
                    Query = null;
                }
                else if (value[0] is Symbols.QuestionMark)
                {
                    Query = value.Substring(1);
                }
                else
                {
                    Query = value;
                }
            }
        }

        /// <summary>
        /// Obtains an advanced view on the provided query parameter.
        /// </summary>
        [DomName("searchParams")]
        public UrlSearchParams SearchParams => _params ??= new UrlSearchParams(this);

        #endregion

        #region Equality

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current url.</returns>
        public override Int32 GetHashCode()
        {
            unchecked
            {
                var hashCode = _fragment != null ? StringComparer.Ordinal.GetHashCode(_fragment) : 0;
                hashCode = (hashCode * 397) ^ (_query != null ? StringComparer.Ordinal.GetHashCode(_query) : 0);
                hashCode = (hashCode * 397) ^ (_path != null ? StringComparer.Ordinal.GetHashCode(_path) : 0);
                hashCode = (hashCode * 397) ^ (_scheme != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(_scheme) : 0);
                hashCode = (hashCode * 397) ^ (_port != null ? StringComparer.Ordinal.GetHashCode(_port) : 0);
                hashCode = (hashCode * 397) ^ (_host != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(_host) : 0);
                hashCode = (hashCode * 397) ^ (_username != null ? StringComparer.Ordinal.GetHashCode(_username) : 0);
                hashCode = (hashCode * 397) ^ (_password != null ? StringComparer.Ordinal.GetHashCode(_password) : 0);
                hashCode = (hashCode * 397) ^ (_schemeData != null ? StringComparer.Ordinal.GetHashCode(_schemeData) : 0);
                return hashCode;
            }
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
        public override Boolean Equals(Object? obj)
        {
            return ReferenceEquals(this, obj) || obj is Url other && Equals(other);
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
        public Boolean Equals(Url? other)
        {
            return other != null && _fragment.Is(other._fragment) && _query.Is(other._query) &&
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
        /// Serializes the URL string to a JSON compatible string representation.
        /// </summary>
        /// <returns>The currently stored url.</returns>
        [DomName("toJSON")]
        public String ToJson() => Serialize();

        /// <summary>
        /// Returns a string that represents the current url.
        /// </summary>
        /// <returns>The currently stored url.</returns>
        public override String ToString() => Serialize();

        /// <summary>
        /// Returns the string representation of the current location.
        /// </summary>
        /// <returns>The string that equals the hyper reference.</returns>
        private String Serialize()
        {
            var output = StringBuilderPool.Obtain();

            if (!String.IsNullOrEmpty(_scheme))
            {
                output.Append(_scheme).Append(Symbols.Colon);
            }

            if (_relative)
            {
                if (!String.IsNullOrEmpty(_host) || !String.IsNullOrEmpty(_scheme))
                {
                    output.Append(Symbols.Solidus).Append(Symbols.Solidus);

                    if (!String.IsNullOrEmpty(_username) || !String.IsNullOrEmpty(_password))
                    {
                        output.Append(_username);

                        if (!String.IsNullOrEmpty(_password))
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

                    if (_hasPath || ProtocolNames.IsRelative(_scheme) || String.IsNullOrEmpty(_scheme))
                    {
                        output.Append(Symbols.Solidus);
                    }
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

        private Boolean ParseUrl(String input, Url? baseUrl = null)
        {
            Reset(baseUrl ?? DefaultBase);
            var normalizedInput = NormalizeInput(input);
            var length = normalizedInput.Length;
            return !ParseScheme(normalizedInput, length);
        }

        private void Reset(Url baseUrl)
        {
            _schemeData = String.Empty;
            _scheme = baseUrl._scheme;
            _host = baseUrl._host;
            _path = baseUrl._path;
            _query = baseUrl._query;
            _port = baseUrl._port;
            _username = baseUrl._username;
            _password = baseUrl._password;
            _hasPath = baseUrl._hasPath;
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
                                _query = null;
                                return RelativeState(input, index + 1, length);
                            }
                            else if (!_relative)
                            {
                                _host = String.Empty;
                                _port = String.Empty;
                                _path = String.Empty;
                                _hasPath = false;
                                _query = null;

                                var afterColon = index + 1;

                                if (afterColon + 1 < length &&
                                    input[afterColon] == Symbols.Solidus &&
                                    input[afterColon + 1] == Symbols.Solidus)
                                {
                                    _relative = true;
                                    return ParseAuthority(input, afterColon + 2, length);
                                }

                                return ParseSchemeData(input, afterColon, length);
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
            var buffer = StringBuilderPool.Obtain();

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

                            if (c is Symbols.Solidus or Symbols.ReverseSolidus)
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
                   (input[index + 1] is Symbols.Colon or Symbols.Solidus) &&
                   (index + 2 == length || input[index + 2] is Symbols.Solidus or Symbols.ReverseSolidus or Symbols.Num or Symbols.QuestionMark))
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
                if (!(input[index] is Symbols.ReverseSolidus or Symbols.Solidus))
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
            var buffer = StringBuilderPool.Obtain();
            var user = default(String);
            var pass = default(String);
            _username = null;
            _password = null;

            while (index < length)
            {
                var c = input[index];

                if (c == Symbols.At)
                {
                    if (user is null)
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
                else if (c == Symbols.Colon && user is null)
                {
                    user = buffer.ToString();
                    pass = String.Empty;
                    buffer.Clear();
                }
                else if (c == Symbols.Percent && index + 2 < length && input[index + 1].IsHex() && input[index + 2].IsHex())
                {
                    buffer.Append(input[index++]).Append(input[index++]).Append(input[index]);
                }
                else if (c is Symbols.Solidus or Symbols.ReverseSolidus or Symbols.Num or Symbols.QuestionMark)
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

            buffer.ReturnToPool();
            return ParseHostName(input, start, length);
        }

        private Boolean ParseFileHost(String input, Int32 index, Int32 length)
        {
            var start = index;
            _path = String.Empty;
            _username = null;
            _password = null;

            while (index < length)
            {
                var c = input[index];

                if (c is Symbols.Solidus or Symbols.ReverseSolidus or Symbols.Num or Symbols.QuestionMark)
                {
                    break;
                }

                index++;
            }

            var span = index - start;

            if (span == 2 && input[start].IsLetter() && input[start + 1] is Symbols.Pipe or Symbols.Colon)
            {
                return ParsePath(input, index - 2, length);
            }
            else if (span != 0)
            {
                if (!TrySanatizeHost(input, start, span, out _host))
                {
                    return false;
                }
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
                        {
                            break;
                        }

                        if (!TrySanatizeHost(input, start, index - start, out _host))
                        {
                            return false;
                        }

                        if (!onlyHost)
                        {
                            return ParsePort(input, index + 1, length, onlyPort);
                        }

                        return true;

                    case Symbols.Solidus:
                    case Symbols.ReverseSolidus:
                    case Symbols.Num:
                    case Symbols.QuestionMark:
                        if (!TrySanatizeHost(input, start, index - start, out _host))
                        {
                            return false;
                        }

                        var error = String.IsNullOrEmpty(_host) && ProtocolNames.IsRelative(_scheme);

                        if (!onlyHost)
                        {
                            _port = String.Empty;
                            return ParsePath(input, index, length) && !error;
                        }

                        return !error;
                }

                index++;
            }

            if (!TrySanatizeHost(input, start, index - start, out _host))
            {
                return false;
            }

            if (!onlyHost)
            {
                _path = String.Empty;
                _port = String.Empty;
                _query = null;
                _fragment = null;
                _params?.Reset();
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
                else if (c.IsDigit())
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
            _hasPath = true;
            var init = index;

            if (index < length && (input[index] == Symbols.Solidus || input[index] == Symbols.ReverseSolidus))
            {
                index++;
            }

            var hasExistingPath = !onlyPath && !String.IsNullOrEmpty(_path) && index - init == 0;
            var segmentCount = 0;
            var originalCount = 0;
            var output = StringBuilderPool.Obtain();

            if (hasExistingPath)
            {
                var lastSlash = _path.LastIndexOf(Symbols.Solidus);

                if (lastSlash >= 0)
                {
                    output.Append(_path, 0, lastSlash);
                    segmentCount = CountChar(output, Symbols.Solidus) + 1;
                    originalCount = segmentCount;
                }
            }

            var buffer = StringBuilderPool.Obtain();

            while (index <= length)
            {
                var c = index == length ? Symbols.EndOfFile : input[index];
                var breakNow = !onlyPath && (c == Symbols.Num || c == Symbols.QuestionMark);

                if (c == Symbols.EndOfFile || c == Symbols.Solidus || c == Symbols.ReverseSolidus || breakNow)
                {
                    var path = buffer.ToString();
                    var close = false;
                    buffer.Clear();

                    if (path.Isi(CurrentDirectoryAlternative))
                    {
                        path = CurrentDirectory;
                    }
                    else if (path.Isi(UpperDirectoryAlternatives[0]) ||
                             path.Isi(UpperDirectoryAlternatives[1]) ||
                             path.Isi(UpperDirectoryAlternatives[2]))
                    {
                        path = UpperDirectory;
                    }

                    if (path.Is(UpperDirectory))
                    {
                        if (segmentCount > 0)
                        {
                            // Remove last segment from output
                            var lastSlash = LastIndexOf(output, Symbols.Solidus);

                            if (lastSlash >= 0)
                            {
                                output.Length = lastSlash;
                            }
                            else
                            {
                                output.Length = 0;
                            }

                            segmentCount--;
                        }

                        close = true;
                    }
                    else if (!path.Is(CurrentDirectory))
                    {
                        if (_scheme.Is(ProtocolNames.File) &&
                            segmentCount == originalCount &&
                            path.Length == 2 &&
                            path[0].IsLetter() &&
                            path[1] == Symbols.Pipe)
                        {
                            path = path.Replace(Symbols.Pipe, Symbols.Colon);
                            output.Length = 0;
                            segmentCount = 0;
                        }

                        if (segmentCount > 0)
                        {
                            output.Append(Symbols.Solidus);
                        }

                        output.Append(path);
                        segmentCount++;
                    }
                    else
                    {
                        close = true;
                    }

                    if (close && c != Symbols.Solidus && c != Symbols.ReverseSolidus)
                    {
                        if (segmentCount > 0)
                        {
                            output.Append(Symbols.Solidus);
                        }

                        segmentCount++;
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

            buffer.ReturnToPool();
            _path = output.ToPool();
            _query = null;

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

        private static Int32 CountChar(StringBuilder sb, Char c)
        {
            var count = 0;

            for (var i = 0; i < sb.Length; i++)
            {
                if (sb[i] == c)
                {
                    count++;
                }
            }

            return count;
        }

        private static Int32 LastIndexOf(StringBuilder sb, Char c)
        {
            for (var i = sb.Length - 1; i >= 0; i--)
            {
                if (sb[i] == c)
                {
                    return i;
                }
            }

            return -1;
        }

        internal Boolean ParseQuery(String input, Int32 index, Int32 length, Boolean onlyQuery = false, Boolean fromParams = false)
        {
            var buffer = StringBuilderPool.Obtain();
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

            if (!fromParams)
            {
                _params?.ChangeTo(_query, true);
            }

            return fragment ? ParseFragment(input, index + 1, length) : true;
        }

        private Boolean ParseFragment(String input, Int32 index, Int32 length)
        {
            var buffer = StringBuilderPool.Obtain();

            while (index < length)
            {
                var c = input[index];

                switch (c)
                {
                    case Symbols.EndOfFile:
                    case Symbols.Null:
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

        private static String NormalizeInput(String input)
        {
            var trimmedInput = input.Trim(C0ControlAndSpace);

            if (trimmedInput.AsSpan().IndexOfAny('\t', '\n', '\r') < 0)
            {
                return trimmedInput;
            }

            var buffer = StringBuilderPool.Obtain();
            foreach (Char c in trimmedInput)
            {
                switch (c)
                {
                    case Symbols.Tab:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                        break;
                    default:
                        buffer.Append(c);
                        break;
                }
            }
            return buffer.ToPool();
        }

        private static String Utf8PercentDecode(String source)
        {
            // https://anglesharp.github.io/Specification-Url/#string-percent-decode
            // 1. Let bytes be the UTF-8 encoding of input.
            var bytes = TextEncoding.Utf8.GetBytes(source);
            var length = bytes.Length;

            // 2. Return the percent decoding of bytes.
            // in-place
            for (Int32 i = 0, insertIndex = 0; i < bytes.Length; i++, insertIndex++)
            {
                var cc = (Char)bytes[i];
                switch (cc)
                {
                    case Symbols.Percent:
                        if (i + 2 < bytes.Length && ((Char)bytes[i + 1]).IsHex() && ((Char)bytes[i + 2]).IsHex())
                        {
                            var weight = ((Char)bytes[i + 1]).FromHex() * 16 + ((Char)bytes[i + 2]).FromHex();
                            cc = (Char)weight;
                            i += 2;
                            length -= 2;
                        }

                        goto default;
                    default:
                        bytes[insertIndex] = (Byte)cc;
                        break;
                }
            }

            return TextEncoding.Utf8.GetString(bytes, 0, length);
        }

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

        private static Boolean TrySanatizeHost(String hostName, Int32 start, Int32 length, out String sanatizedHostName)
        {
            if (length == 0)
            {
                sanatizedHostName = String.Empty;
                return true;
            }

            if (length > 1 && hostName[start] == Symbols.SquareBracketOpen && hostName[start + length - 1] == Symbols.SquareBracketClose)
            {
                var literal = hostName.Substring(start + 1, length - 2);

                if (TryParseIpv6Address(literal, out var normalizedLiteral))
                {
                    sanatizedHostName = String.Concat("[", normalizedLiteral, "]");
                    return true;
                }

                sanatizedHostName = hostName.Substring(start, length);
                return false;
            }

            // https://anglesharp.github.io/Specification-Url/#host-parsing 3.5.4
            // string utf 8 percent decoding of input.
            var percentDecoded = Utf8PercentDecode(hostName.Substring(start, length));

            // https://anglesharp.github.io/Specification-Url/#host-parsing 3.5.5
            // domain to ASCII
            String domainToAscii;

            try
            {
                domainToAscii = DefaultIdnMapping.GetAscii(percentDecoded);
            }
            catch (ArgumentException)
            {
                sanatizedHostName = hostName.Substring(start, length);
                return false;
            }

            var buffer = StringBuilderPool.Obtain();

            // https://anglesharp.github.io/Specification-Url/#host-parsing 3.5.7
            // forbidden host code point check
            foreach (var cc in domainToAscii)
            {
                switch (cc)
                {
                    // U+0000, U+0009, U+000A, U+000D, U+0020, "#", "%", "/", ":", "?", "@", "[", "\", and "]"'
                    case Symbols.Null:
                    case Symbols.Tab:
                    case Symbols.Space:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                    case Symbols.Num:
                    case Symbols.Percent:
                    case Symbols.Solidus:
                    case Symbols.Colon:
                    case Symbols.QuestionMark:
                    case Symbols.At:
                    case Symbols.SquareBracketOpen:
                    case Symbols.SquareBracketClose:
                    case Symbols.ReverseSolidus:
                        buffer.ReturnToPool();
                        sanatizedHostName = hostName.Substring(start, length);
                        return false;
                    default:
                        buffer.Append(Char.ToLowerInvariant(cc));
                        break;
                }
            }

            sanatizedHostName = buffer.ToPool();

            if (EndsInNumber(sanatizedHostName))
            {
                if (!TryParseIpv4Address(sanatizedHostName, out sanatizedHostName))
                {
                    return false;
                }
            }

            return true;
        }

        private static Boolean EndsInNumber(String host)
        {
            var parts = host.Split(Symbols.Dot);
            var count = parts.Length;

            if (count > 1 && parts[count - 1].Length == 0)
            {
                count--;
            }

            if (count == 0)
            {
                return false;
            }

            var last = parts[count - 1];

            if (TryParseIpv4Number(last, out _))
            {
                return true;
            }

            if (last.Length == 0)
            {
                return false;
            }

            for (var i = 0; i < last.Length; i++)
            {
                if (!last[i].IsDigit())
                {
                    return false;
                }
            }

            return true;
        }

        private static Boolean TryParseIpv6Address(String value, out String parsedValue)
        {
            // Zone identifiers are not part of URL host parser IPv6 address literals.
            if (value.IndexOf(Symbols.Percent) >= 0)
            {
                parsedValue = value;
                return false;
            }

            if (IPAddress.TryParse(value, out var address) && address.AddressFamily == AddressFamily.InterNetworkV6)
            {
                parsedValue = address.ToString().ToLowerInvariant();
                return true;
            }

            parsedValue = value;
            return false;
        }

        private static Boolean TryParseIpv4Address(String host, out String parsedHost)
        {
            var parts = host.Split(Symbols.Dot);
            var count = parts.Length;

            if (count > 1 && parts[count - 1].Length == 0)
            {
                count--;
            }

            if (count == 0 || count > 4)
            {
                parsedHost = host;
                return false;
            }

            var numbers = new UInt32[count];

            for (var i = 0; i < count; i++)
            {
                if (!TryParseIpv4Number(parts[i], out numbers[i]))
                {
                    parsedHost = host;
                    return false;
                }
            }

            for (var i = 0; i < count - 1; i++)
            {
                if (numbers[i] > 255)
                {
                    parsedHost = host;
                    return false;
                }
            }

            var maxLastPart = 1UL << (8 * (5 - count));

            if (numbers[count - 1] >= maxLastPart)
            {
                parsedHost = host;
                return false;
            }

            UInt64 address = numbers[count - 1];

            for (var i = 0; i < count - 1; i++)
            {
                address += (UInt64)numbers[i] << (8 * (3 - i));
            }

            parsedHost = String.Concat(
                ((address >> 24) & 0xFF).ToString(CultureInfo.InvariantCulture), ".",
                ((address >> 16) & 0xFF).ToString(CultureInfo.InvariantCulture), ".",
                ((address >> 8) & 0xFF).ToString(CultureInfo.InvariantCulture), ".",
                (address & 0xFF).ToString(CultureInfo.InvariantCulture));
            return true;
        }

        private static Boolean TryParseIpv4Number(String value, out UInt32 parsedValue)
        {
            if (String.IsNullOrEmpty(value))
            {
                parsedValue = 0;
                return false;
            }

            var index = 0;
            var @base = 10;

            if (value.Length >= 2 && value[0] == '0')
            {
                if (value[1] is 'x' or 'X')
                {
                    @base = 16;
                    index = 2;
                }
                else
                {
                    @base = 8;
                    index = 1;
                }
            }

            if (index == value.Length)
            {
                parsedValue = 0;
                return true;
            }

            UInt64 number = 0;

            for (var i = index; i < value.Length; i++)
            {
                var digit = value[i];
                Int32 weight;

                if (digit.IsDigit())
                {
                    weight = digit - '0';
                }
                else if (digit.IsInRange('a', 'f'))
                {
                    weight = digit - 'a' + 10;
                }
                else if (digit.IsInRange('A', 'F'))
                {
                    weight = digit - 'A' + 10;
                }
                else
                {
                    parsedValue = 0;
                    return false;
                }

                if (weight >= @base)
                {
                    parsedValue = 0;
                    return false;
                }

                number = number * (UInt32)@base + (UInt32)weight;

                if (number > UInt32.MaxValue)
                {
                    parsedValue = 0;
                    return false;
                }
            }

            parsedValue = (UInt32)number;
            return true;
        }

        private static String SanatizePort(String port, Int32 start, Int32 length)
        {
            if (length < 128)
            {
                return Go(stackalloc Char[length]);
            }
            else
            {
                return Go(new Char[length]);
            }

            String Go(Span<Char> chars)
            {
                var count = 0;
                var n = start + length;
                for (var i = start; i < n; i++)
                {
                    if (count == 1 && chars[0] == '0')
                    {
                        chars[0] = port[i];
                    }
                    else
                    {
                        chars[count++] = port[i];
                    }
                }
                return chars.Slice(0, count).ToString();
            }
        }
        #endregion
    }
}

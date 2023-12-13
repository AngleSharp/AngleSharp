namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
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
        private static readonly Url DefaultBase = new Url(String.Empty, String.Empty, String.Empty);
        private static readonly Char[] C0ControlAndSpace = Enumerable.Range(0x00, 0x21).Select(c => (Char)c).ToArray();

        // Remark: `UseStd3AsciiRules = false` is against spec
        // https://anglesharp.github.io/Specification-Url/#concept-domain-to-ascii
        // > UseSTD3ASCIIRules set to beStrict
        // But if UseStd3AsciiRules it set to true, _ (underscore) will be considered invalid in host name
        // Set to false here to do loose validation
        private static readonly IdnMapping DefaultIdnMapping = new () { AllowUnassigned = false, UseStd3AsciiRules = false };

        private String? _fragment;
        private String? _query;
        private String _path;
        private String _scheme;
        private String _port;
        private String _host;
        private String? _username;
        private String? _password;
        private Boolean _relative;
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
            get => $"/{_path}";
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
                if(value == null)
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
                var hashCode =  _fragment != null ? StringComparer.Ordinal.GetHashCode(_fragment) : 0;
                hashCode = (hashCode * 397) ^ (_query != null ? StringComparer.Ordinal.GetHashCode(_query) : 0);
                hashCode = (hashCode * 397) ^ (_path != null ? StringComparer.Ordinal.GetHashCode(_path) : 0);
                hashCode = (hashCode * 397) ^ (_scheme != null ? StringComparer.OrdinalIgnoreCase.GetHashCode(_scheme) : 0);
                hashCode = (hashCode * 397) ^ (_port != null ? StringComparer.Ordinal.GetHashCode(_port) : 0);
                hashCode = (hashCode * 397) ^ (_host != null ?  StringComparer.OrdinalIgnoreCase.GetHashCode(_host) : 0);
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
                                _query = null;
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
                        if (paths.Count > 0)
                        {
                            paths.RemoveAt(paths.Count - 1);
                        }

                        close = true;
                    }
                    else if (!path.Is(CurrentDirectory))
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
            _path = String.Join("/", paths);
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
            var buffer = StringBuilderPool.Obtain();
            foreach (Char c in trimmedInput)
            {
                switch (c)
                {
                    case Symbols.Tab:
                    case Symbols.LineFeed:
                    case Symbols.CarriageReturn:
                        // parse error
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
            for (int i = 0, insertIndex = 0; i < bytes.Length; i++, insertIndex++)
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

            // TODO: IPv6 Parsing
            if (length > 1 && hostName[start] == Symbols.SquareBracketOpen && hostName[start + length - 1] == Symbols.SquareBracketClose)
            {
                sanatizedHostName = hostName.Substring(start, length);
                return true;
            }

            // https://anglesharp.github.io/Specification-Url/#host-parsing 3.5.4
            // string utf 8 percent decoding of input.
            string percentDecoded = Utf8PercentDecode(hostName.Substring(start, length));

            // https://anglesharp.github.io/Specification-Url/#host-parsing 3.5.5
            // domain to ASCII
            string domainToAscii;

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

            // TODO: IPv4 parsing
            return true;
        }

        private static String SanatizePort(String port, Int32 start, Int32 length)
        {
            Span<Char> chars = stackalloc Char[length];
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

            return chars.Slice(0, count).CreateString();
        }

#endregion
    }
}

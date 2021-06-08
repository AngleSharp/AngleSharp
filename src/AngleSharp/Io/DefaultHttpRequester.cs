namespace AngleSharp.Io
{
    using AngleSharp.Common;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The default (ready-to-use) HTTP requester.
    /// </summary>
    public sealed class DefaultHttpRequester : BaseRequester
    {
        #region Constants

        private const Int32 BufferSize = 4096;

        private static readonly String Version = typeof(DefaultHttpRequester).Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        private static readonly String AgentName = "AngleSharp/" + Version;

        #endregion

        #region Fields

        private TimeSpan _timeOut;
        private readonly Action<HttpWebRequest> _setup;
        private readonly Dictionary<String, String> _headers;

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a default HTTP requester with the information presented
        /// in the info object.
        /// </summary>
        /// <param name="userAgent">The user-agent name to use, if any.</param>
        /// <param name="setup">An optional setup function for the HttpWebRequest object.</param>
        public DefaultHttpRequester(String? userAgent = null, Action<HttpWebRequest>? setup = null)
        {
            _timeOut = new TimeSpan(0, 0, 0, 45);
            _setup = setup ?? ((HttpWebRequest r) => { });
            _headers = new Dictionary<String, String>
            {
                { HeaderNames.UserAgent, userAgent ?? AgentName },
                { HeaderNames.AcceptEncoding, "gzip, deflate" }
            };
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used headers.
        /// </summary>
        public IDictionary<String, String> Headers => _headers;

        /// <summary>
        /// Gets or sets the timeout value.
        /// </summary>
        public TimeSpan Timeout
        {
            get => _timeOut;
            set => _timeOut = value;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Checks if the given protocol is supported.
        /// </summary>
        /// <param name="protocol">
        /// The protocol to check for, e.g. http.
        /// </param>
        /// <returns>
        /// True if the protocol is supported, otherwise false.
        /// </returns>
        public override Boolean SupportsProtocol(String protocol) =>
            protocol.IsOneOf(ProtocolNames.Http, ProtocolNames.Https);

        /// <summary>
        /// Performs an asynchronous http request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancellationToken">
        /// The token for cancelling the task.
        /// </param>
        /// <returns>
        /// The task that will eventually give the response data.
        /// </returns>
        protected override async Task<IResponse> PerformRequestAsync(Request request, CancellationToken cancellationToken)
        {
            var cts = new CancellationTokenSource(_timeOut);
            var cache = new RequestState(request, _headers, _setup);

            using (cancellationToken.Register(cts.Cancel))
            {
                return await cache.RequestAsync(cts.Token).ConfigureAwait(false);
            }
        }

        #endregion

        #region Transport

        private sealed class RequestState
        {
            private static MethodInfo? _serverString;
            private readonly CookieContainer _cookies;
            private readonly IDictionary<String, String> _headers;
            private readonly HttpWebRequest _http;
            private readonly Request _request;
            private readonly Byte[] _buffer;

            public RequestState(Request request, IDictionary<String, String> headers, Action<HttpWebRequest> setup)
            {
                _cookies = new CookieContainer();
                _headers = headers;
                _request = request;
                _http = (HttpWebRequest)WebRequest.Create(request.Address);
                _http.CookieContainer = _cookies;
                _http.Method = request.Method.ToString().ToUpperInvariant();
                _buffer = new Byte[BufferSize];
                SetHeaders();
                SetCookies();
                AllowCompression();
                DisableAutoRedirect();
                setup.Invoke(_http);
            }

            public async Task<IResponse> RequestAsync(CancellationToken cancellationToken)
            {
                cancellationToken.Register(_http.Abort);

                if (_request.Method == HttpMethod.Post || _request.Method == HttpMethod.Put)
                {
                    var target = await _http.GetRequestStreamAsync().ConfigureAwait(false);
                    SendRequest(target);
                }

                var response = default(WebResponse);

                try
                {
                    response = await _http.GetResponseAsync().ConfigureAwait(false);
                }
                catch (WebException ex)
                {
                    response = ex.Response;
                }

                RaiseConnectionLimit(_http);
                return GetResponse((HttpWebResponse)response);
            }

            private void SendRequest(Stream target)
            {
                var source = _request.Content;

                while (source != null)
                {
                    var length = source.Read(_buffer, 0, BufferSize);

                    if (length == 0)
                    {
                        break;
                    }

                    target.Write(_buffer, 0, length);
                }
            }

            [return: NotNullIfNotNull("response")]
            private DefaultResponse? GetResponse(HttpWebResponse response)
            {
                if (response != null)
                {
                    var originalCookies = _cookies.GetCookies(_request.Address!);
                    var newCookies = _cookies.GetCookies(response.ResponseUri);
                    var cookies = newCookies.OfType<Cookie>().Except(originalCookies.OfType<Cookie>()).ToArray();
                    var headers = response.Headers.AllKeys.Select(m => new { Key = m, Value = response.Headers[m] });
                    var result = new DefaultResponse
                    {
                        Content = response.GetResponseStream(),
                        StatusCode = response.StatusCode,
                        Address = Url.Convert(response.ResponseUri)
                    };

                    foreach (var header in headers)
                    {
                        result.Headers.Add(header.Key, header.Value);
                    }

                    if (cookies.Length > 0)
                    {
                        var strings = cookies.Select(Stringify);
                        result.Headers[HeaderNames.SetCookie] = String.Join(", ", strings);
                    }

                    return result;
                }

                return null;
            }

            /// <summary>
            /// Dirty workaround to re-obtain the string representation of the cookie
            /// for the set-cookie header. Uses the internal ToServerString method and
            /// falls back to the ordinary ToString.
            /// </summary>
            private static String Stringify(Cookie cookie)
            {
                if (_serverString is null)
                {
                    var methods = typeof(Cookie).GetMethods();
                    var func = methods.FirstOrDefault(m => m.Name is "ToServerString");
                    _serverString = func ?? methods.FirstOrDefault(m => m.Name is "ToString");
                }

                return _serverString.Invoke(cookie, null).ToString();
            }

            /// <summary>
            /// Dirty dirty workaround since the webrequester itself is already
            /// quite stupid, but the one here (for the PCL) is really not the
            /// way things should be programmed ...
            /// </summary>
            private void AddHeader(String key, String value)
            {
                if (key.Is(HeaderNames.Accept))
                {
                    _http.Accept = value;
                }
                else if (key.Is(HeaderNames.ContentType))
                {
                    _http.ContentType = value;
                }
                else if (key.Is(HeaderNames.Expect))
                {
                    _http.Expect = value;
                }
                else if (key.Is(HeaderNames.Date))
                {
                    _http.Date = DateTime.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key.Is(HeaderNames.Host))
                {
                    _http.Host = value;
                }
                else if (key.Is(HeaderNames.IfModifiedSince))
                {
                    _http.IfModifiedSince = DateTime.Parse(value, CultureInfo.InvariantCulture);
                }
                else if (key.Is(HeaderNames.Referer))
                {
                    _http.Referer = value;
                }
                else if (key.Is(HeaderNames.UserAgent))
                {
                    _http.UserAgent = value;
                }
                else if (!key.Is(HeaderNames.Connection) && !key.Is(HeaderNames.Range) && !key.Is(HeaderNames.ContentLength) && !key.Is(HeaderNames.TransferEncoding))
                {
                    _http.Headers[key] = value;
                }
            }

            private void SetCookies()
            {
                var cookieHeader = _request.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                _cookies.SetCookies(_http.RequestUri, cookieHeader.Replace(';', ',').Replace("$", ""));
            }

            private void SetHeaders()
            {
                foreach (var header in _headers)
                {
                    AddHeader(header.Key, header.Value);
                }

                foreach (var header in _request.Headers)
                {
                    if (!header.Key.Is(HeaderNames.Cookie))
                    {
                        AddHeader(header.Key, header.Value);
                    }
                }
            }

            private void AllowCompression()
            {
                _http.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
            }

            private void DisableAutoRedirect()
            {
                _http.AllowAutoRedirect = false;
            }
        }   

        private static void RaiseConnectionLimit(HttpWebRequest http)
        {
            http.ServicePoint.ConnectionLimit = 1024;
        }

        #endregion
    }
}

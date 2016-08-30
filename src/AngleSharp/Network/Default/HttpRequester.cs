namespace AngleSharp.Network.Default
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The default (ready-to-use) HTTP requester.
    /// </summary>
    public sealed class HttpRequester : IRequester
    {
        #region Constants

        private const Int32 BufferSize = 4096;

        private static readonly String _version = typeof(HttpRequester).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>().Version;
        private static readonly String _agentName = "AngleSharp/" + _version;
        private static readonly Dictionary<String, PropertyInfo> _propCache = new Dictionary<String, PropertyInfo>();
        private static readonly List<String> _restricted = new List<String>();

        #endregion

        #region Fields

        private TimeSpan _timeOut;
        private readonly Dictionary<String, String> _headers;

        #endregion

        #region ctor

        /// <summary>
        /// Constructs a default HTTP requester with the information presented
        /// in the info object.
        /// </summary>
        /// <param name="userAgent">The user-agent name to use, if any.</param>
        public HttpRequester(String userAgent = null)
        {
            _timeOut = new TimeSpan(0, 0, 0, 45);
            _headers = new Dictionary<String, String>();
            _headers.Add("User-Agent", userAgent ?? _agentName);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used headers.
        /// </summary>
        public IDictionary<String, String> Headers
        {
            get { return _headers; }
        }

        /// <summary>
        /// Gets or sets the timeout value.
        /// </summary>
        public TimeSpan Timeout
        {
            get { return _timeOut; }
            set { _timeOut = value; }
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
        public Boolean SupportsProtocol(String protocol)
        {
            return protocol.IsOneOf(ProtocolNames.Http, ProtocolNames.Https);
        }

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
        public async Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
        {
            var cts = new CancellationTokenSource(_timeOut);
            var cache = new RequestState(request, _headers);

            using (cancellationToken.Register(cts.Cancel))
            {
                return await cache.RequestAsync(cts.Token).ConfigureAwait(false);
            }
        }

        #endregion

        #region Transport

        private sealed class RequestState
        {
            private readonly CookieContainer _cookies;
            private readonly HttpWebRequest _http;
            private readonly IRequest _request;
            private readonly Byte[] _buffer;

            public RequestState(IRequest request, IDictionary<String, String> headers)
            {
                var cookieHeader = request.Headers.GetOrDefault(HeaderNames.Cookie, String.Empty);
                _cookies = new CookieContainer();
                _request = request;
                _http = WebRequest.CreateHttp(request.Address);
                _http.CookieContainer = _cookies;
                _http.Method = request.Method.ToString().ToUpperInvariant();
                _buffer = new Byte[BufferSize];

                foreach (var header in headers)
                {
                    AddHeader(header.Key, header.Value);
                }

                foreach (var header in request.Headers)
                {
                    AddHeader(header.Key, header.Value);
                }

                _cookies.SetCookies(_http.RequestUri, cookieHeader);
            }

            public async Task<IResponse> RequestAsync(CancellationToken cancellationToken)
            {
                cancellationToken.Register(_http.Abort);
                
                if (_request.Method == HttpMethod.Post || _request.Method == HttpMethod.Put)
                {
                    var target = await Task.Factory.FromAsync<Stream>(_http.BeginGetRequestStream, _http.EndGetRequestStream, null).ConfigureAwait(false);
                    SendRequest(target);
                }

                var response = default(WebResponse);

                try
                {
                    response = await Task.Factory.FromAsync<WebResponse>(_http.BeginGetResponse, _http.EndGetResponse, null).ConfigureAwait(false);
                }
                catch (WebException ex)
                {
                    response = ex.Response;
                }

                RaiseConnectionLimit(_http);

                return GetResponse(response as HttpWebResponse);
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

            private Response GetResponse(HttpWebResponse response)
            {
                if (response != null)
                {
                    var cookies = _cookies.GetCookies(response.ResponseUri);
                    var headers = response.Headers.AllKeys.Select(m => new { Key = m, Value = response.Headers[m] });
                    var result = new Response
                    {
                        Content = response.GetResponseStream(),
                        StatusCode = response.StatusCode,
                        Address = Url.Convert(response.ResponseUri)
                    };

                    foreach (var header in headers)
                    {
                        result.Headers.Add(header.Key, header.Value);
                    }

                    if (cookies.Count > 0)
                    {
                        var strings = cookies.OfType<Cookie>().Select(m => m.ToString());
                        result.Headers[HeaderNames.SetCookie] = String.Join(", ", strings);
                    }

                    return result;
                }

                return null;
            }

            /// <summary>
            /// Dirty dirty workaround since the webrequester itself is already
            /// quite stupid, but the one here (for the PCL) is really not the
            /// way things should be programmed ...
            /// </summary>
            /// <param name="key">The key to add or change.</param>
            /// <param name="value">The value to be set.</param>
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
                    SetProperty(HeaderNames.Expect, value);
                }
                else if (key.Is(HeaderNames.Date))
                {
                    SetProperty(HeaderNames.Date, DateTime.Parse(value));
                }
                else if (key.Is(HeaderNames.Host))
                {
                    SetProperty(HeaderNames.Host, value);
                }
                else if (key.Is(HeaderNames.IfModifiedSince))
                {
                    SetProperty("IfModifiedSince", DateTime.Parse(value));
                }
                else if (key.Is(HeaderNames.Referer))
                {
                    SetProperty(HeaderNames.Referer, value);
                }
                else if (key.Is(HeaderNames.UserAgent))
                {
                    SetProperty("UserAgent", value);
                }
                else if (!key.Is(HeaderNames.Connection) && !key.Is(HeaderNames.Range) && !key.Is(HeaderNames.ContentLength) && !key.Is(HeaderNames.TransferEncoding))
                {
                    _http.Headers[key] = value;
                }
            }

            /// <summary>
            /// Sets properties of the special headers (described here
            /// http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.headers.aspx)
            /// which are not accessible (in general) in this profile
            /// (profile78). However, usually they are here and can be modified
            /// with reflection. If not they are not set.
            /// </summary>
            /// <param name="name">The name of the property.</param>
            /// <param name="value">
            /// The value of the property, which will be set.
            /// </param>
            private void SetProperty(String name, Object value)
            {
                var property = default(PropertyInfo);

                if (!_propCache.TryGetValue(name, out property))
                {
                    lock (_propCache)
                    {
                        if (!_propCache.TryGetValue(name, out property))
                        {
                            property = _http.GetType().GetProperty(name);
                            _propCache.Add(name, property);
                        }
                    }
                }

                if (!_restricted.Contains(name) && property != null && property.CanWrite)
                {
                    try
                    {
                        //This might fail on certain platforms
                        property.SetValue(_http, value, null);
                    }
                    catch
                    {
                        //Catch any failure and do not try again on the same platform
                        lock (_restricted)
                        {
                            if (!_restricted.Contains(name))
                            {
                                _restricted.Add(name);
                            }
                        }
                    }
                }
            }
        }

        private static void RaiseConnectionLimit(HttpWebRequest http)
        {
            var field = typeof(HttpWebRequest).GetField("_ServicePoint");
            var servicePoint = field?.GetValue(http);

            if (servicePoint != null)
            {
                var connectionLimit = servicePoint.GetType().GetProperty("ConnectionLimit");
                connectionLimit?.SetValue(servicePoint, 1024);
            }
        }

        #endregion
    }
}

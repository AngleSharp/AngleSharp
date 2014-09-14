namespace AngleSharp.Network
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Reflection;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// The default (ready-to-use) HTTP requester.
    /// </summary>
    public class DefaultRequester : IRequester
    {
        #region Constants

        const Int32 BufferSize = 4096;

        static readonly Dictionary<String, PropertyInfo> _propCache;

        #endregion

        #region Fields

        TimeSpan _timeOut;
        readonly Dictionary<String, String> _headers;

        #endregion

        #region ctor

        static DefaultRequester()
        {
            _propCache = new Dictionary<String, PropertyInfo>();
        }

        /// <summary>
        /// Constructs a default HTTP requester with the information
        /// presented in the info object.
        /// </summary>
        /// <param name="info">The information to use.</param>
        public DefaultRequester(IInfo info)
        {
            _timeOut = new TimeSpan(0, 0, 0, 45);
            _headers = new Dictionary<String, String>();
            _headers.Add("User-Agent", info.Agent);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the used headers.
        /// </summary>
        public Dictionary<String, String> Headers
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
        /// <param name="protocol">The protocol to check for, e.g. http.</param>
        /// <returns>True if the protocol is supported, otherwise false.</returns>
        public Boolean SupportsProtocol(String protocol)
        {
            return KnownProtocols.Http == protocol || KnownProtocols.Https == protocol;
        }

        /// <summary>
        /// Performs a blocking http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The response data.</returns>
        public IResponse Request(IRequest request)
        {
            var cache = new RequestState(request, _headers);
            return cache.Request();
        }

        /// <summary>
        /// Performs an asynchronous http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public Task<IResponse> RequestAsync(IRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// Performs an asynchronous http request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancellationToken">The token for cancelling the task.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public Task<IResponse> RequestAsync(IRequest request, CancellationToken cancellationToken)
        {
            var cache = new RequestState(request, _headers);
            return cache.RequestAsync(cancellationToken);
        }

        #endregion

        #region Transport

        sealed class RequestState
        {
            TaskCompletionSource<Boolean> _completed;
            HttpWebResponse _response;

            readonly HttpWebRequest _http;
            readonly IRequest _request;
            readonly Byte[] _buffer;

            public RequestState(IRequest request, IDictionary<String, String> headers)
            {
                _request = request;
                _http = WebRequest.Create(request.Address) as HttpWebRequest;
                _http.Method = request.Method.ToString();
                _buffer = new Byte[BufferSize];
                _completed = new TaskCompletionSource<Boolean>();

                foreach (var header in headers)
                    AddHeader(header.Key, header.Value);

                foreach (var header in request.Headers)
                    AddHeader(header.Key, header.Value);
            }

            public IResponse Request()
            {
                if (_request.Method == HttpMethod.Post || _request.Method == HttpMethod.Put)
                {
                    _http.BeginGetRequestStream(SendRequest, _request);
                    _completed.Task.Wait();
                    _completed = new TaskCompletionSource<Boolean>();
                }

                _http.BeginGetResponse(ReceiveResponse, null);
                _completed.Task.Wait();
                return GetResponse();
            }

            public async Task<IResponse> RequestAsync(CancellationToken cancellationToken)
            {
                if (_request.Method == HttpMethod.Post || _request.Method == HttpMethod.Put)
                {
                    _http.BeginGetRequestStream(SendRequest, _request);

                    if (cancellationToken.IsCancellationRequested)
                        return null;

                    await _completed.Task;
                    _completed = new TaskCompletionSource<Boolean>();
                }

                if (cancellationToken.IsCancellationRequested)
                    return null;

                _http.BeginGetResponse(ReceiveResponse, null);
                await _completed.Task;

                if (cancellationToken.IsCancellationRequested)
                    return null;

                return GetResponse();
            }

            void SendRequest(IAsyncResult ar)
            {
                var carrier = (IRequest)ar.AsyncState;
                var source = carrier.Content;
                var target = _http.EndGetRequestStream(ar);

                if (source != null)
                {
                    while (source != null)
                    {
                        var length = source.Read(_buffer, 0, BufferSize);

                        if (length == 0)
                            break;

                        target.Write(_buffer, 0, length);
                    }
                }

                _completed.SetResult(true);
            }

            void ReceiveResponse(IAsyncResult ar)
            {
                try { _response = (HttpWebResponse)_http.EndGetResponse(ar); }
                catch (WebException ex) { _response = (HttpWebResponse)ex.Response; }
                _completed.SetResult(true);
            }

            DefaultResponse GetResponse()
            {
                var result = new DefaultResponse();
                var headers = _response.Headers.AllKeys.Select(m => new { Key = m, Value = _response.Headers[m] });
                result.Content = _response.GetResponseStream();
                result.StatusCode = _response.StatusCode;
                result.Address = new Url(_response.ResponseUri);

                foreach (var header in headers)
                    result.Headers.Add(header.Key, header.Value);

                return result;
            }

            /// <summary>
            /// Dirty dirty workaround since the webrequester itself is
            /// already quite stupid, but the one here (for the PCL) is
            /// really not the way things should be programmed ...
            /// </summary>
            /// <param name="key">The key to add or change.</param>
            /// <param name="value">The value to be set.</param>
            void AddHeader(String key, String value)
            {
                if (key == HeaderNames.Accept)
                    _http.Accept = value;
                else if (key == HeaderNames.ContentType)
                    _http.ContentType = value;
                else if (key == HeaderNames.Expect)
                    SetProperty(HeaderNames.Expect, value);
                else if (key == HeaderNames.Date)
                    SetProperty(HeaderNames.Date, DateTime.Parse(value));
                else if (key == HeaderNames.Host)
                    SetProperty(HeaderNames.Host, value);
                else if (key == HeaderNames.IfModifiedSince)
                    SetProperty("IfModifiedSince", DateTime.Parse(value));
                else if (key == HeaderNames.Referer)
                    SetProperty(HeaderNames.Referer, value);
                else if (key == HeaderNames.UserAgent)
                    SetProperty("UserAgent", value);
                else if (key != HeaderNames.Connection && key != HeaderNames.Range && key != HeaderNames.ContentLength && key != HeaderNames.TransferEncoding)
                    _http.Headers[key] = value;
            }

            /// <summary>
            /// Sets properties of the special headers (desc. here
            /// http://msdn.microsoft.com/en-us/library/system.net.httpwebrequest.headers.aspx)
            /// which are not accessible (in general) in this profile (profile78).
            /// However, usually they are here and can be modified with reflection.
            /// If not they are not set.
            /// </summary>
            /// <param name="name">The name of the property.</param>
            /// <param name="value">The value of the property, which will be set.</param>
            void SetProperty(String name, Object value)
            {
                if (!_propCache.ContainsKey(name))
                    _propCache.Add(name, _http.GetType().GetTypeInfo().GetDeclaredProperty(name));

                var property = _propCache[name];

                if (property != null)
                    property.SetValue(_http, value);
            }
        }

        #endregion
    }
}

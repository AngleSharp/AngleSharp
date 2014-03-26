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
    sealed class DefaultHttpRequester : IHttpRequester
    {
        #region Constants

        const Int32 CHUNK = 4096;

        static readonly Dictionary<String, PropertyInfo> _propCache;

        #endregion

        #region Fields

        TimeSpan _timeOut;
        HttpWebRequest _http;
        HttpWebResponse _response;
        TaskCompletionSource<Boolean> _completed;

        readonly Byte[] _buffer;
        readonly Dictionary<String, String> _headers;

        #endregion

        #region ctor

        static DefaultHttpRequester()
        {
            _propCache = new Dictionary<String, PropertyInfo>();
        }

        /// <summary>
        /// Constructs a default HTTP requester with the
        /// default information (placed in the dependency resolver).
        /// </summary>
        public DefaultHttpRequester()
            : this(DependencyResolver.Current.GetService<IInfo>() ?? new DefaultInfo())
        {
        }

        /// <summary>
        /// Constructs a default HTTP requester with the information
        /// presented in the info object.
        /// </summary>
        /// <param name="info">The information to use.</param>
        public DefaultHttpRequester(IInfo info)
        {
            _buffer = new Byte[CHUNK];
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
        /// Performs a blocking http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The response data.</returns>
        public IHttpResponse Request(IHttpRequest request)
        {
            if (CreateRequest(request))
            {
                _http.BeginGetRequestStream(SendRequest, request);
                _completed.Task.Wait();
                _completed = new TaskCompletionSource<Boolean>();
            }

            _http.BeginGetResponse(ReceiveResponse, null);
            _completed.Task.Wait();
            return GetResponse();
        }

        /// <summary>
        /// Performs an asynchronous http request with the given options.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public Task<IHttpResponse> RequestAsync(IHttpRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        /// <summary>
        /// Performs an asynchronous http request that can be cancelled.
        /// </summary>
        /// <param name="request">The options to consider.</param>
        /// <param name="cancellationToken">The token for cancelling the task.</param>
        /// <returns>The task that will eventually give the response data.</returns>
        public async Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken)
        {
            if (CreateRequest(request))
            {
                _http.BeginGetRequestStream(SendRequest, request);

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

        #endregion

        #region Helpers

        Boolean CreateRequest(IHttpRequest request)
        {
            _completed = new TaskCompletionSource<Boolean>();
            _http = WebRequest.Create(request.Address) as HttpWebRequest;
            _http.Method = request.Method.ToString();

            foreach (var header in _headers)
                AddHeader(header.Key, header.Value);

            foreach (var header in request.Headers)
                AddHeader(header.Key, header.Value);

            return request.Method == HttpMethod.POST || request.Method == HttpMethod.PUT;
        }

        void SendRequest(IAsyncResult ar)
        {
            var carrier = (IHttpRequest)ar.AsyncState;
            var source = carrier.Content;
            var target = _http.EndGetRequestStream(ar);

            if (source != null)
            {
                while (source != null)
                {
                    var length = source.Read(_buffer, 0, CHUNK);

                    if (length == 0)
                        break;

                    target.Write(_buffer, 0, length);
                }
            }

            _completed.SetResult(true);
        }

        void ReceiveResponse(IAsyncResult ar)
        {
            try
            {
                _response = (HttpWebResponse)_http.EndGetResponse(ar);
            }
            catch (WebException ex)
            {
                _response = (HttpWebResponse)ex.Response;
            }

            _completed.SetResult(true);
        }

        DefaultHttpResponse GetResponse()
        {
            var result = new DefaultHttpResponse();
            var headers = _response.Headers.AllKeys.Select(m => new { Key = m, Value = _response.Headers[m] });
            result.Content = _response.GetResponseStream();
            result.StatusCode = _response.StatusCode;

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
            else if (key == HeaderNames.Content_Type)
                _http.ContentType = value;
            else if (key == HeaderNames.Expect)
                SetProperty("Expect", value);
            else if (key == HeaderNames.Date)
                SetProperty("Date", DateTime.Parse(value));
            else if (key == HeaderNames.Host)
                SetProperty("Host", value);
            else if (key == HeaderNames.If_Modified_Since)
                SetProperty("IfModifiedSince", DateTime.Parse(value));
            else if (key == HeaderNames.Referer)
                SetProperty("Referer", value);
            else if (key == HeaderNames.User_Agent)
                SetProperty("UserAgent", value);
            else if (key != HeaderNames.Connection && key != HeaderNames.Range && key != HeaderNames.Content_Length && key != HeaderNames.Transfer_Encoding)
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
                _propCache.Add(name, _http.GetType().GetProperty(name));

            var property = _propCache[name];

            if (property != null)
                property.SetValue(_http, value, null);
        }

        #endregion
    }
}

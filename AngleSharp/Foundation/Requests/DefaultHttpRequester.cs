namespace AngleSharp
{
    using AngleSharp.Interfaces;
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

        static TimeSpan _timeOut;
        static Dictionary<String, String> _defaultHeaders;
        static readonly Dictionary<String, PropertyInfo> _propCache;

        #endregion

        #region Members

        Byte[] buffer;
        HttpWebRequest http;
        HttpWebResponse response;
        TaskCompletionSource<Boolean> completed;

        #endregion

        #region ctor

        static DefaultHttpRequester()
        {
            _propCache = new Dictionary<String, PropertyInfo>();
            _defaultHeaders = new Dictionary<String, String>();
            _defaultHeaders.Add("User-Agent", Info.Agent);
            _timeOut = new TimeSpan(0, 0, 0, 45);
        }

        public DefaultHttpRequester()
        {
            buffer = new Byte[CHUNK];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default headers.
        /// </summary>
        public Dictionary<String, String> DefaultHeaders
        {
            get { return _defaultHeaders; }
            set { _defaultHeaders = value; }
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

        public IHttpResponse Request(IHttpRequest request)
        {
            if (CreateRequest(request))
            {
                http.BeginGetRequestStream(SendRequest, request);
                completed.Task.Wait();
                completed = new TaskCompletionSource<Boolean>();
            }

            http.BeginGetResponse(ReceiveResponse, null);
            completed.Task.Wait();
            return GetResponse();
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request)
        {
            return RequestAsync(request, CancellationToken.None);
        }

        public async Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken)
        {
            if (CreateRequest(request))
            {
                http.BeginGetRequestStream(SendRequest, request);

                if (cancellationToken.IsCancellationRequested)
                    return null;

                await completed.Task;
                completed = new TaskCompletionSource<Boolean>();
            }

            if (cancellationToken.IsCancellationRequested)
                return null;

            http.BeginGetResponse(ReceiveResponse, null);
            await completed.Task;

            if (cancellationToken.IsCancellationRequested)
                return null;

            return GetResponse();
        }

        #endregion

        #region Helpers

        Boolean CreateRequest(IHttpRequest request)
        {
            completed = new TaskCompletionSource<Boolean>();
            http = WebRequest.CreateHttp(request.Address);
            http.Method = request.Method.ToString();

            foreach (var header in DefaultHeaders)
                AddHeader(header.Key, header.Value);

            foreach (var header in request.Headers)
                AddHeader(header.Key, header.Value);

            return request.Method == HttpMethod.POST || request.Method == HttpMethod.PUT;
        }

        void SendRequest(IAsyncResult ar)
        {
            var carrier = (IHttpRequest)ar.AsyncState;
            var source = carrier.Content;
            var target = http.EndGetRequestStream(ar);

            if (source != null)
            {
                while (source != null)
                {
                    var length = source.Read(buffer, 0, CHUNK);

                    if (length == 0)
                        break;

                    target.Write(buffer, 0, length);
                }
            }

            completed.SetResult(true);
        }

        void ReceiveResponse(IAsyncResult ar)
        {
            try
            {
                response = (HttpWebResponse)http.EndGetResponse(ar);
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
            }

            completed.SetResult(true);
        }

        DefaultHttpResponse GetResponse()
        {
            var result = new DefaultHttpResponse();
            var headers = response.Headers.AllKeys.Select(m => new { Key = m, Value = response.Headers[m] });
            result.Content = response.GetResponseStream();
            result.StatusCode = response.StatusCode;

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
            if (key == Headers.Accept)
                http.Accept = value;
            else if (key == Headers.Content_Type)
                http.ContentType = value;
            else if (key == Headers.Expect)
                SetProperty("Expect", value);
            else if (key == Headers.Date)
                SetProperty("Date", DateTime.Parse(value));
            else if (key == Headers.Host)
                SetProperty("Host", value);
            else if (key == Headers.If_Modified_Since)
                SetProperty("IfModifiedSince", DateTime.Parse(value));
            else if (key == Headers.Referer)
                SetProperty("Referer", value);
            else if (key == Headers.User_Agent)
                SetProperty("UserAgent", value);
            else if (key != Headers.Connection && key != Headers.Range && key != Headers.Content_Length && key != Headers.Transfer_Encoding)
                http.Headers[key] = value;
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
                _propCache.Add(name, http.GetType().GetTypeInfo().GetDeclaredProperty(name));

            var property = _propCache[name];

            if (property != null)
                property.SetValue(http, value);
        }

        #endregion
    }
}

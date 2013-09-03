using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
using System.Reflection;
using System.Threading.Tasks;
using System.Collections.Generic;
using AngleSharp.Interfaces;

namespace AngleSharp
{
    /// <summary>
    /// The default (ready-to-use) HTTP requester.
    /// </summary>
    sealed class DefaultHttpRequester : IHttpRequester
    {
        #region Constants

        const Int32 CHUNK = 4096;

        static readonly Dictionary<String, String> _defaultHeaders;
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
        }

        public DefaultHttpRequester()
        {
            buffer = new Byte[CHUNK];
            DefaultHeaders = _defaultHeaders;
            Timeout = new TimeSpan(0, 0, 0, 45);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the default headers.
        /// </summary>
        public Dictionary<String, String> DefaultHeaders
        {
            get;
            set;
        }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan Timeout
        {
            get;
            set;
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
            switch (key)
            {
                case Headers.ACCEPT:
                    http.Accept = value;
                    break;

                case Headers.CONTENT_TYPE:
                    http.ContentType = value;
                    break;

                case Headers.EXPECT:
                    SetProperty("Expect", value);
                    break;

                case Headers.DATE:
                    SetProperty("Date", DateTime.Parse(value));
                    break;

                case Headers.HOST:
                    SetProperty("Host", value);
                    break;

                case Headers.IF_MODIFIED_SINCE:
                    SetProperty("IfModifiedSince", DateTime.Parse(value));
                    break;

                case Headers.REFERER:
                    SetProperty("Referer", value);
                    break;

                case Headers.USER_AGENT:
                    SetProperty("UserAgent", value);
                    break;

                case Headers.CONNECTION:
                case Headers.RANGE:
                case Headers.CONTENT_LENGTH:
                case Headers.TRANSFER_ENCODING:
                    break;

                default:
                    http.Headers[key] = value;
                    break;
            }
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

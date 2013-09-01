using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Threading;
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

        #endregion

        #region Members

        Byte[] buffer;
        HttpWebRequest http;

        #endregion

        #region ctor

        static DefaultHttpRequester()
        {
            _defaultHeaders = new Dictionary<String, String>();
            //_defaultHeaders.Add("", "");
        }

        public DefaultHttpRequester()
        {
            buffer = new Byte[CHUNK];
            DefaultHeaders = _defaultHeaders;
            Timeout = new TimeSpan(0, 0, 0, 45);
        }

        #endregion

        #region Properties

        public Dictionary<String, String> DefaultHeaders
        {
            get;
            set;
        }

        public TimeSpan Timeout
        {
            get;
            set;
        }

        #endregion

        #region Methods

        public IHttpResponse Request(IHttpRequest request)
        {
            var response = new DefaultHttpResponse();
            http = WebRequest.CreateHttp(request.Address);
            http.Method = request.Method.ToString();

            var result = http.BeginGetRequestStream(SendRequest, request);
            result.AsyncWaitHandle.WaitOne();

            result = http.BeginGetResponse(ReceiveResponse, response);
            result.AsyncWaitHandle.WaitOne();

            return response;
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request)
        {
            return RequestAsync(request, new CancellationToken());
        }

        public Task<IHttpResponse> RequestAsync(IHttpRequest request, CancellationToken cancellationToken)
        {
            http = WebRequest.CreateHttp("");
            //TODO
            return null;
        }

        #endregion

        #region Helpers

        void SendRequest(IAsyncResult ar)
        {
            var request = (IHttpRequest)ar.AsyncState;
            var source = request.Content;
            var target = http.EndGetRequestStream(ar);
            source.Seek(0, SeekOrigin.Begin);

            while (source != null)
            {
                var length = source.Read(buffer, 0, CHUNK);

                if (length == 0)
                    break;

                target.Write(buffer, 0, length);
            }
        }

        void ReceiveResponse(IAsyncResult ar)
        {
            var response = (IHttpResponse)ar.AsyncState;
            var wr = (HttpWebResponse)http.EndGetResponse(ar);
            var headers = wr.Headers.AllKeys.Select(m => new { Key = m, Value = wr.Headers[m] });
            response.Content = wr.GetResponseStream();
            response.StatusCode = wr.StatusCode;

            foreach (var header in headers)
                response.Headers.Add(header.Key, header.Value);
        }

        #endregion
    }
}

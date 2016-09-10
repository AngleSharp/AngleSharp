namespace AngleSharp.Network
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;

    /// <summary>
    /// The virtual response class.
    /// </summary>
    public class VirtualResponse : IResponse
    {
        #region Fields

        private Url _address;
        private HttpStatusCode _status;
        private Dictionary<String, String> _headers;
        private Stream _content;
        private Boolean _dispose;

        #endregion

        #region ctor

        private VirtualResponse()
        {
            _address = Url.Create("http://localhost/");
            _status = HttpStatusCode.OK;
            _headers = new Dictionary<String, String>();
            _content = MemoryStream.Null;
            _dispose = false;
        }

        /// <summary>
        /// Creates a new virtual response.
        /// </summary>
        /// <param name="request">The request callback.</param>
        /// <returns>The resulted response.</returns>
        public static IResponse Create(Action<VirtualResponse> request)
        {
            var vr = new VirtualResponse();
            request(vr);
            return vr;
        }

        #endregion

        #region Properties

        Url IResponse.Address
        {
            get { return _address; }
        }

        Stream IResponse.Content
        {
            get { return _content; }
        }

        IDictionary<String, String> IResponse.Headers
        {
            get { return _headers; }
        }

        HttpStatusCode IResponse.StatusCode
        {
            get { return _status; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets the location of the response to the given url.
        /// </summary>
        /// <param name="url">The imaginary url of the response.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Address(Url url)
        {
            _address = url;
            return this;
        }

        /// <summary>
        /// Sets the location of the response to the provided address.
        /// </summary>
        /// <param name="address">The string to use as an url.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Address(String address)
        {
            return Address(Url.Create(address ?? String.Empty));
        }

        /// <summary>
        /// Sets the location of the response to the uri's value.
        /// </summary>
        /// <param name="url">The Uri instance to convert.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Address(Uri url)
        {
            return Address(Url.Convert(url));
        }

        /// <summary>
        /// Sets the status code.
        /// </summary>
        /// <param name="code">The status code to set.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Status(HttpStatusCode code)
        {
            _status = code;
            return this;
        }

        /// <summary>
        /// Sets the status code by providing the integer value.
        /// </summary>
        /// <param name="code">The integer representing the code.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Status(Int32 code)
        {
            return Status((HttpStatusCode)code);
        }

        /// <summary>
        /// Sets the header with the given name and value.
        /// </summary>
        /// <param name="name">The header name to set.</param>
        /// <param name="value">The value for the key.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Header(String name, String value)
        {
            _headers[name] = value;
            return this;
        }

        /// <summary>
        /// Sets the headers with the name of the properties and their 
        /// assigned values.
        /// </summary>
        /// <param name="obj">The object to decompose.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Headers(Object obj)
        {
            var headers = obj.ToDictionary();
            return Headers(headers);
        }

        /// <summary>
        /// Sets the headers with the name of the keys and their assigned
        /// values.
        /// </summary>
        /// <param name="headers">The dictionary to use.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Headers(IDictionary<String, String> headers)
        {
            foreach (var header in headers)
            {
                Header(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Sets the response's content from the provided string.
        /// </summary>
        /// <param name="text">The text to use as content.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Content(String text)
        {
            Release();
            var raw = TextEncoding.Utf8.GetBytes(text);
            _content = new MemoryStream(raw);
            _dispose = true;
            return this;
        }

        /// <summary>
        /// Sets the response's content from the provided stream.
        /// </summary>
        /// <param name="stream">The response's content stream.</param>
        /// <param name="shouldDispose">True to dispose afterwards.</param>
        /// <returns>The current instance.</returns>
        public VirtualResponse Content(Stream stream, Boolean shouldDispose = false)
        {
            Release();
            _content = stream;
            _dispose = shouldDispose;
            return this;
        }

        #endregion

        #region Helpers

        private void Release()
        {
            if (_dispose)
            {
                _content?.Dispose();
            }

            _dispose = false;
            _content = null;
        }

        void IDisposable.Dispose()
        {
            Release();
        }

        #endregion
    }
}

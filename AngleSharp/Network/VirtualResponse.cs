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

        Url address;
        HttpStatusCode status;
        Dictionary<String, String> headers;
        TextSource source;
        Stream content;
        Boolean dispose;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new virtual response.
        /// </summary>
        public VirtualResponse()
        {
            address = Url.Create("http://localhost/");
            status = HttpStatusCode.OK;
            headers = new Dictionary<String, String>();
            content = MemoryStream.Null;
            source = null;
            dispose = false;
        }

        #endregion

        #region Properties

        Url IResponse.Address
        {
            get { return address; }
        }

        Stream IResponse.Content
        {
            get { return content; }
        }

        IDictionary<String, String> IResponse.Headers
        {
            get { return headers; }
        }

        HttpStatusCode IResponse.StatusCode
        {
            get { return status; }
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
            address = url;
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
            status = code;
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
            headers[name] = value;
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
            source = new TextSource(text);
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
            content = stream;
            dispose = shouldDispose;
            return this;
        }

        void Release()
        {
            if (content != null && dispose)
            {
                content.Dispose();
            }
            else if (source != null)
            {
                source.Dispose();
            }

            dispose = false;
            source = null;
            content = null;
        }

        void IDisposable.Dispose()
        {
            Release();
        }

        internal TextSource CreateSourceFor(IConfiguration configuration)
        {
            if (source != null)
            {
                return source;
            }
            else if (content != null)
            {
                return new TextSource(content, configuration.DefaultEncoding());
            }
            else
            {
                return new TextSource(String.Empty);
            }
        }

        #endregion
    }
}

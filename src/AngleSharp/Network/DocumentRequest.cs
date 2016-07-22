namespace AngleSharp.Network
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.IO;

    /// <summary>
    /// Represents the arguments to load a document.
    /// </summary>
    public class DocumentRequest
    {
        #region ctor

        /// <summary>
        /// Creates a new document request for the given url.
        /// </summary>
        /// <param name="target">The resource's url.</param>
        public DocumentRequest(Url target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));

            Headers = new Dictionary<String, String>(StringComparer.OrdinalIgnoreCase);
            Target = target;
            Method = HttpMethod.Get;
            Body = MemoryStream.Null;
        }

        #endregion

        #region Static Construction

        /// <summary>
        /// Creates a GET request for the given target from the optional source
        /// node and optional referer string.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="source">The optional source of the request.</param>
        /// <param name="referer">The optional referrer string.</param>
        /// <returns>The new document request.</returns>
        public static DocumentRequest Get(Url target, INode source = null, String referer = null)
        {
            return new DocumentRequest(target)
            {
                Method = HttpMethod.Get,
                Referer = referer,
                Source = source
            };
        }

        /// <summary>
        /// Creates a POST request for the given target with the provided body
        /// and encoding type from the optional source node and optional
        /// referer string.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="body">The body of the request.</param>
        /// <param name="type">The type of the request's body.</param>
        /// <param name="source">The optional source of the request.</param>
        /// <param name="referer">The optional referrer string.</param>
        /// <returns>The new document request.</returns>
        public static DocumentRequest Post(Url target, Stream body, String type, INode source = null, String referer = null)
        {
            if (body == null)
                throw new ArgumentNullException(nameof(body));

            if (type == null)
                throw new ArgumentNullException(nameof(type));

            return new DocumentRequest(target)
            {
                Method = HttpMethod.Post,
                Body = body,
                MimeType = type,
                Referer = referer,
                Source = source
            };
        }

        /// <summary>
        /// Creates a POST request for the given target with the fields being
        /// used to generate the body and encoding type plaintext.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="fields">The fields to send.</param>
        /// <returns>The new document request.</returns>
        public static DocumentRequest PostAsPlaintext(Url target, IDictionary<String, String> fields)
        {
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            var fds = new FormDataSet();

            foreach (var field in fields)
            {
                fds.Append(field.Key, field.Value, InputTypeNames.Text);
            }

            return Post(target, fds.AsPlaintext(), MimeTypeNames.Plain);
        }

        /// <summary>
        /// Creates a POST request for the given target with the fields being
        /// used to generate the body and encoding type url encoded.
        /// </summary>
        /// <param name="target">The target to use.</param>
        /// <param name="fields">The fields to send.</param>
        /// <returns>The new document request.</returns>
        public static DocumentRequest PostAsUrlencoded(Url target, IDictionary<String, String> fields)
        {
            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            var fds = new FormDataSet();

            foreach (var field in fields)
            {
                fds.Append(field.Key, field.Value, InputTypeNames.Text);
            }

            return Post(target, fds.AsUrlEncoded(), MimeTypeNames.UrlencodedForm);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the source of the request, if any.
        /// </summary>
        public INode Source
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the target of the request.
        /// </summary>
        public Url Target
        {
            get;
        }

        /// <summary>
        /// Gets or sets the referrer of the request, if any. The name is
        /// intentionally spelled wrong, to emphasize the relationship with the
        /// HTTP header.
        /// </summary>
        public String Referer
        {
            get { return GetHeader(HeaderNames.Referer); }
            set { SetHeader(HeaderNames.Referer, value); }
        }

        /// <summary>
        /// Gets or sets the method to use.
        /// </summary>
        public HttpMethod Method
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the stream of the request's body.
        /// </summary>
        public Stream Body
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the mime-type to use, if any.
        /// </summary>
        public String MimeType
        {
            get { return GetHeader(HeaderNames.ContentType); }
            set { SetHeader(HeaderNames.ContentType, value); }
        }

        /// <summary>
        /// Gets a list of headers (key-values) that should be used.
        /// </summary>
        public Dictionary<String, String> Headers
        {
            get;
        }

        #endregion

        #region Helpers

        private void SetHeader(String name, String value)
        {
            Headers[name] = value;
        }

        private String GetHeader(String name)
        {
            var value = default(String);
            Headers.TryGetValue(name, out value);
            return value;
        }

        #endregion
    }
}

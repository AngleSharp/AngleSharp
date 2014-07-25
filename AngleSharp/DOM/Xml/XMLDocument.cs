namespace AngleSharp.DOM.Xml
{
    using System;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument(TextSource source)
            : base(source)
        {
            _contentType = MimeTypes.Xml;
        }

        internal XmlDocument()
            : this(new TextSource(String.Empty))
        {
        }

        Boolean IXmlDocument.Load(String url)
        {
            Uri uri;
            _location.Href = url;
            Cookie = String.Empty;

            if (!Uri.TryCreate(url, UriKind.Absolute, out uri))
                throw new ArgumentException("The given URL is not valid as an absolute URL.");

            var task = Options.LoadAsync(uri);

            var result = task.ContinueWith(m =>
            {
                if (m.IsCompleted && !m.IsFaulted)
                {
                    Load(m.Result);
                    return true;
                }

                return false;
            });

            result.Wait();
            return result.Result;
        }
    }
}

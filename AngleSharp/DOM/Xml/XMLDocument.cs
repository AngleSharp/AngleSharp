namespace AngleSharp.DOM.Xml
{
    using System;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument(ITextSource source)
            : base(source)
        {
            ContentType = MimeTypes.Xml;
        }

        internal XmlDocument(String source)
            : this(new TextSource(source))
        {
        }

        internal XmlDocument()
            : this(String.Empty)
        {
        }

        Boolean IXmlDocument.LoadXml(String url)
        {
            Uri uri;

            Location.Href = url;
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

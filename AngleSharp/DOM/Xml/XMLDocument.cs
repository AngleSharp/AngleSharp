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
            Location.Href = url;
            Cookie = String.Empty;
            var task = Options.LoadAsync(new Url(url));

            var result = task.ContinueWith(m =>
            {
                if (m.IsCompleted && !m.IsFaulted && m.Result != null)
                {
                    Load(m.Result.Content);
                    return true;
                }

                return false;
            });

            result.Wait();
            return result.Result;
        }
    }
}

namespace AngleSharp.Html.Submitters
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    sealed class MultipartFormDataSetVisitor : IFormSubmitter
    {
        #region Fields

        static readonly String DashDash = "--";

        readonly Encoding _encoding;
        readonly List<Action<StreamWriter>> _writers;
        readonly String _boundary;

        #endregion

        #region ctor

        public MultipartFormDataSetVisitor(Encoding encoding, String boundary)
        {
            _encoding = encoding;
            _writers = new List<Action<StreamWriter>>();
            _boundary = boundary;
        }

        #endregion

        #region Methods

        public void Text(FormDataSetEntry entry, String value)
        {
            if (entry.HasName && value != null)
            {
                _writers.Add(stream =>
                {
                    stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"", entry.Name.HtmlEncode(_encoding));
                    stream.WriteLine();
                    stream.WriteLine(value.HtmlEncode(_encoding));
                });
            }
        }

        public void File(FormDataSetEntry entry, String fileName, String contentType, IFile content)
        {
            if (entry.HasName)
            {
                _writers.Add(stream =>
                {
                    var hasContent = content != null && content?.Name != null && content.Type != null && content.Body != null;

                    stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"",
                        entry.Name.HtmlEncode(_encoding), fileName.HtmlEncode(_encoding));
                    stream.WriteLine("Content-Type: {0}", contentType);
                    stream.WriteLine();

                    if (hasContent)
                    {
                        stream.Flush();
                        content.Body.CopyTo(stream.BaseStream);
                    }

                    stream.WriteLine();
                });
            }
        }

        public void Serialize(StreamWriter stream)
        {
            foreach (var writer in _writers)
            {
                stream.Write(DashDash);
                stream.WriteLine(_boundary);
                writer(stream);
            }

            stream.Write(DashDash);
            stream.Write(_boundary);
            stream.Write(DashDash);
        }

        #endregion
    }
}

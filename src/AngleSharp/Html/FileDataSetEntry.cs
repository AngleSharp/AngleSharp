namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Network;
    using System;
    using System.IO;
    using System.Text;

    /// <summary>
    /// A file entry in a form.
    /// </summary>
    sealed class FileDataSetEntry : FormDataSetEntry
    {
        private readonly IFile _value;

        public FileDataSetEntry(String name, IFile value, String type)
            : base(name, type)
        {
            _value = value;
        }

        public String FileName
        {
            get { return _value?.Name ?? String.Empty; }
        }

        public String ContentType
        {
            get { return _value?.Type ?? MimeTypeNames.Binary; }
        }

        public override Boolean Contains(String boundary, Encoding encoding)
        {
            var result = false;
            var content = _value?.Body;

            #if !NET40 && !SL50
            if (content != null && content.CanSeek)
            {
                using (var sr = new StreamReader(content, encoding, false, 4096, true))
                {
                    while (sr.Peek() != -1)
                    {
                        var line = sr.ReadLine();

                        if (line.Contains(boundary))
                        {
                            result = true;
                            break;
                        }
                    }
                }

                content.Seek(0, SeekOrigin.Begin);
            }
            #endif

            return result;
        }

        public override void Accept(IFormDataSetVisitor visitor)
        {
            visitor.File(this, FileName, ContentType, _value);
        }
    }
}

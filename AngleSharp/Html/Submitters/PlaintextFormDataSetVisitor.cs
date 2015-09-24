namespace AngleSharp.Html.Submitters
{
    using AngleSharp.Dom.Io;
    using System;
    using System.Collections.Generic;
    using System.IO;

    sealed class PlaintextFormDataSetVisitor : IFormSubmitter
    {
        readonly List<String> _lines;

        public PlaintextFormDataSetVisitor()
        {
            _lines = new List<String>();
        }

        public void Text(FormDataSetEntry entry, String value)
        {
            if (entry.HasName && value != null)
                Add(entry.Name, value);
        }

        public void File(FormDataSetEntry entry, String fileName, String contentType, IFile content)
        {
            if (entry.HasName && content != null && content.Name != null)
                Add(entry.Name, content.Name);
        }

        void Add(String name, String value)
        {
            _lines.Add(String.Concat(name, "=", value));
        }

        public void Serialize(StreamWriter stream)
        {
            var content = String.Join("\r\n", _lines);
            stream.Write(content);
        }
    }
}

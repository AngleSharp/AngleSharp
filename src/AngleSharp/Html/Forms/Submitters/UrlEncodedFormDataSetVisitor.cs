namespace AngleSharp.Html.Forms.Submitters
{
    using AngleSharp.Dom;
    using AngleSharp.Io.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    sealed class UrlEncodedFormDataSetVisitor : IFormSubmitter
    {
        #region Fields

        private readonly Encoding _encoding;
        private readonly List<String> _lines;
        private Boolean _first;
        private String _index;

        #endregion

        #region ctor

        public UrlEncodedFormDataSetVisitor(Encoding encoding)
        {
            _encoding = encoding;
            _lines = new List<String>();
            _first = true;
            _index = String.Empty;
        }

        #endregion

        #region Methods

        public void Text(FormDataSetEntry entry, String value)
        {
            if (_first && entry.HasName && entry.Name.Is(TagNames.IsIndex) && entry.Type.Isi(InputTypeNames.Text))
            {
                _index = value ?? String.Empty;
            }
            else if (entry.HasName && value != null)
            {
                var k = _encoding.GetBytes(entry.Name);
                var v = _encoding.GetBytes(value);
                Add(k, v);
            }

            _first = false;
        }

        public void File(FormDataSetEntry entry, String fileName, String contentType, IFile content)
        {
            if (entry.HasName && content?.Name != null)
            {
                var k = _encoding.GetBytes(entry.Name);
                var v = _encoding.GetBytes(content.Name);
                Add(k, v);
            }

            _first = false;
        }

        public void Serialize(StreamWriter stream)
        {
            var content = String.Join("&", _lines);
            stream.Write(_index);
            stream.Write(content);
        }

        #endregion

        #region Helpers

        private void Add(Byte[] name, Byte[] value)
        {
            _lines.Add(String.Concat(name.UrlEncode(), "=", value.UrlEncode()));
        }

        #endregion
    }
}

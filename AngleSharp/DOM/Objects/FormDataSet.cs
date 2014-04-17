namespace AngleSharp.DOM
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Bundles information stored in HTML forms.
    /// </summary>
    sealed class FormDataSet : IEnumerable<String>
    {
        #region Fields

        String _boundary;
        List<FormDataSetEntry> _entries;

        #endregion

        #region ctor

        public FormDataSet()
        {
            _boundary = Guid.NewGuid().ToString();
            _entries = new List<FormDataSetEntry>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the chosen boundary.
        /// </summary>
        public String Boundary
        {
            get { return _boundary; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Applies the multipart/form-data algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#multipart/form-data-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsMultipart(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);

            using (var tw = new StreamWriter(ms, encoding))
            {
                tw.WriteLine();

                foreach (var entry in _entries)
                {
                    tw.Write("--");
                    tw.WriteLine(_boundary);
                    entry.AsMultipart(tw);
                }

                tw.Write("--");
                tw.Write(_boundary);
                tw.Write("--");
            }

            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Applies the urlencoded algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsUrlEncoded(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);

            using (var tw = new StreamWriter(ms, encoding))
            {
                if (_entries.Count > 0)
                {
                    if (_entries[0].Name.Equals("isindex") && _entries[0].Type.Equals("text", StringComparison.OrdinalIgnoreCase))
                        tw.Write(((TextDataSetEntry)_entries[0]).Value);
                    else
                        _entries[0].AsUrlEncoded(tw);

                    for (int i = 1; i < _entries.Count; i++)
                    {
                        tw.Write('&');
                        _entries[i].AsUrlEncoded(tw);
                    }
                }
            }

            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Applies the plain encoding algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#text/plain-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsPlaintext(Encoding encoding = null)
        {
            encoding = encoding ?? Encoding.UTF8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);

            using (var tw = new StreamWriter(ms, encoding))
            {
                tw.WriteLine();

                foreach (var entry in _entries)
                {
                    entry.AsPlaintext(tw);
                    tw.Write("\r\n");
                }
            }

            ms.Position = 0;
            return ms;
        }

        public void Append(String name, String value, String type)
        {
            if (String.Compare(type, "textarea", StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
                value = Normalize(value);
            }

            _entries.Add(new TextDataSetEntry { Name = name, Value = value, Type = type });
        }

        public void Append(String name, FileEntry value, String type)
        {
            if (String.Compare(type, "file", StringComparison.OrdinalIgnoreCase) == 0)
                name = Normalize(name);

            _entries.Add(new FileDataSetEntry { Name = name, Value = value, Type = type });
        }

        #endregion

        #region Helpers

        void ReplaceCharset(Encoding encoding)
        {
            for (int i = 0; i < _entries.Count; i++ )
            {
                var entry = _entries[i];

                if (entry.Name.Equals("_charset_") && entry.Type.Equals("hidden", StringComparison.OrdinalIgnoreCase))
                    _entries[i] = new TextDataSetEntry { Name = entry.Name, Type = entry.Type, Value = encoding.WebName };
            }
        }

        void CheckBoundaries(Encoding encoding)
        {
            var found = false;

            do
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    if (_entries[i].Name.Contains(_boundary))
                    { }
                }

                //TODO
                //Check if there is any collision with the boundary string - if there is:
                //Re-Generate Boundary String until there are no collisions with any value
            } while (found);
        }

        /// <summary>
        /// Replaces every occurrence of a "CR" (U+000D) character not followed by a "LF" (U+000A)
        /// character, and every occurrence of a "LF" (U+000A) character not preceded by a "CR"
        /// (U+000D) character, by a two-character string consisting of a U+000D CARRIAGE RETURN
        /// "CRLF" (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        static String Normalize(String value)
        {
            var lines = value.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            return String.Join("\r\n", lines);
        }

        #endregion

        #region Entry Class

        /// <summary>
        /// Encapsulates the data contained in an entry.
        /// </summary>
        abstract class FormDataSetEntry
        {
            protected String _name;
            protected String _type;

            /// <summary>
            /// Gets or sets the entry's name.
            /// </summary>
            public String Name
            {
                get { return _name; }
                set { _name = value; }
            }

            /// <summary>
            /// Gets or sets the entry's type.
            /// </summary>
            public String Type
            {
                get { return _type; }
                set { _type = value; }
            }

            /// <summary>
            /// Replaces characters in names and values that cannot be expressed by using the given
            /// encoding with &#...; base-10 unicode point.
            /// </summary>
            /// <param name="value">The value to sanatize.</param>
            /// <param name="encoding">The encoding to consider.</param>
            /// <returns>The sanatized value.</returns>
            protected static String Sanatize(String value, Encoding encoding)
            {
                return value;
            }

            public abstract void AsMultipart(StreamWriter stream);

            public abstract void AsPlaintext(StreamWriter stream);

            public abstract void AsUrlEncoded(StreamWriter stream);
        }

        sealed class TextDataSetEntry : FormDataSetEntry
        {
            String _value;

            /// <summary>
            /// Gets or sets the entry's value.
            /// </summary>
            public String Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public override void AsMultipart(StreamWriter stream)
            {
                stream.WriteLine(String.Concat("content-disposition: form-data; name=\"", Sanatize(_name, stream.Encoding), "\""));
                stream.WriteLine();
                stream.WriteLine(Sanatize(_value, stream.Encoding));
            }

            public override void AsPlaintext(StreamWriter stream)
            {
                stream.Write(_name);
                stream.Write('=');
                stream.Write(_value);
            }

            public override void AsUrlEncoded(StreamWriter stream)
            {
                stream.Write(_name);
                stream.Write('=');
                //TODO
                throw new NotImplementedException();
            }
        }

        sealed class FileDataSetEntry : FormDataSetEntry
        {
            FileEntry _value;

            /// <summary>
            /// Gets or sets the entry's value.
            /// </summary>
            public FileEntry Value
            {
                get { return _value; }
                set { _value = value; }
            }

            public override void AsMultipart(StreamWriter stream)
            {
                stream.WriteLine("content-disposition: form-data; name=\"{0}\"; filename=\"{1}\"", Sanatize(_name, stream.Encoding), _value.FileName);
                stream.WriteLine("content-type: " + _value.Type);
                stream.WriteLine("content-transfer-encoding: binary");
                stream.WriteLine();
                stream.BaseStream.Write(_value.Body, 0, _value.Body.Length);
                stream.WriteLine();
            }

            public override void AsPlaintext(StreamWriter stream)
            {
                stream.Write(_name);
                stream.Write('=');
                stream.Write(_value.FileName);
            }

            public override void AsUrlEncoded(StreamWriter stream)
            {
                stream.Write(_name);
                stream.Write('=');
                //TODO
                throw new NotImplementedException();
            }
        }

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over all entry names.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public IEnumerator<String> GetEnumerator()
        {
            return _entries.Select(m => m.Name).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

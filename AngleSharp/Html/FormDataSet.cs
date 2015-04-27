namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
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

        readonly List<FormDataSetEntry> _entries;
        String _boundary;

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
            encoding = encoding ?? TextEncoding.Utf8;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
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

            tw.Flush();
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
            encoding = encoding ?? TextEncoding.Utf8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
            var offset = 0;
            var requireAmpersand = false;

            if (offset < _entries.Count && 
                _entries[offset].HasName &&
                _entries[offset].Name.Equals(Tags.IsIndex) &&
                _entries[offset].Type.Equals(InputTypeNames.Text, StringComparison.OrdinalIgnoreCase))
            {
                tw.Write(((TextDataSetEntry)_entries[offset]).Value);
                offset++;
            }

            while (offset < _entries.Count)
            {
                if (_entries[offset].HasName)
                {
                    if (requireAmpersand)
                        tw.Write('&');

                    _entries[offset].AsUrlEncoded(tw);
                    requireAmpersand = true;
                }

                offset++;
            }

            tw.Flush();
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
            encoding = encoding ?? TextEncoding.Utf8;
            var charset = encoding.WebName;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
            tw.WriteLine();

            foreach (var entry in _entries)
            {
                entry.AsPlaintext(tw);
                tw.Write("\r\n");
            }

            tw.Flush();
            ms.Position = 0;
            return ms;
        }

        public void Append(String name, String value, String type)
        {
            if (String.Compare(type, Tags.Textarea, StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
                value = Normalize(value);
            }

            _entries.Add(new TextDataSetEntry(name, value, type));
        }

        public void Append(String name, IFile value, String type)
        {
            if (String.Compare(type, InputTypeNames.File, StringComparison.OrdinalIgnoreCase) == 0)
            {
                name = Normalize(name);
            }

            _entries.Add(new FileDataSetEntry(name, value, type));
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Replaces a charset field (if any) that is hidden with the given character encoding.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        void ReplaceCharset(Encoding encoding)
        {
            for (int i = 0; i < _entries.Count; i++ )
            {
                var entry = _entries[i];

                if (!String.IsNullOrEmpty(entry.Name) && entry.Name.Equals("_charset_") && 
                    entry.Type.Equals(InputTypeNames.Hidden, StringComparison.OrdinalIgnoreCase))
                {
                    _entries[i] = new TextDataSetEntry(entry.Name, encoding.WebName, entry.Type);
                }
            }
        }

        /// <summary>
        /// Checks the entries for boundary collisions. If a collision is detected, then a new
        /// boundary string is generated. This algorithm will produce a boundary string that
        /// satisfies all requirements.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        void CheckBoundaries(Encoding encoding)
        {
            var found = false;

            do
            {
                for (int i = 0; i < _entries.Count; i++)
                {
                    if (found = _entries[i].Contains(_boundary, encoding))
                    {
                        _boundary = Guid.NewGuid().ToString();
                        break;
                    }
                }
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
            readonly String _name;
            readonly String _type;

            public FormDataSetEntry(String name, String type)
            {
                _name = name;
                _type = type;
            }

            /// <summary>
            /// Gets if the name has been given.
            /// </summary>
            public Boolean HasName
            {
                get { return _name != null; }
            }

            /// <summary>
            /// Gets the entry's name.
            /// </summary>
            public String Name
            {
                get { return _name ?? String.Empty; }
            }

            /// <summary>
            /// Gets the entry's type.
            /// </summary>
            public String Type
            {
                get { return _type ?? InputTypeNames.Text; }
            }

            public abstract void AsMultipart(StreamWriter stream);

            public abstract void AsPlaintext(StreamWriter stream);

            public abstract void AsUrlEncoded(StreamWriter stream);

            public abstract Boolean Contains(String boundary, Encoding encoding);
        }

        sealed class TextDataSetEntry : FormDataSetEntry
        {
            readonly String _value;

            public TextDataSetEntry(String name, String value, String type)
                : base(name, type)
            {
                _value = value;
            }

            /// <summary>
            /// Gets if the value has been given.
            /// </summary>
            public Boolean HasValue
            {
                get { return _value != null; }
            }

            /// <summary>
            /// Gets the entry's value.
            /// </summary>
            public String Value
            {
                get { return _value; }
            }

            public override Boolean Contains(String boundary, Encoding encoding)
            {
                if (_value == null)
                    return false;

                return _value.Contains(boundary);
            }

            public override void AsMultipart(StreamWriter stream)
            {
                if (HasName && HasValue)
                {
                    stream.WriteLine(String.Concat("content-disposition: form-data; name=\"", 
                        Name.HtmlEncode(stream.Encoding), "\""));
                    stream.WriteLine();
                    stream.WriteLine(_value.HtmlEncode(stream.Encoding));
                }
            }

            public override void AsPlaintext(StreamWriter stream)
            {
                if (HasName && HasValue)
                {
                    stream.Write(Name);
                    stream.Write('=');
                    stream.Write(_value);
                }
            }

            public override void AsUrlEncoded(StreamWriter stream)
            {
                if (HasName && HasValue)
                {
                    stream.Write(Name.UrlEncode(stream.Encoding));
                    stream.Write('=');
                    stream.Write(_value.UrlEncode(stream.Encoding));
                }
            }
        }

        sealed class FileDataSetEntry : FormDataSetEntry
        {
            readonly IFile _value;

            public FileDataSetEntry(String name, IFile value, String type)
                : base(name, type)
            {
                _value = value;
            }

            /// <summary>
            /// Gets if the value has been given.
            /// </summary>
            public Boolean HasValue
            {
                get { return _value != null && _value.Name != null; }
            }

            /// <summary>
            /// Gets if the value has a body and type.
            /// </summary>
            public Boolean HasValueBody
            {
                get { return _value != null && _value.Body != null && _value.Type != null; }
            }

            /// <summary>
            /// Gets the entry's value.
            /// </summary>
            public IFile Value
            {
                get { return _value; }
            }

            public override Boolean Contains(String boundary, Encoding encoding)
            {
                if (_value == null || _value.Body == null)
                    return false;
                
                //TODO boundary check required?
                return false;
            }

            public override void AsMultipart(StreamWriter stream)
            {
                if (HasName && HasValue && HasValueBody)
                {
                    stream.WriteLine("content-disposition: form-data; name=\"{0}\"; filename=\"{1}\"", 
                        Name.HtmlEncode(stream.Encoding), _value.Name.HtmlEncode(stream.Encoding));
                    stream.WriteLine("content-type: " + _value.Type);
                    stream.WriteLine("content-transfer-encoding: binary");
                    stream.WriteLine();
                    stream.Flush();
                    _value.Body.CopyTo(stream.BaseStream);
                    stream.WriteLine();
                }
            }

            public override void AsPlaintext(StreamWriter stream)
            {
                if (HasName && HasValue)
                {
                    stream.Write(Name);
                    stream.Write('=');
                    stream.Write(_value.Name);
                }
            }

            public override void AsUrlEncoded(StreamWriter stream)
            {
                if (HasName && HasValue)
                {
                    stream.Write(Name.UrlEncode(stream.Encoding));
                    stream.Write('=');
                    stream.Write(_value.Name.UrlEncode(stream.Encoding));
                }
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

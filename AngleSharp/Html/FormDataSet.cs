namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Html.Json;
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

        static readonly String[] NewLines = new[] { "\r\n", "\r", "\n" };

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
            return Build(encoding, stream =>
            {
                var enc = stream.Encoding;
                var entryWriters = _entries.Select(m => m.AsMultipart(enc)).
                                            Where(m => m != null);

                foreach (var entryWriter in entryWriters)
                {
                    stream.Write("--");
                    stream.WriteLine(_boundary);
                    entryWriter(stream);
                }

                stream.Write("--");
                stream.Write(_boundary);
                stream.Write("--");
            });
        }

        /// <summary>
        /// Applies the urlencoded algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsUrlEncoded(Encoding encoding = null)
        {
            return Build(encoding, stream =>
            {
                var offset = 0;
                var enc = stream.Encoding;

                if (offset < _entries.Count &&
                    _entries[offset].HasName &&
                    _entries[offset].Name.Equals(Tags.IsIndex) &&
                    _entries[offset].Type.Equals(InputTypeNames.Text, StringComparison.OrdinalIgnoreCase))
                {
                    stream.Write(((TextDataSetEntry)_entries[offset]).Value);
                    offset++;
                }

                var list = _entries.Skip(offset).
                                    Select(m => m.AsUrlEncoded(enc)).
                                    Where(m => m != null).
                                    ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    if (i > 0)
                        stream.Write('&');

                    stream.Write(list[i].Item1);
                    stream.Write('=');
                    stream.Write(list[i].Item2);
                }
            });
        }

        /// <summary>
        /// Applies the plain encoding algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#text/plain-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsPlaintext(Encoding encoding = null)
        {
            return Build(encoding, stream =>
            {
                var list = _entries.Select(m => m.AsPlaintext()).
                                    Where(m => m != null).
                                    ToArray();

                for (int i = 0; i < list.Length; i++)
                {
                    if (i > 0)
                        stream.Write("\r\n");

                    stream.Write(list[i].Item1);
                    stream.Write('=');
                    stream.Write(list[i].Item2);
                }
            });
        }

        /// <summary>
        /// Applies the application json encoding algorithm.
        /// https://darobin.github.io/formic/specs/json/#the-application-json-encoding-algorithm
        /// </summary>
        /// <returns>A stream containing the body.</returns>
        public Stream AsJson()
        {
            //spec dictates utf8
            return Build(TextEncoding.Utf8, stream =>
            {
                var resultingObject = new JsonObject();

                foreach (var entry in _entries)
                {
                    entry.AsJson(resultingObject);
                }

                stream.Write(resultingObject.ToString());
            });
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
        /// Builds the specific request body / url.
        /// </summary>
        /// <param name="encoding">The encoding to use.</param>
        /// <param name="process">The action to generate the content.</param>
        /// <returns>The constructed stream.</returns>
        Stream Build(Encoding encoding, Action<StreamWriter> process)
        {
            encoding = encoding ?? TextEncoding.Utf8;
            var ms = new MemoryStream();
            CheckBoundaries(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
            process(tw);
            tw.Flush();
            ms.Position = 0;
            return ms;
        }

        /// <summary>
        /// Replaces a charset field (if any) that is hidden with the given
        /// character encoding.
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
        /// Checks the entries for boundary collisions. If a collision is
        /// detected, then a new boundary string is generated. This algorithm
        /// will produce a boundary string that satisfies all requirements.
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
        /// Replaces every occurrence of a "CR" (U+000D) character not followed
        /// by a "LF" (U+000A) character, and every occurrence of a "LF"
        /// (U+000A) character not preceded by a "CR" (U+000D) character, by a
        /// two-character string consisting of a U+000D CARRIAGE RETURN "CRLF"
        /// (U+000A) character pair.
        /// </summary>
        /// <param name="value">The value to normalize.</param>
        /// <returns>The normalized string.</returns>
        static String Normalize(String value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                var lines = value.Split(NewLines, StringSplitOptions.None);
                return String.Join("\r\n", lines);
            }

            return value;
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

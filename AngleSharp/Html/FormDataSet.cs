namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Network;
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

                if (offset < _entries.Count && _entries[offset].HasName && _entries[offset].Name.Is(Tags.IsIndex) && _entries[offset].Type.Isi(InputTypeNames.Text))
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
                    {
                        stream.Write(Symbols.Ampersand);
                    }

                    stream.Write(list[i].Item1);
                    stream.Write(Symbols.Equality);
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
                    {
                        stream.Write(Symbols.NewLines[0]);
                    }

                    stream.Write(list[i].Item1);
                    stream.Write(Symbols.Equality);
                    stream.Write(list[i].Item2);
                }
            });
        }

        public void Append(String name, String value, String type)
        {
            if (type.Isi(Tags.Textarea))
            {
                name = Normalize(name);
                value = Normalize(value);
            }

            _entries.Add(new TextDataSetEntry(name, value, type));
        }

        public void Append(String name, IFile value, String type)
        {
            if (type.Isi(InputTypeNames.File))
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

                if (!String.IsNullOrEmpty(entry.Name) && entry.Name.Is("_charset_") && entry.Type.Isi(InputTypeNames.Hidden))
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
                var lines = value.Split(Symbols.NewLines, StringSplitOptions.None);
                return String.Join(Symbols.NewLines[0], lines);
            }

            return value;
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

            public abstract Action<StreamWriter> AsMultipart(Encoding encoding);

            public abstract Tuple<String, String> AsPlaintext();

            public abstract Tuple<String, String> AsUrlEncoded(Encoding encoding);

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

            public override Action<StreamWriter> AsMultipart(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    return stream =>
                    {
                        stream.WriteLine(String.Concat("Content-Disposition: form-data; name=\"",
                            Name.HtmlEncode(encoding), "\""));
                        stream.WriteLine();
                        stream.WriteLine(_value.HtmlEncode(encoding));
                    };
                }

                return null;
            }

            public override Tuple<String, String> AsPlaintext()
            {
                if (HasName && HasValue)
                    return Tuple.Create(Name, _value);

                return null;
            }

            public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    var name = encoding.GetBytes(Name);
                    var value = encoding.GetBytes(_value);
                    return Tuple.Create(name.UrlEncode(), value.UrlEncode());
                }

                return null;
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

            public String FileName
            {
                get { return _value != null ? _value.Name : String.Empty; }
            }

            public String ContentType
            {
                get { return _value != null ? _value.Type : MimeTypes.Binary; }
            }

            public override Boolean Contains(String boundary, Encoding encoding)
            {
                if (_value == null || _value.Body == null)
                    return false;
                
                //TODO boundary check required?
                return false;
            }

            public override Action<StreamWriter> AsMultipart(Encoding encoding)
            {
                if (HasName)
                {
                    return stream =>
                    {
                        var hasContent = HasValue && HasValueBody;

                        stream.WriteLine("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"",
                            Name.HtmlEncode(encoding), FileName.HtmlEncode(encoding));

                        stream.WriteLine("Content-Type: " + ContentType);
                        stream.WriteLine();

                        if (hasContent)
                        {
                            stream.Flush();
                            _value.Body.CopyTo(stream.BaseStream);
                        }

                        stream.WriteLine();
                    };
                }

                return null;
            }

            public override Tuple<String, String> AsPlaintext()
            {
                if (HasName && HasValue)
                    return Tuple.Create(Name, _value.Name);

                return null;
            }

            public override Tuple<String, String> AsUrlEncoded(Encoding encoding)
            {
                if (HasName && HasValue)
                {
                    var name = encoding.GetBytes(Name);
                    var value = encoding.GetBytes(_value.Name);
                    return Tuple.Create(name.UrlEncode(), value.UrlEncode());
                }

                return null;
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

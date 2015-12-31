namespace AngleSharp.Html
{
    using AngleSharp.Dom.Io;
    using AngleSharp.Extensions;
    using AngleSharp.Html.Submitters;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// Bundles information stored in HTML forms.
    /// </summary>
    public sealed class FormDataSet : IEnumerable<String>
    {
        #region Fields

        readonly List<FormDataSetEntry> _entries;
        String _boundary;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new form data set with a randomly generated boundary.
        /// </summary>
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
            return BuildRequestContent(encoding, stream => Connect(new MultipartFormDataSetVisitor(stream.Encoding, _boundary), stream));
        }

        /// <summary>
        /// Applies the urlencoded algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#application/x-www-form-urlencoded-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsUrlEncoded(Encoding encoding = null)
        {
            return BuildRequestContent(encoding, stream => Connect(new UrlEncodedFormDataSetVisitor(stream.Encoding), stream));
        }

        /// <summary>
        /// Applies the plain encoding algorithm.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#text/plain-encoding-algorithm
        /// </summary>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream AsPlaintext(Encoding encoding = null)
        {
            return BuildRequestContent(encoding, stream => Connect(new PlaintextFormDataSetVisitor(), stream));
        }

        /// <summary>
        /// Applies the application json encoding algorithm.
        /// https://darobin.github.io/formic/specs/json/#the-application-json-encoding-algorithm
        /// </summary>
        /// <returns>A stream containing the body.</returns>
        public Stream AsJson()
        {
            return BuildRequestContent(TextEncoding.Utf8, stream => Connect(new JsonFormDataSetVisitor(), stream));
        }

        /// <summary>
        /// Applies the given submitter to serialize the form data set.
        /// </summary>
        /// <param name="submitter">The algorithm to use.</param>
        /// <param name="encoding">(Optional) Explicit encoding.</param>
        /// <returns>A stream containing the body.</returns>
        public Stream As(IFormSubmitter submitter, Encoding encoding = null)
        {
            return BuildRequestContent(encoding, stream => Connect(submitter, stream));
        }

        /// <summary>
        /// Appends a text entry to the form data set.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="value">The value of the entry.</param>
        /// <param name="type">The type of the entry.</param>
        public void Append(String name, String value, String type)
        {
            if (type.Isi(TagNames.Textarea))
            {
                name = name.NormalizeLineEndings();
                value = value.NormalizeLineEndings();
            }

            _entries.Add(new TextDataSetEntry(name, value, type));
        }

        /// <summary>
        /// Appends a file entry to the form data set.
        /// </summary>
        /// <param name="name">The name of the entry.</param>
        /// <param name="value">The value of the entry.</param>
        /// <param name="type">The type of the entry.</param>
        public void Append(String name, IFile value, String type)
        {
            if (type.Isi(InputTypeNames.File))
            {
                name = name.NormalizeLineEndings();
            }

            _entries.Add(new FileDataSetEntry(name, value, type));
        }

        #endregion

        #region Helpers

        Stream BuildRequestContent(Encoding encoding, Action<StreamWriter> process)
        {
            encoding = encoding ?? TextEncoding.Utf8;
            var ms = new MemoryStream();
            FixPotentialBoundaryCollisions(encoding);
            ReplaceCharset(encoding);
            var tw = new StreamWriter(ms, encoding);
            process(tw);
            tw.Flush();
            ms.Position = 0;
            return ms;
        }

        void Connect(IFormSubmitter submitter, StreamWriter stream)
        {
            foreach (var entry in _entries)
            {
                entry.Accept(submitter);
            }

            submitter.Serialize(stream);
        }

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

        void FixPotentialBoundaryCollisions(Encoding encoding)
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

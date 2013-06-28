using System;
using System.Diagnostics;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Performs the tokenization of the source code.
    /// </summary>
    [DebuggerStepThrough]
    class XmlTokenizer
    {
        #region Members

        SourceManager src;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new tokenizer for XML documents.
        /// </summary>
        /// <param name="source">The source code manager.</param>
        public XmlTokenizer(SourceManager source)
        {
            src = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the underlying stream.
        /// </summary>
        public SourceManager Stream
        {
            get { return src; }
        }

        #endregion
    }
}

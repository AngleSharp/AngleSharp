namespace AngleSharp
{
    using System.Diagnostics;

    /// <summary>
    /// The positional range in the source code.
    /// </summary>
    [DebuggerStepThrough]
    struct TextRange
    {
        #region Fields

        readonly TextPosition _start;
        readonly TextPosition _end;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new text range.
        /// </summary>
        /// <param name="start">The start of the range.</param>
        /// <param name="end">The end of the range.</param>
        public TextRange(TextPosition start, TextPosition end)
        {
            _start = start;
            _end = end;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start position of the range.
        /// </summary>
        public TextPosition Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Gets the end position of the range.
        /// </summary>
        public TextPosition End
        {
            get { return _end; }
        }

        #endregion
    }
}

namespace AngleSharp
{
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Represents a view on a particular source code.
    /// </summary>
    [DebuggerStepThrough]
    public class TextView
    {
        #region Fields

        readonly TextRange _range;
        readonly TextSource _source;

        #endregion

        #region ctor

        internal TextView(TextRange range, TextSource source)
        {
            _range = range;
            _source = source;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start and end of the text view.
        /// </summary>
        public TextRange Range
        {
            get { return _range; }
        }

        /// <summary>
        /// Gets the text associated with this view.
        /// </summary>
        public String Text
        {
            get { return _source.Text.Substring(_range.Start.Position, _range.End.Position - _range.Start.Position); }
        }

        #endregion
    }
}

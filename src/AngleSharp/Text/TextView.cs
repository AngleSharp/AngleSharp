namespace AngleSharp.Text
{
    using System;

    /// <summary>
    /// Represents a view on a particular source code.
    /// </summary>
    public class TextView
    {
        #region Fields

        private readonly TextSource _source;
        private readonly TextRange _range;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new text view for the given range.
        /// </summary>
        public TextView(TextSource source, TextRange range)
        {
            _source = source;
            _range = range;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the start and end of the text view.
        /// </summary>
        public TextRange Range => _range;

        /// <summary>
        /// Gets the text associated with this view.
        /// </summary>
        public String Text
        {
            get 
            {
                var start = Math.Max(_range.Start.Position - 1, 0);
                var length = _range.End.Position + 1 - _range.Start.Position;
                var text = _source.Text;

                if (start + length > text.Length)
                {
                    length = text.Length - start;
                }

                return text.Substring(start, length);
            }
        }

        #endregion
    }
}

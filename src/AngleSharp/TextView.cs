namespace AngleSharp
{
    using System;

    /// <summary>
    /// Represents a view on a particular source code.
    /// </summary>
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

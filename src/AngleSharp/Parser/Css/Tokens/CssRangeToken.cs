namespace AngleSharp.Parser.Css
{
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    /// <summary>
    /// Represents the CSS range token.
    /// </summary>
    sealed class CssRangeToken : CssToken
    {
        #region Fields

        readonly String[] _range;
        readonly String _start;
        readonly String _end;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS range token.
        /// </summary>
        /// <param name="range">The selected range string.</param>
        /// <param name="position">The token's position.</param>
        public CssRangeToken(String range, TextPosition position)
            : base(CssTokenType.Range, range, position)
        {
            _start = range.Replace(Symbols.QuestionMark, '0');
            _end = range.Replace(Symbols.QuestionMark, 'F');
            _range = GetRange();
        }

        /// <summary>
        /// Creates a new CSS range token.
        /// </summary>
        /// <param name="start">The selected range's start.</param>
        /// <param name="end">The selected range's end.</param>
        /// <param name="position">The token's position.</param>
        public CssRangeToken(String start, String end, TextPosition position)
            : base(CssTokenType.Range, String.Concat(start, "-", end), position)
        {
            _start = start;
            _end = end;
            _range = GetRange();

        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the status of the range.
        /// </summary>
        public Boolean IsEmpty
        {
            get { return _range == null || _range.Length == 0; }
        }

        /// <summary>
        /// Gets the range's start.
        /// </summary>
        public String Start
        {
            get { return _start; }
        }

        /// <summary>
        /// Gets the range's end.
        /// </summary>
        public String End
        {
            get { return _end; }
        }

        /// <summary>
        /// Gets the content of the range token.
        /// </summary>
        public String[] SelectedRange
        {
            get { return _range; }
        }

        #endregion

        #region Helpers

        String[] GetRange()
        {
            var index = Int32.Parse(_start, NumberStyles.HexNumber);

            if (index <= Symbols.MaximumCodepoint)
            {
                if (_end != null)
                {
                    var list = new List<String>();
                    var f = Int32.Parse(_end, NumberStyles.HexNumber);

                    if (f > Symbols.MaximumCodepoint)
                    {
                        f = Symbols.MaximumCodepoint;
                    }

                    while (index <= f)
                    {
                        list.Add(index.ConvertFromUtf32());
                        index++;
                    }

                    return list.ToArray();
                }

                return new String[] { index.ConvertFromUtf32() };
            }

            return null;
        }

        #endregion
    }
}

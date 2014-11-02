namespace AngleSharp.Parser.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Represents the CSS range token.
    /// </summary>
    sealed class CssRangeToken : CssToken
    {
        #region Fields

        readonly String[] _range;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new CSS range token.
        /// </summary>
        /// <param name="start">The (hex-)string where to begin.</param>
        /// <param name="end">The (hex-)string where to end.</param>
        public CssRangeToken(String start, String end)
        {
            _type = CssTokenType.Range;
            var index = Int32.Parse(start, System.Globalization.NumberStyles.HexNumber);

            if (index <= Specification.MaximumCodepoint)
            {
                if (end != null)
                {
                    var list = new List<String>();
                    var f = Int32.Parse(end, System.Globalization.NumberStyles.HexNumber);

                    if (f > Specification.MaximumCodepoint)
                        f = Specification.MaximumCodepoint;

                    for (; index <= f; index++)
                        list.Add(Char.ConvertFromUtf32(index));

                    _range = list.ToArray();
                }
                else
                    _range = new String[] { Char.ConvertFromUtf32(index) };
            }
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
        /// Gets the content of the range token.
        /// </summary>
        public String[] SelectedRange
        {
            get { return _range; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override String ToValue()
        {
            if (IsEmpty)
                return string.Empty;

            if (_range.Length == 1)
                return "#" + char.ConvertToUtf32(_range[0], 0).ToString("x");

            return "#" + char.ConvertToUtf32(_range[0], 0).ToString("x") + "-#" + char.ConvertToUtf32(_range[_range.Length - 1], 0).ToString("x");
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents the CSS range token.
    /// </summary>
    sealed class CssRangeToken : CssToken
    {
        string[] _range;

        /// <summary>
        /// Creates a new CSS range token.
        /// </summary>
        public CssRangeToken()
        {
            _type = CssTokenType.Range;
        }

        /// <summary>
        /// Sets the range in the token.
        /// </summary>
        /// <param name="start">The (hex-)string where to begin.</param>
        /// <param name="end">The (hex-)string where to end.</param>
        /// <returns>The token itself.</returns>
        public CssRangeToken SetRange(string start, string end)
        {
            var i = int.Parse(start, System.Globalization.NumberStyles.HexNumber);

            if (i <= Specification.MAXIMUM_CODEPOINT)
            {
                if (end == null)
                {
                    _range = new string[] { char.ConvertFromUtf32(i) };
                }
                else
                {
                    var list = new List<string>();
                    var f = int.Parse(end, System.Globalization.NumberStyles.HexNumber);

                    if (f > Specification.MAXIMUM_CODEPOINT)
                        f = Specification.MAXIMUM_CODEPOINT;

                    for (; i <= f; i++)
                        list.Add(char.ConvertFromUtf32(i));

                    _range = list.ToArray();
                }
            }

            return this;
        }

        /// <summary>
        /// Gets the status of the range.
        /// </summary>
        public bool IsEmpty
        {
            get { return _range == null || _range.Length == 0; }
        }

        /// <summary>
        /// Gets the content of the range token.
        /// </summary>
        public string[] Range
        {
            get { return _range; }
        }

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            if (IsEmpty)
                return string.Empty;

            if (_range.Length == 1)
                return "#" + char.ConvertToUtf32(_range[0], 0).ToString("x");

            return "#" + char.ConvertToUtf32(_range[0], 0).ToString("x") + "-#" + char.ConvertToUtf32(_range[_range.Length - 1], 0).ToString("x");
        }
    }
}

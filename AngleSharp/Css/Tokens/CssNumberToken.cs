using System;

namespace AngleSharp.Css
{
    /// <summary>
    /// Represents a CSS number token.
    /// </summary>
    class CssNumberToken : CssToken
    {
        float _data;

        /// <summary>
        /// Creates a new CSS number token.
        /// </summary>
        /// <param name="number">The number to contain.</param>
        public CssNumberToken(float number)
        {
            _type = CssTokenType.Number;
            _data = number;
        }

        /// <summary>
        /// Gets the contained number.
        /// </summary>
        public float Data
        {
            get { return _data; }
        }

        /// <summary>
        /// Gets a string which represents the original value.
        /// </summary>
        /// <returns>The original value.</returns>
        public override string ToValue()
        {
            return _data.ToString();
        }
    }
}

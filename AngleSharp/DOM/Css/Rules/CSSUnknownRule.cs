using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an unknown rule.
    /// </summary>
    sealed class CSSUnknownRule : CSSRule
    {
        String _text;

        /// <summary>
        /// Sets the textual representation.
        /// </summary>
        /// <param name="text">The test to set.</param>
        internal void SetText(String text)
        {
            _text = text;
        }

        /// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return _text;
        }
    }
}

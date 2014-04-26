namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an unknown rule.
    /// </summary>
	sealed class CSSUnknownRule : CSSGroupingRule
	{
		#region Fields

		String _text;

		#endregion

		#region Internal Methods

		/// <summary>
        /// Sets the textual representation of this unknown rule.
        /// </summary>
        /// <param name="text">The test to set.</param>
        internal void SetText(String text)
        {
            _text = text;
        }

		#endregion

		#region String Representation

		/// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
			return _text;
		}

		#endregion
	}
}

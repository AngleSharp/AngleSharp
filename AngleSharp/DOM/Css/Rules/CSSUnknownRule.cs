using System;

namespace AngleSharp.DOM.Css
{
    /// <summary>
    /// Represents an unknown rule.
    /// </summary>
	sealed class CSSUnknownRule : CSSGroupingRule
	{
		#region Members

		Boolean _stopped;
		String _text;

		#endregion

		#region Internal Methods

		/// <summary>
        /// Sets the textual representation for rules with
		/// no sub-rules (instruction - stopped by ;).
        /// </summary>
        /// <param name="text">The test to set.</param>
        internal void SetInstruction(String text)
        {
            _text = text;
			_stopped = true;
        }

		/// <summary>
		/// Sets the textual representation for rules with
		/// sub-rules (curly brackets).
		/// </summary>
		/// <param name="text">The test to set.</param>
		internal void SetCondition(String text)
		{
			_text = text;
			_stopped = false;
		}

		#endregion

		#region String Representation

		/// <summary>
        /// Returns a CSS code representation of the rule.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
			if (_stopped)
				return _text + ";";
			
			return _text + "{" + CssRules.ToCss() + "}";
		}

		#endregion
	}
}

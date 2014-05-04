namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a rule that has been created using the only prefix.
    /// </summary>
    sealed class CSSOnlyMedium : CSSMedium
    {
        internal CSSOnlyMedium()
        { }

        /// <summary>
        /// Returns a CSS code representation of the medium.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat("only ", base.ToCss());
        }
    }
}

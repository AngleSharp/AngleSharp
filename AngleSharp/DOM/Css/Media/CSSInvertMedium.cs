namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents a rule that has been created using the not prefix.
    /// It therefore inverts the validation of the constraints specified
    /// after the not keyword.
    /// </summary>
    sealed class CSSInvertMedium : CSSMedium
    {
        internal CSSInvertMedium()
        { }

        /// <summary>
        /// Validates the given medium.
        /// </summary>
        /// <returns>True if the constraints are satisfied, otherwise false.</returns>
        public override Boolean Validate()
        {
            return !base.Validate();
        }

        /// <summary>
        /// Returns a CSS code representation of the medium.
        /// </summary>
        /// <returns>A string that contains the code.</returns>
        public override String ToCss()
        {
            return String.Concat("not ", base.ToCss());
        }
    }
}

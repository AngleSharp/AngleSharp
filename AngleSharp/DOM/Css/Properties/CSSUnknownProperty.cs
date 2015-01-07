namespace AngleSharp.DOM.Css
{
    using System;

    /// <summary>
    /// Represents an unknown property that takes every value.
    /// </summary>
    sealed class CSSUnknownProperty : CSSProperty
    {
        #region ctor

        internal CSSUnknownProperty(String name, CssStyleDeclaration rule)
            : base(name, rule)
        {
            Reset();
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return true;
        }

        #endregion
    }
}

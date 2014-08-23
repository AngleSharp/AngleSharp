namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More infos can be found on the W3C homepage or
    /// in condensed form at 
    /// http://css-infos.net/property/box-decoration-break
    /// </summary>
    public sealed class CSSBoxDecorationBreak : CSSProperty
    {
        #region Fields

        Boolean _clone;

        #endregion

        #region ctor

        internal CSSBoxDecorationBreak()
            : base(PropertyNames.BoxDecorationBreak)
        {
            _clone = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if each box is independently wrapped with the border
        /// and padding. Otherwise no border and no padding are inserted
        /// at the break.
        /// </summary>
        public Boolean IsCloned
        {
            get { return _clone; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("slice"))
                _clone = false;
            else if (value.Is("clone"))
                _clone = true;
            else if (value == CSSValue.Inherit)
                return true;

            return false;
        }

        #endregion
    }
}

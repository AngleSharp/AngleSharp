namespace AngleSharp.DOM.Css.Properties
{
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// </summary>
    public sealed class CSSOutlineStyleProperty : CSSProperty
    {
        #region Fields

        LineStyle _style;

        #endregion

        #region ctor

        internal CSSOutlineStyleProperty()
            : base(PropertyNames.OutlineStyle)
        {
            _style = LineStyle.None;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected outline style.
        /// </summary>
        public LineStyle Style
        {
            get { return _style; }
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
            var ls = value.ToLineStyle();

            if (ls.HasValue)
                _style = ls.Value;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

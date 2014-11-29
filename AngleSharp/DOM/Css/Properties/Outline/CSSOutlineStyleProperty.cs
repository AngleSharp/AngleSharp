namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// More information available:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/outline-style
    /// </summary>
    sealed class CSSOutlineStyleProperty : CSSProperty, ICssOutlineStyleProperty
    {
        #region Fields

        LineStyle _style;

        #endregion

        #region ctor

        internal CSSOutlineStyleProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.OutlineStyle, rule)
        {
            Reset();
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

        public void SetStyle(LineStyle style)
        {
            _style = style;
        }

        internal override void Reset()
        {
            _style = LineStyle.None;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return From(Map.LineStyles).TryConvert(value, SetStyle);
        }

        #endregion
    }
}

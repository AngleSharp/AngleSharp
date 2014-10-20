namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// </summary>
    sealed class CSSPageBreakInsideProperty : CSSProperty, ICssPageBreakInsideProperty
    {
        #region Fields

        BreakMode _mode;

        #endregion

        #region ctor

        internal CSSPageBreakInsideProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.PageBreakInside, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        public BreakMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = BreakMode.Auto;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.Auto))
                _mode = BreakMode.Auto;
            else if (value.Is(Keywords.Avoid))
                _mode = BreakMode.Avoid;
            else
                return false;

            return true;
        }

        #endregion
    }
}

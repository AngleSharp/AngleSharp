namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/page-break-inside
    /// </summary>
    public sealed class CSSPageBreakInsideProperty : CSSProperty
    {
        #region Fields

        BreakMode _mode;

        #endregion

        #region ctor

        internal CSSPageBreakInsideProperty()
            : base(PropertyNames.PageBreakInside)
        {
            _mode = BreakMode.Auto;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected break mode.
        /// </summary>
        public BreakMode Mode
        {
            get { return _mode; }
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
            if (value.Is("auto"))
                _mode = BreakMode.Auto;
            else if (value.Is("avoid"))
                _mode = BreakMode.Avoid;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

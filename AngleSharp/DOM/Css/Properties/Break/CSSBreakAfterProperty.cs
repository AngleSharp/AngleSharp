namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-after
    /// </summary>
    sealed class CSSBreakAfterProperty : CSSProperty, ICssBreakAfterProperty
    {
        #region Fields

        BreakMode _mode;

        #endregion

        #region ctor

        internal CSSBreakAfterProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.BreakAfter, rule)
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
            var mode = value.ToBreakMode();

            if (mode.HasValue)
            {
                _mode = mode.Value;
                return true;
            }

            return false;
        }

        #endregion
    }
}

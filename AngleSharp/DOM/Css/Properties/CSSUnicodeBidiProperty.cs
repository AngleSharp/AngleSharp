namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CSSUnicodeBidiProperty : CSSProperty, ICssUnicodeBidiProperty
    {
        #region Fields

        UnicodeMode _mode;

        #endregion

        #region ctor

        internal CSSUnicodeBidiProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.UnicodeBidi, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected unicode mode.
        /// </summary>
        public UnicodeMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(UnicodeMode mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = UnicodeMode.Normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.From(Map.UnicodeModes).TryConvert(value, SetState);
        }

        #endregion
    }
}

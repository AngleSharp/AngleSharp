namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CSSOverflowProperty : CSSProperty, ICssOverflowProperty
    {
        #region Fields

        static readonly Dictionary<String, OverflowMode> modes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase);
        OverflowMode _mode;

        #endregion

        #region ctor

        static CSSOverflowProperty()
        {
            modes.Add(Keywords.Visible, OverflowMode.Visible);
            modes.Add(Keywords.Hidden, OverflowMode.Hidden);
            modes.Add(Keywords.Scroll, OverflowMode.Scroll);
            modes.Add(Keywords.Auto, OverflowMode.Auto);
        }

        internal CSSOverflowProperty()
            : base(PropertyNames.Overflow)
        {
        }

        #endregion

        #region Properties

        public OverflowMode State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            _mode = OverflowMode.Visible;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            OverflowMode mode;

            if (modes.TryGetValue(value, out mode))
            {
                _mode = mode;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}

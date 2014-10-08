namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/break-inside
    /// or even better
    /// http://dev.w3.org/csswg/css-break/#break-inside
    /// </summary>
    sealed class CSSBreakInsideProperty : CSSProperty, ICssBreakInsideProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakMode> modes = new Dictionary<String, BreakMode>(StringComparer.OrdinalIgnoreCase);
        BreakMode _mode;

        #endregion

        #region ctor

        static CSSBreakInsideProperty()
        {
            modes.Add(Keywords.Auto, BreakMode.Auto);
            modes.Add(Keywords.Avoid, BreakMode.Avoid);
            modes.Add(Keywords.AvoidPage, BreakMode.AvoidPage);
            modes.Add(Keywords.AvoidColumn, BreakMode.AvoidColumn);
            modes.Add(Keywords.AvoidRegion, BreakMode.AvoidRegion);
        }

        internal CSSBreakInsideProperty()
            : base(PropertyNames.BreakInside)
        {
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

        protected override void Reset()
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
            BreakMode mode;

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

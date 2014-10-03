namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// </summary>
    sealed class CSSPositionProperty : CSSProperty, ICssPositionProperty
    {
        #region Fields

        static readonly Dictionary<String, PositionMode> modes = new Dictionary<String, PositionMode>(StringComparer.OrdinalIgnoreCase);
        PositionMode _mode;

        #endregion

        #region ctor

        static CSSPositionProperty()
        {
            modes.Add(Keywords.Static, PositionMode.Static);
            modes.Add(Keywords.Relative, PositionMode.Relative);
            modes.Add(Keywords.Absolute, PositionMode.Absolute);
            modes.Add(Keywords.Sticky, PositionMode.Sticky);
            modes.Add(Keywords.Fixed, PositionMode.Fixed);
        }

        internal CSSPositionProperty()
            : base(PropertyNames.Position)
        {
            _mode = modes[Keywords.Static];
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the currently selected position mode.
        /// </summary>
        public PositionMode State
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
            PositionMode mode;

            if (modes.TryGetValue(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

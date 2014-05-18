namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    public sealed class CSSOverflowProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, OverflowMode> modes = new Dictionary<String, OverflowMode>(StringComparer.OrdinalIgnoreCase);
        OverflowMode _mode;

        #endregion

        #region ctor

        static CSSOverflowProperty()
        {
            modes.Add("visible", new VisibleOverflowMode());
            modes.Add("hidden", new HiddenOverflowMode());
            modes.Add("scroll", new ScrollOverflowMode());
            modes.Add("auto", new AutoOverflowMode());
        }

        internal CSSOverflowProperty()
            : base(PropertyNames.Overflow)
        {
            _mode = modes["visible"];
            _inherited = false;
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
            OverflowMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes
        
        abstract class OverflowMode
        {
            //TODO Add members that make sense
        }

        sealed class VisibleOverflowMode : OverflowMode
        {
        }

        sealed class HiddenOverflowMode : OverflowMode
        {
        }

        sealed class ScrollOverflowMode : OverflowMode
        {
        }

        sealed class AutoOverflowMode : OverflowMode
        {
        }

        #endregion
    }
}

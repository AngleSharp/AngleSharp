namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clear
    /// </summary>
    public sealed class CSSClearProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, ClearMode> modes = new Dictionary<String, ClearMode>(StringComparer.OrdinalIgnoreCase);
        ClearMode _mode;

        #endregion

        #region ctor

        static CSSClearProperty()
        {
            modes.Add("none", new NoneClearMode());
            modes.Add("left", new LeftClearMode());
            modes.Add("right", new RightClearMode());
            modes.Add("both", new BothClearMode());
        }

        internal CSSClearProperty()
            : base(PropertyNames.Clear)
        {
            _mode = modes["none"];
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
            ClearMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes
        
        abstract class ClearMode
        {
            //TODO Add members that make sense
        }

        class NoneClearMode : ClearMode
        {
        }

        class LeftClearMode : ClearMode
        {
        }

        class RightClearMode : ClearMode
        {
        }

        class BothClearMode : ClearMode
        {
        }

        #endregion
    }
}

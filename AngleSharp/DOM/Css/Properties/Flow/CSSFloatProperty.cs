namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// </summary>
    public sealed class CSSFloatProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, FloatMode> modes = new Dictionary<String, FloatMode>(StringComparer.OrdinalIgnoreCase);
        FloatMode _mode;

        #endregion

        #region ctor

        static CSSFloatProperty()
        {
            modes.Add("none", new NoneFloatMode());
            modes.Add("left", new LeftFloatMode());
            modes.Add("right", new RightFloatMode());
        }

        internal CSSFloatProperty()
            : base(PropertyNames.Float)
        {
            _mode = modes["none"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            FloatMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class FloatMode
        {
            //TODO Add members that make sense
        }

        class NoneFloatMode : FloatMode
        {
        }

        class LeftFloatMode : FloatMode
        {
        }

        class RightFloatMode : FloatMode
        {
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/float
    /// </summary>
    sealed class CSSFloatProperty : CSSProperty
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

        public CSSFloatProperty()
            : base(PropertyNames.Float)
        {
            _mode = modes["none"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                FloatMode mode;

                if (modes.TryGetValue(ident.Value, out mode))
                {
                    _mode = mode;
                    return true;
                }
            }
            else if (value == CSSValue.Inherit)
                return true;

            return false;
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

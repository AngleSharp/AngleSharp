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
            : base(PropertyNames.FLOAT)
        {
            _mode = modes["none"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                FloatMode mode;

                if (modes.TryGetValue(ident.Token, out mode))
                {
                    _mode = mode;
                    return value;
                }
            }
            else if (value == CSSValue.Inherit)
                return value;

            return null;
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

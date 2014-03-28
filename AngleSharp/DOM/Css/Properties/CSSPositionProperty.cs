namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// </summary>
    sealed class CSSPositionProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, PositionMode> modes = new Dictionary<String, PositionMode>(StringComparer.OrdinalIgnoreCase);
        PositionMode _mode;

        #endregion

        #region ctor

        static CSSPositionProperty()
        {
            modes.Add("static", new StaticPositionMode());
            modes.Add("relative", new RelativePositionMode());
            modes.Add("absolute", new AbsolutePositionMode());
            modes.Add("sticky", new StickyPositionMode());
            modes.Add("fixed", new FixedPositionMode());
        }

        public CSSPositionProperty()
            : base(PropertyNames.POSITION)
        {
            _mode = modes["static"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                PositionMode mode;

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

        abstract class PositionMode
        {
            //TODO Add members that make sense
        }

        class StaticPositionMode : PositionMode
        {
        }

        class RelativePositionMode : PositionMode
        {
        }

        class AbsolutePositionMode : PositionMode
        {
        }

        class StickyPositionMode : PositionMode
        {
        }

        class FixedPositionMode : PositionMode
        {
        }

        #endregion
    }
}

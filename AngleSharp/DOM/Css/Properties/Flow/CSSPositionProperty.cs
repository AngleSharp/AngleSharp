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
            : base(PropertyNames.Position)
        {
            _mode = modes["static"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                PositionMode mode;

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

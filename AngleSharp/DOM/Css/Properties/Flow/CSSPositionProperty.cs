namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/position
    /// </summary>
    public sealed class CSSPositionProperty : CSSProperty
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

        internal CSSPositionProperty()
            : base(PropertyNames.Position)
        {
            _mode = modes["static"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            PositionMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
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

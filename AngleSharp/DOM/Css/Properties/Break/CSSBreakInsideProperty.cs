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
    sealed class CSSBreakInsideProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BreakInsideMode> modes = new Dictionary<String, BreakInsideMode>(StringComparer.OrdinalIgnoreCase);
        BreakInsideMode _mode;

        #endregion

        #region ctor

        static CSSBreakInsideProperty()
        {
            modes.Add("auto", new AutoBreakInsideMode());
            modes.Add("avoid", new AvoidPageBreakInsideMode());
            modes.Add("avoid-page", new AvoidPageBreakInsideMode());
            modes.Add("avoid-column", new AvoidColumnBreakInsideMode());
            modes.Add("avoid-region", new AvoidRegionBreakInsideMode());
        }

        public CSSBreakInsideProperty()
            : base(PropertyNames.BreakInside)
        {
            _mode = modes["auto"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                BreakInsideMode mode;

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

        abstract class BreakInsideMode
        {
            //TODO Add members that make sense
        }
        
        class AutoBreakInsideMode : BreakInsideMode
        {
        }

        class AvoidBreakInsideMode : BreakInsideMode
        {
        }

        class AvoidPageBreakInsideMode : BreakInsideMode
        {
        }

        class AvoidColumnBreakInsideMode : BreakInsideMode
        {
        }

        class AvoidRegionBreakInsideMode : BreakInsideMode
        {
        }
        
        #endregion
    }
}

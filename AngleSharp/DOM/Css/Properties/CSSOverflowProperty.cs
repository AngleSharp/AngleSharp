namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/overflow
    /// </summary>
    sealed class CSSOverflowProperty : CSSProperty
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

        public CSSOverflowProperty()
            : base(PropertyNames.OVERFLOW)
        {
            _mode = modes["visible"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override CSSValue CheckValue(CSSValue value)
        {
            if (value is CSSIdent)
            {
                var ident = (CSSIdent)value;
                OverflowMode mode;

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
        
        abstract class OverflowMode
        {
            //TODO Add members that make sense
        }

        class VisibleOverflowMode : OverflowMode
        {
        }

        class HiddenOverflowMode : OverflowMode
        {
        }

        class ScrollOverflowMode : OverflowMode
        {
        }

        class AutoOverflowMode : OverflowMode
        {
        }

        #endregion
    }
}

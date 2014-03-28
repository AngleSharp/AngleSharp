namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/clear
    /// </summary>
    sealed class CSSClearProperty : CSSProperty
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

        public CSSClearProperty()
            : base(PropertyNames.CLEAR)
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
                ClearMode mode;

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

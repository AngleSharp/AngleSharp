namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// </summary>
    sealed class CSSCaptionSideProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, CaptionSideMode> modes = new Dictionary<String, CaptionSideMode>(StringComparer.OrdinalIgnoreCase);
        CaptionSideMode _mode;

        #endregion

        #region ctor

        static CSSCaptionSideProperty()
        {
            modes.Add("top", new TopCaptionSideMode());
            modes.Add("bottom", new BottomCaptionSideMode());
        }

        public CSSCaptionSideProperty()
            : base(PropertyNames.CAPTION_SIDE)
        {
            _mode = modes["top"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifier)
            {
                var ident = (CSSIdentifier)value;
                CaptionSideMode mode;

                if (modes.TryGetValue(ident.Identifier, out mode))
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
        
        abstract class CaptionSideMode
        {
            //TODO Add members that make sense
        }

        class TopCaptionSideMode : CaptionSideMode
        {
        }

        class BottomCaptionSideMode : CaptionSideMode
        {
        }

        #endregion
    }
}

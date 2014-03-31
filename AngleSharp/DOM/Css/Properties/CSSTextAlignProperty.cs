namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-align
    /// </summary>
    sealed class CSSTextAlignProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TextAlignMode> modes = new Dictionary<String, TextAlignMode>(StringComparer.OrdinalIgnoreCase);
        TextAlignMode _mode;

        #endregion

        #region ctor

        static CSSTextAlignProperty()
        {
            modes.Add("left", new LeftTextAlignMode());
            modes.Add("right", new RightTextAlignMode());
            modes.Add("center", new CenterTextAlignMode());
            modes.Add("justify", new JustifyTextAlignMode());
        }

        public CSSTextAlignProperty()
            : base(PropertyNames.TEXT_ALIGN)
        {
            _mode = modes["left"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                TextAlignMode mode;

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
        
        abstract class TextAlignMode
        {
            //TODO Add members that make sense
        }

        class LeftTextAlignMode : TextAlignMode
        {
        }

        class RightTextAlignMode : TextAlignMode
        {
        }

        class CenterTextAlignMode : TextAlignMode
        {
        }

        class JustifyTextAlignMode : TextAlignMode
        {
        }

        #endregion
    }
}

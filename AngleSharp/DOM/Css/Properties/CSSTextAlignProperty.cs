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

        /// <summary>
        /// The inline contents are aligned to the left edge of the line box.
        /// </summary>
        sealed class LeftTextAlignMode : TextAlignMode
        {
        }

        /// <summary>
        /// The inline contents are aligned to the right edge of the line box.
        /// </summary>
        sealed class RightTextAlignMode : TextAlignMode
        {
        }

        /// <summary>
        /// The inline contents are centered within the line box.
        /// </summary>
        sealed class CenterTextAlignMode : TextAlignMode
        {
        }

        /// <summary>
        /// The text is justified. Text should line up their left and right
        /// edges to the left and right content edges of the paragraph.
        /// </summary>
        sealed class JustifyTextAlignMode : TextAlignMode
        {
        }

        #endregion
    }
}

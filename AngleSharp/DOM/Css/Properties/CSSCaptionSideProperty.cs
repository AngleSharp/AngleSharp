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

        static readonly Dictionary<String, CaptionSide> modes = new Dictionary<String, CaptionSide>(StringComparer.OrdinalIgnoreCase);
        CaptionSide _mode;

        #endregion

        #region ctor

        static CSSCaptionSideProperty()
        {
            modes.Add("top", CaptionSide.Top);
            modes.Add("bottom", CaptionSide.Bottom);
        }

        public CSSCaptionSideProperty()
            : base(PropertyNames.CaptionSide)
        {
            _mode = CaptionSide.Top;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            CaptionSide mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum CaptionSide
        {
            /// <summary>
            /// The caption box will be above the table.
            /// </summary>
            Top,
            /// <summary>
            /// The caption box will be below the table.
            /// </summary>
            Bottom
        }

        #endregion
    }
}

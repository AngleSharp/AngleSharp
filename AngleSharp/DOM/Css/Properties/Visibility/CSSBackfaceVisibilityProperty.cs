namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/backface-visibility
    /// </summary>
    sealed class CSSBackfaceVisibility : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, BackfaceVisibility> modes = new Dictionary<String, BackfaceVisibility>(StringComparer.OrdinalIgnoreCase);
        BackfaceVisibility _mode;

        #endregion

        #region ctor

        static CSSBackfaceVisibility()
        {
            modes.Add("visible", BackfaceVisibility.Visible);
            modes.Add("hidden", BackfaceVisibility.Hidden);
        }

        public CSSBackfaceVisibility()
            : base(PropertyNames.BackfaceVisibility)
        {
            _mode = BackfaceVisibility.Visible;
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            BackfaceVisibility mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum BackfaceVisibility
        {
            /// <summary>
            /// The back face is visible, allowing the front
            /// face to be displayed mirrored.
            /// </summary>
            Visible,
            /// <summary>
            /// The back face is not visible, hiding the front face.
            /// </summary>
            Hidden
        }

        #endregion
    }
}

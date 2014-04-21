namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// </summary>
    public sealed class CSSTextTransformProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TextTransform> modes = new Dictionary<String, TextTransform>(StringComparer.OrdinalIgnoreCase);
        TextTransform _mode;

        #endregion

        #region ctor

        static CSSTextTransformProperty()
        {
            modes.Add("none", TextTransform.None);
            modes.Add("capitalize", TextTransform.Capitalize);
            modes.Add("uppercase", TextTransform.Uppercase);
            modes.Add("lowercase", TextTransform.Lowercase);
            modes.Add("full-width", TextTransform.FullWidth);
        }

        internal CSSTextTransformProperty()
            : base(PropertyNames.TextTransform)
        {
            _mode = TextTransform.None;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected text transformation mode.
        /// </summary>
        public TextTransform Transform
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            TextTransform mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

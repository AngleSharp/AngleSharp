namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-transform
    /// </summary>
    sealed class CSSTextTransformProperty : CSSProperty
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

        public CSSTextTransformProperty()
            : base(PropertyNames.TextTransform)
        {
            _mode = TextTransform.None;
            _inherited = true;
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

        #region Modes

        enum TextTransform
        {
            /// <summary>
            /// Is a keyword preventing the case of all characters to be changed.
            /// </summary>
            None,
            /// <summary>
            /// Is a keyword forcing the first letter of each word to be converted
            /// to uppercase. Other characters are unchanged; that is, they retain
            /// their original case as written in the element's text.
            /// </summary>
            Capitalize,
            /// <summary>
            /// Is a keyword forcing all characters to be converted to uppercase.
            /// </summary>
            Uppercase,
            /// <summary>
            /// Is a keyword forcing all characters to be converted to lowercase.
            /// </summary>
            Lowercase,
            /// <summary>
            /// Is a keyword forcing the writing of a character, mainly ideograms and
            /// latin scripts inside a square, allowing them to be aligned in the
            /// usual East Asian scripts (like Chinese or Japanese).
            /// </summary>
            FullWidth
        }

        #endregion
    }
}

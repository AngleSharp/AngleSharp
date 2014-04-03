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

        static readonly Dictionary<String, TextTransformMode> modes = new Dictionary<String, TextTransformMode>(StringComparer.OrdinalIgnoreCase);
        TextTransformMode _mode;

        #endregion

        #region ctor

        static CSSTextTransformProperty()
        {
            modes.Add("none", new NoneTextTransformMode());
            modes.Add("capitalize", new CapitalizeTextTransformMode());
            modes.Add("uppercase", new UppercaseTextTransformMode());
            modes.Add("lowercase", new LowercaseTextTransformMode());
            modes.Add("full-width", new FullWidthTextTransformMode());
        }

        public CSSTextTransformProperty()
            : base(PropertyNames.TEXT_TRANSFORM)
        {
            _mode = modes["none"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifierValue)
            {
                var ident = (CSSIdentifierValue)value;
                TextTransformMode mode;

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
        
        abstract class TextTransformMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Is a keyword preventing the case of all characters to be changed.
        /// </summary>
        sealed class NoneTextTransformMode : TextTransformMode
        {
        }

        /// <summary>
        /// Is a keyword forcing the first letter of each word to be converted
        /// to uppercase. Other characters are unchanged; that is, they retain
        /// their original case as written in the element's text.
        /// </summary>
        sealed class CapitalizeTextTransformMode : TextTransformMode
        {
        }

        /// <summary>
        /// Is a keyword forcing all characters to be converted to uppercase.
        /// </summary>
        sealed class UppercaseTextTransformMode : TextTransformMode
        {
        }

        /// <summary>
        /// Is a keyword forcing all characters to be converted to lowercase.
        /// </summary>
        sealed class LowercaseTextTransformMode : TextTransformMode
        {
        }

        /// <summary>
        /// Is a keyword forcing the writing of a character, mainly ideograms and
        /// latin scripts inside a square, allowing them to be aligned in the
        /// usual East Asian scripts (like Chinese or Japanese).
        /// </summary>
        sealed class FullWidthTextTransformMode : TextTransformMode
        {
        }

        #endregion
    }
}

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

        class NoneTextTransformMode : TextTransformMode
        {
        }

        class CapitalizeTextTransformMode : TextTransformMode
        {
        }

        class UppercaseTextTransformMode : TextTransformMode
        {
        }

        class LowercaseTextTransformMode : TextTransformMode
        {
        }

        class FullWidthTextTransformMode : TextTransformMode
        {
        }

        #endregion
    }
}

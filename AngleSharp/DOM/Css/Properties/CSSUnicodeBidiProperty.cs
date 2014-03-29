namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    sealed class CSSUnicodeBidiProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, UnicodeMode> modes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase);
        UnicodeMode _mode;

        #endregion

        #region ctor

        static CSSUnicodeBidiProperty()
        {
            modes.Add("normal", new NormalUnicodeMode());
            modes.Add("embed", new EmbedUnicodeMode());
            modes.Add("isolate", new IsolateUnicodeMode());
            modes.Add("isolate-override", new IsolateOverrideUnicodeMode());
            modes.Add("bidi-override", new BidiOverrideUnicodeMode());
            modes.Add("plaintext", new PlaintextUnicodeMode());
        }

        public CSSUnicodeBidiProperty()
            : base(PropertyNames.UNICODE_BIDI)
        {
            _mode = modes["normal"];
            _inherited = false;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            if (value is CSSIdentifier)
            {
                var ident = (CSSIdentifier)value;
                UnicodeMode mode;

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
        
        abstract class UnicodeMode
        {
            //TODO Add members that make sense
        }

        class NormalUnicodeMode : UnicodeMode
        {
        }

        class EmbedUnicodeMode : UnicodeMode
        {
        }

        class IsolateUnicodeMode : UnicodeMode
        {
        }

        class BidiOverrideUnicodeMode : UnicodeMode
        {
        }

        class IsolateOverrideUnicodeMode : UnicodeMode
        {
        }

        class PlaintextUnicodeMode : UnicodeMode
        {
        }

        #endregion
    }
}

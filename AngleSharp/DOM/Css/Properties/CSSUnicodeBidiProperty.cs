namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/unicode-bidi
    /// </summary>
    public sealed class CSSUnicodeBidiProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, UnicodeMode> modes = new Dictionary<String, UnicodeMode>(StringComparer.OrdinalIgnoreCase);
        UnicodeMode _mode;

        #endregion

        #region ctor

        static CSSUnicodeBidiProperty()
        {
            modes.Add("normal", UnicodeMode.Normal);
            modes.Add("embed", UnicodeMode.Embed);
            modes.Add("isolate", UnicodeMode.Isolate);
            modes.Add("isolate-override", UnicodeMode.IsolateOverride);
            modes.Add("bidi-override", UnicodeMode.BidiOverride);
            modes.Add("plaintext", UnicodeMode.Plaintext);
        }

        internal CSSUnicodeBidiProperty()
            : base(PropertyNames.UnicodeBidi)
        {
            _mode = UnicodeMode.Normal;
            _inherited = false;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected unicode mode.
        /// </summary>
        public UnicodeMode Mode
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            UnicodeMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

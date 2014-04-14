namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// </summary>
    sealed class CSSWhiteSpaceProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, WhiteSpaceMode> modes = new Dictionary<String, WhiteSpaceMode>(StringComparer.OrdinalIgnoreCase);
        WhiteSpaceMode _mode;

        #endregion

        #region ctor

        static CSSWhiteSpaceProperty()
        {
            modes.Add("normal", new NormalWhiteSpaceMode());
            modes.Add("pre", new PreWhiteSpaceMode());
            modes.Add("nowrap", new NoWrapWhiteSpaceMode());
            modes.Add("pre-wrap", new PreWrapWhiteSpaceMode());
            modes.Add("pre-line", new PreWrapWhiteSpaceMode());
        }

        public CSSWhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace)
        {
            _mode = modes["normal"];
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            WhiteSpaceMode mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes
        
        abstract class WhiteSpaceMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// Sequences of whitespace are collapsed. Newline characters in the source
        /// are handled as other whitespace. Breaks lines as necessary to fill
        /// line boxes.
        /// </summary>
        sealed class NormalWhiteSpaceMode : WhiteSpaceMode
        {
        }

        /// <summary>
        /// Sequences of whitespace are preserved, lines are only broken at newline
        /// characters in the source and at <br> elements.
        /// </summary>
        sealed class PreWhiteSpaceMode : WhiteSpaceMode
        {
        }

        /// <summary>
        /// Collapses whitespace as for normal, but suppresses line breaks (text
        /// wrapping) within text.
        /// </summary>
        sealed class NoWrapWhiteSpaceMode : WhiteSpaceMode
        {
        }

        /// <summary>
        /// Sequences of whitespace are preserved. Lines are broken at newline characters,
        /// at br, and as necessary to fill line boxes.
        /// </summary>
        sealed class PreWrapWhiteSpaceMode : WhiteSpaceMode
        {
        }

        /// <summary>
        /// Sequences of whitespace are collapsed. Lines are broken at newline characters,
        /// at br, and as necessary to fill line boxes.
        /// </summary>
        sealed class PreLineWhiteSpaceMode : WhiteSpaceMode
        {
        }

        #endregion
    }
}

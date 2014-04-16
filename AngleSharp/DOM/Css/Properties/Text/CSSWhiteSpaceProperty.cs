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

        static readonly Dictionary<String, Whitespace> modes = new Dictionary<String, Whitespace>(StringComparer.OrdinalIgnoreCase);
        Whitespace _mode;

        #endregion

        #region ctor

        static CSSWhiteSpaceProperty()
        {
            modes.Add("normal", Whitespace.Normal);
            modes.Add("pre", Whitespace.Pre);
            modes.Add("nowrap", Whitespace.NoWrap);
            modes.Add("pre-wrap", Whitespace.PreWrap);
            modes.Add("pre-line", Whitespace.PreLine);
        }

        public CSSWhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace)
        {
            _mode = Whitespace.Normal;
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            Whitespace mode;

            if (value is CSSIdentifierValue && modes.TryGetValue(((CSSIdentifierValue)value).Value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        enum Whitespace
        {
            /// <summary>
            /// Sequences of whitespace are collapsed. Newline characters in the source
            /// are handled as other whitespace. Breaks lines as necessary to fill
            /// line boxes.
            /// </summary>
            Normal,
            /// <summary>
            /// Sequences of whitespace are preserved, lines are only broken at newline
            /// characters in the source and at <br> elements.
            /// </summary>
            Pre,
            /// <summary>
            /// Collapses whitespace as for normal, but suppresses line breaks (text
            /// wrapping) within text.
            /// </summary>
            NoWrap,
            /// <summary>
            /// Sequences of whitespace are preserved. Lines are broken at newline characters,
            /// at br, and as necessary to fill line boxes.
            /// </summary>
            PreWrap,
            /// <summary>
            /// Sequences of whitespace are collapsed. Lines are broken at newline characters,
            /// at br, and as necessary to fill line boxes.
            /// </summary>
            PreLine

        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// </summary>
    public sealed class CSSWhiteSpaceProperty : CSSProperty
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

        internal CSSWhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace)
        {
            _mode = Whitespace.Normal;
            _inherited = true;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected whitespace handling mode.
        /// </summary>
        public Whitespace Mode
        {
            get { return _mode; }
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
    }
}

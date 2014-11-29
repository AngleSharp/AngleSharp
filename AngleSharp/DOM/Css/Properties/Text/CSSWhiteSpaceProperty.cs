namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// </summary>
    sealed class CSSWhiteSpaceProperty : CSSProperty, ICssWhitespaceProperty
    {
        #region Fields

        static readonly Dictionary<String, Whitespace> modes = new Dictionary<String, Whitespace>(StringComparer.OrdinalIgnoreCase);
        Whitespace _mode;

        #endregion

        #region ctor

        static CSSWhiteSpaceProperty()
        {
            modes.Add(Keywords.Normal, Whitespace.Normal);
            modes.Add(Keywords.Pre, Whitespace.Pre);
            modes.Add(Keywords.Nowrap, Whitespace.NoWrap);
            modes.Add(Keywords.PreWrap, Whitespace.PreWrap);
            modes.Add(Keywords.PreLine, Whitespace.PreLine);
        }

        internal CSSWhiteSpaceProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.WhiteSpace, rule, PropertyFlags.Inherited)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected whitespace handling mode.
        /// </summary>
        public Whitespace State
        {
            get { return _mode; }
        }

        #endregion

        #region Methods

        public void SetState(Whitespace mode)
        {
            _mode = mode;
        }

        internal override void Reset()
        {
            _mode = Whitespace.Normal;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return From(modes).TryConvert(value, SetState);
        }

        #endregion
    }
}

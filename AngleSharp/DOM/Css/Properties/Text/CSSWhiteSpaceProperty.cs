namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/white-space
    /// </summary>
    sealed class CSSWhiteSpaceProperty : CSSProperty, ICssWhiteSpaceProperty
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

        internal CSSWhiteSpaceProperty()
            : base(PropertyNames.WhiteSpace, PropertyFlags.Inherited)
        {
            _mode = Whitespace.Normal;
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

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            Whitespace mode;

            if (modes.TryGetValue(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

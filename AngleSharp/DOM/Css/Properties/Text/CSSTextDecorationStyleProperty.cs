namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// </summary>
    public sealed class CSSTextDecorationStyleProperty : CSSProperty
    {
        #region Fields

        static readonly Dictionary<String, TextDecorationStyle> _styles = new Dictionary<String, TextDecorationStyle>(StringComparer.OrdinalIgnoreCase);
        TextDecorationStyle _style;

        #endregion

        #region ctor

        static CSSTextDecorationStyleProperty()
        {
            _styles.Add(Keywords.Solid, TextDecorationStyle.Solid);
            _styles.Add(Keywords.Double, TextDecorationStyle.Double);
            _styles.Add(Keywords.Dotted, TextDecorationStyle.Dotted);
            _styles.Add(Keywords.Dashed, TextDecorationStyle.Dashed);
            _styles.Add(Keywords.Wavy, TextDecorationStyle.Wavy);
        }

        internal CSSTextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
        {
            _style = TextDecorationStyle.Solid;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the selected decoration style.
        /// </summary>
        public TextDecorationStyle DecorationStyle
        {
            get { return _style; }
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
            TextDecorationStyle style;

            if (value is CSSIdentifierValue && _styles.TryGetValue(((CSSIdentifierValue)value).Value, out style))
                _style = style;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/text-decoration-style
    /// </summary>
    sealed class CSSTextDecorationStyleProperty : CSSProperty, ICssTextDecorationStyleProperty
    {
        #region Fields

        TextDecorationStyle _style;

        #endregion

        #region ctor

        internal CSSTextDecorationStyleProperty()
            : base(PropertyNames.TextDecorationStyle)
        {
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

        internal override void Reset()
        {
            _style = TextDecorationStyle.Solid;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            var style = value.ToDecorationStyle();

            if (style.HasValue)
            {
                _style = style.Value;
                return true;
            }
            
            return false;
        }

        #endregion
    }
}

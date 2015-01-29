namespace AngleSharp.Dom.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/caption-side
    /// </summary>
    sealed class CssCaptionSideProperty : CssProperty, ICssCaptionSideProperty
    {
        #region Fields

        internal static readonly IValueConverter<Boolean> Converter = Converters.Toggle(Keywords.Top, Keywords.Bottom);
        internal static readonly Boolean Default = true;
        Boolean _top;

        #endregion

        #region ctor

        internal CssCaptionSideProperty(CssStyleDeclaration rule)
            : base(PropertyNames.CaptionSide, rule)
        {
            Reset();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets if the caption box will be above the table.
        /// Otherwise the caption box will be below the table.
        /// </summary>
        public Boolean IsOnTop
        {
            get { return _top; }
        }

        #endregion

        #region Methods

        public void SetMode(Boolean onTop)
        {
            _top = onTop;
        }

        internal override void Reset()
        {
            _top = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetMode);
        }

        #endregion
    }
}

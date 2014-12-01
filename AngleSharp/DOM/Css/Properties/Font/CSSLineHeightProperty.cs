namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/line-height
    /// </summary>
    sealed class CSSLineHeightProperty : CSSProperty, ICssLineHeightProperty
    {
        #region Fields

        internal static readonly IDistance Default = new Percent(120f);
        internal static readonly IValueConverter<IDistance> Converter = WithLineHeight();
        IDistance _height;

        #endregion

        #region ctor

        internal CSSLineHeightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.LineHeight, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        public IDistance Height
        {
            get { return _height; }
        }

        #endregion

        #region Methods

        public void SetHeight(IDistance height)
        {
            _height = height;
        }

        internal override void Reset()
        {
            _height = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetHeight);
        }

        #endregion
    }
}

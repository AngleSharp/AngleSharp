namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    sealed class CSSFontWeightProperty : CSSProperty, ICssFontWeightProperty
    {
        #region Fields

        internal static readonly FontWeight Default = Map.FontWeights[Keywords.Normal];
        internal static readonly IValueConverter<FontWeight> Converter = From(Map.FontWeights).Or(
            WithInteger().Constraint(m => m >= 100 && m <= 900).To(m => new FontWeight { IsRelative = false, Value = m }));
        FontWeight _weight;

        #endregion

        #region ctor
        
        internal CSSFontWeightProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.FontWeight, rule, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
            Reset();
        }

        #endregion

        #region Properties

        public Int32 Weight
        {
            get { return _weight.Value; }
        }

        public Boolean IsRelative
        {
            get { return _weight.IsRelative; }
        }

        #endregion

        #region Methods

        void SetWeight(FontWeight weight)
        {
            _weight = weight;
        }

        internal override void Reset()
        {
            _weight = Default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(ICssValue value)
        {
            return Converter.TryConvert(value, SetWeight);
        }

        #endregion
    }
}

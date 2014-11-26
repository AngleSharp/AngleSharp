namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/font-weight
    /// </summary>
    sealed class CSSFontWeightProperty : CSSProperty, ICssFontWeightProperty
    {
        #region Fields

        static readonly Dictionary<String, FontWeight> _weights = new Dictionary<String, FontWeight>(StringComparer.OrdinalIgnoreCase);
        FontWeight _weight;

        #endregion

        #region ctor

        static CSSFontWeightProperty()
        {
            _weights.Add(Keywords.Normal, new FontWeight { IsRelative = false, Value = 400 });
            _weights.Add(Keywords.Bold, new FontWeight { IsRelative = false, Value = 700 });
            _weights.Add(Keywords.Bolder, new FontWeight { IsRelative = true, Value = 100 });
            _weights.Add(Keywords.Lighter, new FontWeight { IsRelative = true, Value = -100 });
        }

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

        public void SetWeight(Int32 weight)
        {
            SetExplicitWeight(new FontWeight { IsRelative = false, Value = weight });
        }

        void SetExplicitWeight(FontWeight weight)
        {
            _weight = weight;
        }

        internal override void Reset()
        {
            _weight = _weights[Keywords.Normal];
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithInteger().Constraint(m => m >= 100 && m <= 900).To(m => new FontWeight { IsRelative = false, Value = m }).Or(
                   this.From(_weights)).TryConvert(value, SetExplicitWeight);
        }

        struct FontWeight
        {
            public Boolean IsRelative;
            public Int32 Value;
        }

        #endregion
    }
}

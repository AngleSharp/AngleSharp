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

        static readonly Dictionary<String, FontWeight> FontWeights = new Dictionary<String, FontWeight>(StringComparer.OrdinalIgnoreCase)
        {
            { Keywords.Normal, new FontWeight { IsRelative = false, Value = 400 } },
            { Keywords.Bold, new FontWeight { IsRelative = false, Value = 700 } },
            { Keywords.Bolder, new FontWeight { IsRelative = true, Value = 100 } },
            { Keywords.Lighter, new FontWeight { IsRelative = true, Value = -100 } }
        };

        internal static readonly FontWeight Default = FontWeights[Keywords.Normal];
        internal static readonly IValueConverter<FontWeight> Converter = FontWeights.ToConverter().Or(
            Converters.IntegerConverter.Constraint(m => m >= 100 && m <= 900).To(
            m => new FontWeight { IsRelative = false, Value = m }));
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

        #region Structure

        internal struct FontWeight
        {
            public Boolean IsRelative;
            public Int32 Value;
        }

        #endregion
    }
}

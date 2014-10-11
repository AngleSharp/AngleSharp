namespace AngleSharp.DOM.Css
{
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

        internal CSSFontWeightProperty()
            : base(PropertyNames.FontWeight, PropertyFlags.Inherited | PropertyFlags.Animatable)
        {
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
            FontWeight weight;

            if (_weights.TryGetValue(value, out weight))
            {
                _weight = weight;
                return true;
            }
            else
            {
                var val = value.ToInteger();

                if (val.HasValue && val.Value >= 100 && val.Value <= 900)
                {
                    _weight = new FontWeight { IsRelative = false, Value = val.Value };   
                    return true;
                }
            }

            return false;
        }

        struct FontWeight
        {
            public Boolean IsRelative;
            public Int32 Value;
        }

        #endregion
    }
}

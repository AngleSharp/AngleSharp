namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CSSCounterResetProperty : CSSProperty, ICssCounterResetProperty
    {
        #region Fields

        Dictionary<String, Int32> _resets;

        #endregion

        #region ctor

        internal CSSCounterResetProperty()
            : base(PropertyNames.CounterReset)
        {
            _resets = new Dictionary<String, Int32>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the reset-value of the specified counter.
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public Int32 this[String counter]
        {
            get { return _resets[counter]; }
        }

        /// <summary>
        /// Gets an enumeration over all counters.
        /// </summary>
        public IEnumerable<String> Counters
        {
            get { return _resets.Keys; }
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
            if (value == CSSValue.Inherit)
                return true;
            else if (value is CSSValueList)
                return CheckList((CSSValueList)value);

            var primitive = value as CSSPrimitiveValue;

            if (primitive != null && primitive.Unit == UnitType.Ident)
                return CheckIdentifier(primitive);

            return false;
        }

        Boolean CheckIdentifier(CSSPrimitiveValue ident)
        {
            _resets.Clear();

            if (!ident.Is(Keywords.None))
                _resets.Add(ident.GetString(), 0);

            return true;                
        }

        Boolean CheckList(CSSValueList list)
        {
            var entries = new List<KeyValuePair<String, Int32>>();

            for (int i = 0; i < list.Length; i++)
            {
                var primitive = list[i] as CSSPrimitiveValue;

                if (primitive == null || primitive.Unit != UnitType.Ident)
                    return false;

                var ident = primitive.GetString();
                var num = 0;

                if (i + 1 < list.Length)
                {
                    var number = list[i + 1].ToInteger();

                    if (number.HasValue)
                    {
                        i++;
                        num = number.Value;
                    }
                }

                entries.Add(new KeyValuePair<String, Int32>(ident, num));
            }

            _resets.Clear();

            foreach (var entry in entries)
                _resets[entry.Key] = entry.Value;

            return true;
        }

        #endregion
    }
}

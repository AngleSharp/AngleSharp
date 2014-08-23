namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    public sealed class CSSCounterIncrementProperty : CSSProperty
    {
        #region Fields

        Dictionary<String, Int32> _increments;

        #endregion

        #region ctor

        internal CSSCounterIncrementProperty()
            : base(PropertyNames.CounterIncrement)
        {
            _increments = new Dictionary<String, Int32>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the increment of the specified counter.
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public Int32 this[String counter]
        {
            get { return _increments[counter]; }
        }

        /// <summary>
        /// Gets an enumeration over all counters.
        /// </summary>
        public IEnumerable<String> Counters
        {
            get { return _increments.Keys; }
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
            else if (value is CSSIdentifierValue)
                return CheckIdentifier((CSSIdentifierValue)value);

            return false;
        }

        Boolean CheckIdentifier(CSSIdentifierValue ident)
        {
            var value = ident.Value;
            _increments.Clear();

            if (!value.Equals("none", StringComparison.OrdinalIgnoreCase))
                _increments.Add(value, 1);

            return true;
        }

        Boolean CheckList(CSSValueList list)
        {
            var entries = new List<KeyValuePair<String, Int32>>();

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] is CSSIdentifierValue == false)
                    return false;

                var ident = ((CSSIdentifierValue)list[i]).Value;
                var num = 1;

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

            _increments.Clear();

            foreach (var entry in entries)
                _increments[entry.Key] = entry.Value;

            return true;
        }

        #endregion
    }
}

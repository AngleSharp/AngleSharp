namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-increment
    /// </summary>
    sealed class CSSCounterIncrementProperty : CSSProperty, ICssCounterIncrementProperty
    {
        #region Fields

        readonly Dictionary<String, Int32> _increments;

        #endregion

        #region ctor

        internal CSSCounterIncrementProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.CounterIncrement, rule)
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

        public void SetCounters(IEnumerable<KeyValuePair<String, Int32>> counters)
        {
            _increments.Clear();

            foreach (var counter in counters)
                _increments[counter.Key] = counter.Value;
        }

        internal override void Reset()
        {
            _increments.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return WithIdentifier().Split(
                        WithIdentifier().To(m => new KeyValuePair<String, Int32>(m, 1)).Or(
                        WithArgs(
                            WithIdentifier(), 
                            WithInteger(), 
                        m => new KeyValuePair<String, Int32>(m.Item1, m.Item2)))).TryConvert(value, SetCounters);
        }

        #endregion
    }
}

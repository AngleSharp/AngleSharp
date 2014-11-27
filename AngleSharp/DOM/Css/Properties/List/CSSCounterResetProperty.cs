namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// More information available at:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/counter-reset
    /// </summary>
    sealed class CSSCounterResetProperty : CSSProperty, ICssCounterResetProperty
    {
        #region Fields

        readonly Dictionary<String, Int32> _resets;

        #endregion

        #region ctor

        internal CSSCounterResetProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.CounterReset, rule)
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

        public void SetCounters(IEnumerable<KeyValuePair<String, Int32>> counters)
        {
            _resets.Clear();

            foreach (var counter in counters)
                _resets[counter.Key] = counter.Value;
        }

        internal override void Reset()
        {
            _resets.Clear();
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.WithIdentifier().Split(
                        this.WithIdentifier().To(m => new KeyValuePair<String, Int32>(m, 0)).Or(
                        this.WithArgs(
                            this.WithIdentifier(),
                            this.WithInteger(),
                        m => new KeyValuePair<String, Int32>(m.Item1, m.Item2)))).TryConvert(value, SetCounters);
        }

        #endregion
    }
}

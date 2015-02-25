namespace AngleSharp.Dom.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Base class for all counter properties.
    /// </summary>
    abstract class CssCounterProperty : CssProperty
    {
        #region Fields
        
        readonly Dictionary<String, Int32> _counters;

        #endregion

        #region ctor

        internal CssCounterProperty(String name, CssStyleDeclaration rule)
            : base(name, rule)
        {
            _counters = new Dictionary<String, Int32>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the value of the specified counter.
        /// </summary>
        /// <param name="counter"></param>
        /// <returns></returns>
        public Int32 this[String counter]
        {
            get { return _counters[counter]; }
        }

        /// <summary>
        /// Gets an enumeration over all counters.
        /// </summary>
        public IEnumerable<String> Counters
        {
            get { return _counters.Keys; }
        }

        #endregion

        #region Methods

        public void SetCounters(IEnumerable<KeyValuePair<String, Int32>> counters)
        {
            _counters.Clear();

            foreach (var counter in counters)
                _counters[counter.Key] = counter.Value;
        }

        internal override void Reset()
        {
            _counters.Clear();
        }

        #endregion
    }
}

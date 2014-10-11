namespace AngleSharp.DOM.Css
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/quotes
    /// </summary>
    sealed class CSSQuotesProperty : CSSProperty, ICssQuotesProperty
    {
        #region Fields

        static readonly Tuple<String, String> _default = new Tuple<String, String>("«", "»");
        List<Tuple<String, String>> _quotes;

        #endregion

        #region ctor

        internal CSSQuotesProperty()
            : base(PropertyNames.Quotes, PropertyFlags.Inherited)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the enumeration with pairs of values for open-quote and
        /// close-quote. The first pair represents the outer level of quotation,
        /// the second pair is for the first nested level, next pair for third
        /// level and so on.
        /// </summary>
        public IEnumerable<Tuple<String, String>> Quotes
        {
            get { return _quotes; }
        }

        #endregion

        #region Methods

        internal override void Reset()
        {
            if (_quotes == null)
                _quotes = new List<Tuple<String, String>>();
            else
                _quotes.Clear();

            _quotes.Add(_default);
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is(Keywords.None))
                _quotes.Clear();
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length % 2 != 0)
                    return false;

                var quotes = new List<Tuple<String, String>>();

                for (int i = 0; i < values.Length; i += 2)
                {
                    var open = values[i].ToCssString();
                    var close = values[i + 1].ToCssString();

                    if (open == null || close == null)
                        return false;

                    quotes.Add(Tuple.Create(open, close));
                }

                _quotes = quotes;
            }
            else
                return false;

            return true;
        }

        #endregion
    }
}

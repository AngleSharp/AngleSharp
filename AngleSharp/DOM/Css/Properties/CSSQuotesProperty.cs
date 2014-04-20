namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/quotes
    /// </summary>
    public sealed class CSSQuotesProperty : CSSProperty
    {
        #region Fields

        static readonly Tuple<String, String> _default = new Tuple<String, String>("«", "»");
        List<Tuple<String, String>> _quotes;

        #endregion

        #region ctor

        internal CSSQuotesProperty()
            : base(PropertyNames.Quotes)
        {
            _quotes = new List<Tuple<String, String>>();
            _quotes.Add(_default);
            _inherited = true;
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

        protected override Boolean IsValid(CSSValue value)
        {
            if (value.Is("none"))
                _quotes.Clear();
            else if (value is CSSValueList)
            {
                var values = (CSSValueList)value;

                if (values.Length % 2 != 0)
                    return false;

                var quotes = new List<Tuple<String, String>>();

                for (int i = 0; i < values.Length; i += 2)
                {
                    var open = values[i] as CSSStringValue;
                    var close = values[i + 1] as CSSStringValue;

                    if (open == null || close == null)
                        return false;

                    quotes.Add(Tuple.Create(open.Value, close.Value));
                }

                _quotes = quotes;
            }
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion
    }
}

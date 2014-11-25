namespace AngleSharp.DOM.Css
{
    using AngleSharp.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/quotes
    /// </summary>
    sealed class CSSQuotesProperty : CSSProperty, ICssQuotesProperty
    {
        #region Fields

        static readonly Tuple<String, String>[] _default = new Tuple<String, String>[] { Tuple.Create("«", "»") };
        static readonly Tuple<String, String>[] _none = new Tuple<String, String>[0];
        Tuple<String, String>[] _quotes;

        #endregion

        #region ctor

        internal CSSQuotesProperty(CSSStyleDeclaration rule)
            : base(PropertyNames.Quotes, rule, PropertyFlags.Inherited)
        {
            Reset();
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

        public void SetQuotes(Tuple<String, String>[] quotes)
        {
            _quotes = quotes;
        }

        internal override void Reset()
        {
            _quotes = _default;
        }

        /// <summary>
        /// Determines if the given value represents a valid state of this property.
        /// </summary>
        /// <param name="value">The state that should be used.</param>
        /// <returns>True if the state is valid, otherwise false.</returns>
        protected override Boolean IsValid(CSSValue value)
        {
            return this.TakeOne(Keywords.None, _none).Or(
                this.TakeMany(this.WithString()).Constraint(m => m.Length % 2 == 0).To(TransformArray)).TryConvert(value, SetQuotes);
        }

        #endregion

        #region Helpers

        static Tuple<String, String>[] TransformArray(String[] arrays)
        {
            var tuples = new Tuple<String, String>[arrays.Length / 2];

            for (int i = 0, k = 0; i < arrays.Length; i += 2, k++)
                tuples[k] = Tuple.Create(arrays[i], arrays[i + 1]);

            return tuples;
        }

        #endregion
    }
}

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

        static readonly ValueConverter<QuotesMode> _creator;
        QuotesMode _mode;

        #endregion

        #region ctor

        static CSSQuotesProperty()
        {
            _creator = new ValueConverter<QuotesMode>();
            _creator.AddStatic("none", new NoQuotesMode(), exclusive: true);
            _creator.AddConstructed<StandardQuotesMode>();
            _creator.AddEnumerable<MultiQuotesMode>(2);
        }

        internal CSSQuotesProperty()
            : base(PropertyNames.Quotes)
        {
            _mode = new StandardQuotesMode("«", "»");
            _inherited = true;
        }

        #endregion

        #region Methods

        protected override Boolean IsValid(CSSValue value)
        {
            QuotesMode mode;

            if (_creator.TryCreate(value, out mode))
                _mode = mode;
            else if (value != CSSValue.Inherit)
                return false;

            return true;
        }

        #endregion

        #region Modes

        abstract class QuotesMode
        {
            //TODO Add members that make sense
        }

        /// <summary>
        /// The open-quote and close-quote values of the content
        /// property produce no quotation marks.
        /// </summary>
        sealed class NoQuotesMode : QuotesMode
        {
        }

        /// <summary>
        /// A single pair of quotes. A pair consists of an
        /// open-quote and a close-quote.
        /// </summary>
        sealed class StandardQuotesMode : QuotesMode
        {
            String _open;
            String _closed;

            public StandardQuotesMode(String open, String closed)
            {
                _open = open;
                _closed = closed;
            }
        }

        /// <summary>
        /// One or more pairs of values for open-quote and close-quote.
        /// The first pair represents the outer level of quotation, the
        /// second pair is for the first nested level, next pair for
        /// third level and so on.
        /// </summary>
        sealed class MultiQuotesMode : QuotesMode
        {
            List<QuotesMode> _quotes;

            public MultiQuotesMode(List<QuotesMode> quotes)
            {
                _quotes = quotes;
            }
        }

        #endregion
    }
}

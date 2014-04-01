namespace AngleSharp.DOM.Css.Properties
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Information can be found on MDN:
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/quotes
    /// </summary>
    sealed class CSSQuotesProperty : CSSProperty
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
            _creator.AddMultiple<MultiQuotesMode>();
        }

        public CSSQuotesProperty()
            : base(PropertyNames.QUOTES)
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

        sealed class NoQuotesMode : QuotesMode
        {
        }

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

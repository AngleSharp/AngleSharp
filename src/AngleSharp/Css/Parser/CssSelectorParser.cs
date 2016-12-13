namespace AngleSharp.Css.Parser
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Allows the simply creation of CSS selectors.
    /// </summary>
    public class CssSelectorParser : ICssSelectorParser
    {
        private readonly IAttributeSelectorFactory _attribute;
        private readonly IPseudoClassSelectorFactory _pseudoClass;
        private readonly IPseudoElementSelectorFactory _pseudoElement;

        /// <summary>
        /// Creates a new selector parser.
        /// </summary>
        public CssSelectorParser()
            : this(default(IBrowsingContext))
        {
        }

        /// <summary>
        /// Creates a new selector parser using the different factories.
        /// </summary>
        internal CssSelectorParser(IBrowsingContext context)
        {
            if (context == null)
            {
                context = BrowsingContext.NewFrom<ICssSelectorParser>(this);
            }

            _attribute = context.GetFactory<IAttributeSelectorFactory>();
            _pseudoClass = context.GetFactory<IPseudoClassSelectorFactory>();
            _pseudoElement = context.GetFactory<IPseudoElementSelectorFactory>();
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector ParseSelector(String selectorText)
        {
            var source = new StringSource(selectorText);
            var tokenizer = new CssTokenizer(source);
            var constructor = new CssSelectorConstructor(tokenizer, _attribute, _pseudoClass, _pseudoElement);
            return constructor.Parse();
        }
    }
}

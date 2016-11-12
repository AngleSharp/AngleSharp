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
        /// Creates a new selector parser using the different factories.
        /// </summary>
        public CssSelectorParser(IBrowsingContext context)
        {
            _attribute = context.GetFactory<IAttributeSelectorFactory>();
            _pseudoClass = context.GetFactory<IPseudoClassSelectorFactory>();
            _pseudoElement = context.GetFactory<IPseudoElementSelectorFactory>();
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector ParseSelector(String selectorText)
        {
            var source = new TextSource(selectorText);
            var tokenizer = new CssTokenizer(source);
            var token = tokenizer.Get();
            var constructor = new CssSelectorConstructor(_attribute, _pseudoClass, _pseudoElement);

            while (token.Type != CssTokenType.EndOfFile)
            {
                constructor.Apply(token);
                token = tokenizer.Get();
            }
            
            return constructor.IsValid ? constructor.GetResult() : null;
        }
    }
}

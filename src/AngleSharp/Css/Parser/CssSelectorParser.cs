namespace AngleSharp.Css.Parser
{
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Services;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Allows the simply creation of CSS selectors.
    /// </summary>
    public class CssSelectorParser
    {
        private readonly IAttributeSelectorFactory _attribute;
        private readonly IPseudoClassSelectorFactory _pseudoClass;
        private readonly IPseudoElementSelectorFactory _pseudoElement;

        /// <summary>
        /// Gets the default selector parser.
        /// </summary>
        public static readonly CssSelectorParser Default = new CssSelectorParser(
            Factory.AttributeSelector, Factory.PseudoClassSelector, Factory.PseudoElementSelector);

        /// <summary>
        /// Creates a new selector parser using the different factories.
        /// </summary>
        public CssSelectorParser(IAttributeSelectorFactory attribute, IPseudoClassSelectorFactory pseudoClass, IPseudoElementSelectorFactory pseudoElement)
        {
            _attribute = attribute;
            _pseudoClass = pseudoClass;
            _pseudoElement = pseudoElement;
        }

        /// <summary>
        /// Takes a string and transforms it into a selector object.
        /// </summary>
        public ISelector ParseSelector(String selectorText)
        {
            var source = new TextSource(selectorText);
            var tokenizer = new CssTokenizer(source);
            var token = tokenizer.Get();
            var constructor = Pool.NewSelectorConstructor(_attribute, _pseudoClass, _pseudoElement);

            while (token.Type != CssTokenType.EndOfFile)
            {
                constructor.Apply(token);
                token = tokenizer.Get();
            }

            var valid = constructor.IsValid;
            var result = constructor.ToPool();
            return valid ? result : null;
        }
    }
}

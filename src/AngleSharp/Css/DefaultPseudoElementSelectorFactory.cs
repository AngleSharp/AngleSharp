namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS pseudo element selector instance mappings.
    /// </summary>
    public class DefaultPseudoElementSelectorFactory : IPseudoElementSelectorFactory
    {
        private readonly Dictionary<String, ISelector> _selectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase)
        {
            //TODO
            //- some lack implementation (selection, content, footnote-call, footnote-marker, ...),
            //- some implementations are dubious (first-line, first-letter, ...)
            { PseudoElementNames.Before, new PseudoElementSelector(el => el.IsPseudo(PseudoElementNames.Before), PseudoElementNames.Before) },
            { PseudoElementNames.After, new PseudoElementSelector(el => el.IsPseudo(PseudoElementNames.After), PseudoElementNames.After) },
            { PseudoElementNames.Selection, new PseudoElementSelector(el => false, PseudoElementNames.Selection) },
            { PseudoElementNames.FootnoteCall, new PseudoElementSelector(el => false, PseudoElementNames.FootnoteCall) },
            { PseudoElementNames.FootnoteMarker, new PseudoElementSelector(el => false, PseudoElementNames.FootnoteMarker) },
            { PseudoElementNames.FirstLine, new PseudoElementSelector(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine) },
            { PseudoElementNames.FirstLetter, new PseudoElementSelector(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter) },
            { PseudoElementNames.Content, new PseudoElementSelector(el => false, PseudoElementNames.Content) },
        };

        /// <summary>
        /// Registers a new selector for the specified name.
        /// Throws an exception if another selector for the given
        /// name is already added.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo element.</param>
        /// <param name="selector">The selector to register.</param>
        public void Register(String name, ISelector selector) => _selectors.Add(name, selector);

        /// <summary>
        /// Unregisters an existing selector for the given name.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo element.</param>
        /// <returns>The registered selector, if any.</returns>
        public ISelector Unregister(String name)
        {
            if (_selectors.TryGetValue(name, out var selector))
            {
                _selectors.Remove(name);
            }

            return selector;
        }

        /// <summary>
        /// Creates the default CSS pseudo element selector for the given
        /// name.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <returns>The selector with the given name.</returns>
        protected virtual ISelector CreateDefault(String name) => null;

        /// <summary>
        /// Creates or gets the associated CSS pseudo element selector.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo element.</param>
        /// <returns>The associated selector.</returns>
        public ISelector Create(String name)
        {
            if (_selectors.TryGetValue(name, out var selector))
            {
                return selector;
            }

            return CreateDefault(name);
        }
    }
}

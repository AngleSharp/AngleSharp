namespace AngleSharp.Services.Default
{
    using AngleSharp.Css;
    using AngleSharp.Dom;
    using AngleSharp.Dom.Css;
    using AngleSharp.Extensions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS pseudo element selector instance mappings.
    /// </summary>
    public sealed class PseudoElementSelectorFactory : IPseudoElementSelectorFactory
    {
        readonly Dictionary<String, ISelector> selectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase)
        {
            //TODO some lack implementation (selection, content, ...), some implementations are dubious (first-line, first-letter, ...)
            { PseudoElementNames.Before, SimpleSelector.PseudoElement(el => el.IsPseudo("::" + PseudoElementNames.Before), PseudoElementNames.Before) },
            { PseudoElementNames.After, SimpleSelector.PseudoElement(el => el.IsPseudo("::" + PseudoElementNames.After), PseudoElementNames.After) },
            { PseudoElementNames.Selection, SimpleSelector.PseudoElement(el => false, PseudoElementNames.Selection) },
            { PseudoElementNames.FirstLine, SimpleSelector.PseudoElement(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine) },
            { PseudoElementNames.FirstLetter, SimpleSelector.PseudoElement(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter) },
            { PseudoElementNames.Content, SimpleSelector.PseudoElement(el => false, PseudoElementNames.Content) },
        };

        /// <summary>
        /// Creates or gets the associated CSS pseudo element selector.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo element.</param>
        /// <returns>The associated selector.</returns>
        public ISelector Create(String name)
        {
            var selector = default(ISelector);

            if (selectors.TryGetValue(name, out selector))
            {
                return selector;
            }

            return null;
        }
    }
}

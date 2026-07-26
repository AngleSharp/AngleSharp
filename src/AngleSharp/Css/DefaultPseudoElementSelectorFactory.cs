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
        private readonly Dictionary<String, ISelector> _selectors = new(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoElementNames.Before, CreatePseudoElementSelector(PseudoElementNames.Before) },
            { PseudoElementNames.After, CreatePseudoElementSelector(PseudoElementNames.After) },
            { PseudoElementNames.Selection, CreateUnsupportedPseudoElementSelector(PseudoElementNames.Selection) },
            { PseudoElementNames.FootnoteCall, CreateUnsupportedPseudoElementSelector(PseudoElementNames.FootnoteCall) },
            { PseudoElementNames.FootnoteMarker, CreateUnsupportedPseudoElementSelector(PseudoElementNames.FootnoteMarker) },
            { PseudoElementNames.FirstLine, CreateUnsupportedPseudoElementSelector(PseudoElementNames.FirstLine) },
            { PseudoElementNames.FirstLetter, CreateUnsupportedPseudoElementSelector(PseudoElementNames.FirstLetter) },
            { PseudoElementNames.Content, CreateUnsupportedPseudoElementSelector(PseudoElementNames.Content) },
            { PseudoElementNames.Checkmark, CreatePseudoElementSelector(PseudoElementNames.Checkmark) },
            { PseudoElementNames.PickerIcon, CreatePseudoElementSelector(PseudoElementNames.PickerIcon) },
        };

        private static ISelector CreatePseudoElementSelector(String name) => new PseudoElementSelector(el => el.IsPseudo(name), name);

        private static ISelector CreateUnsupportedPseudoElementSelector(String name) => new PseudoElementSelector(_ => false, name);

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
        public ISelector? Unregister(String name)
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
        protected virtual ISelector? CreateDefault(String name) => null;

        /// <summary>
        /// Creates or gets the associated CSS pseudo element selector.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo element.</param>
        /// <returns>The associated selector.</returns>
        public ISelector? Create(String name)
        {
            if (_selectors.TryGetValue(name, out var selector))
            {
                return selector;
            }

            return CreateDefault(name);
        }
    }
}

namespace AngleSharp.Css
{
    using AngleSharp.Css.Dom;
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides string to CSS pseudo class selector instance mappings.
    /// </summary>
    public class DefaultPseudoClassSelectorFactory : IPseudoClassSelectorFactory
    {
        private readonly Dictionary<String, ISelector> _selectors = new Dictionary<String, ISelector>(StringComparer.OrdinalIgnoreCase)
        {
            { PseudoClassNames.Root, new PseudoClassSelector(el => el.Owner.DocumentElement == el, PseudoClassNames.Root) },
            { PseudoClassNames.Scope, ScopePseudoClassSelector.Instance },
            { PseudoClassNames.OnlyType, new PseudoClassSelector(el => el.IsOnlyOfType(), PseudoClassNames.OnlyType) },
            { PseudoClassNames.FirstOfType, new PseudoClassSelector(el => el.IsFirstOfType(), PseudoClassNames.FirstOfType) },
            { PseudoClassNames.LastOfType, new PseudoClassSelector(el => el.IsLastOfType(), PseudoClassNames.LastOfType) },
            { PseudoClassNames.OnlyChild, new PseudoClassSelector(el => el.IsOnlyChild(), PseudoClassNames.OnlyChild) },
            { PseudoClassNames.FirstChild, new PseudoClassSelector(el => el.IsFirstChild(), PseudoClassNames.FirstChild) },
            { PseudoClassNames.LastChild, new PseudoClassSelector(el => el.IsLastChild(), PseudoClassNames.LastChild) },
            { PseudoClassNames.Empty, new PseudoClassSelector(el => el.ChildElementCount == 0 && el.TextContent.Is(String.Empty), PseudoClassNames.Empty) },
            { PseudoClassNames.AnyLink, new PseudoClassSelector(el => el.IsLink() || el.IsVisited(), PseudoClassNames.AnyLink) },
            { PseudoClassNames.Link, new PseudoClassSelector(el => el.IsLink(), PseudoClassNames.Link) },
            { PseudoClassNames.Visited, new PseudoClassSelector(el => el.IsVisited(), PseudoClassNames.Visited) },
            { PseudoClassNames.Active, new PseudoClassSelector(el => el.IsActive(), PseudoClassNames.Active) },
            { PseudoClassNames.Hover, new PseudoClassSelector(el => el.IsHovered(), PseudoClassNames.Hover) },
            { PseudoClassNames.Focus, new PseudoClassSelector(el => el.IsFocused, PseudoClassNames.Focus) },
            { PseudoClassNames.Target, new PseudoClassSelector(el => el.IsTarget(), PseudoClassNames.Target) },
            { PseudoClassNames.Enabled, new PseudoClassSelector(el => el.IsEnabled(), PseudoClassNames.Enabled) },
            { PseudoClassNames.Disabled, new PseudoClassSelector(el => el.IsDisabled(), PseudoClassNames.Disabled) },
            { PseudoClassNames.Default, new PseudoClassSelector(el => el.IsDefault(), PseudoClassNames.Default) },
            { PseudoClassNames.Checked, new PseudoClassSelector(el => el.IsChecked(), PseudoClassNames.Checked) },
            { PseudoClassNames.Indeterminate, new PseudoClassSelector(el => el.IsIndeterminate(), PseudoClassNames.Indeterminate) },
            { PseudoClassNames.PlaceholderShown, new PseudoClassSelector(el => el.IsPlaceholderShown(), PseudoClassNames.PlaceholderShown) },
            { PseudoClassNames.Unchecked, new PseudoClassSelector(el => el.IsUnchecked(), PseudoClassNames.Unchecked) },
            { PseudoClassNames.Valid, new PseudoClassSelector(el => el.IsValid(), PseudoClassNames.Valid) },
            { PseudoClassNames.Invalid, new PseudoClassSelector(el => el.IsInvalid(), PseudoClassNames.Invalid) },
            { PseudoClassNames.Required, new PseudoClassSelector(el => el.IsRequired(), PseudoClassNames.Required) },
            { PseudoClassNames.ReadOnly, new PseudoClassSelector(el => el.IsReadOnly(), PseudoClassNames.ReadOnly) },
            { PseudoClassNames.ReadWrite, new PseudoClassSelector(el => el.IsEditable(), PseudoClassNames.ReadWrite) },
            { PseudoClassNames.InRange, new PseudoClassSelector(el => el.IsInRange(), PseudoClassNames.InRange) },
            { PseudoClassNames.OutOfRange, new PseudoClassSelector(el => el.IsOutOfRange(), PseudoClassNames.OutOfRange) },
            { PseudoClassNames.Optional, new PseudoClassSelector(el => el.IsOptional(), PseudoClassNames.Optional) },
            { PseudoClassNames.Shadow, new PseudoClassSelector(el => el.IsShadow(), PseudoClassNames.Shadow) },
            { PseudoElementNames.Before, new PseudoClassSelector(el => el.IsPseudo(PseudoElementNames.Before), PseudoElementNames.Before) },
            { PseudoElementNames.After, new PseudoClassSelector(el => el.IsPseudo(PseudoElementNames.After), PseudoElementNames.After) },
            { PseudoElementNames.FirstLine, new PseudoClassSelector(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine) },
            { PseudoElementNames.FirstLetter, new PseudoClassSelector(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter) },
        };

        /// <summary>
        /// Registers a new selector for the specified name.
        /// Throws an exception if another selector for the given
        /// name is already added.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <param name="selector">The selector to register.</param>
        public void Register(String name, ISelector selector) => _selectors.Add(name, selector);

        /// <summary>
        /// Unregisters an existing selector for the given name.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
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
        /// Creates the default CSS pseudo class selector for the given
        /// name.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <returns>The selector with the given name.</returns>
        protected virtual ISelector CreateDefault(String name) => null;

        /// <summary>
        /// Creates or gets the associated CSS pseudo class selector.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
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

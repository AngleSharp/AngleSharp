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
            { PseudoClassNames.Root, SimpleSelector.PseudoClass(el => el.Owner.DocumentElement == el, PseudoClassNames.Root) },
            { PseudoClassNames.Scope, ScopePseudoClassSelector.Instance },
            { PseudoClassNames.OnlyType, SimpleSelector.PseudoClass(el => el.IsOnlyOfType(), PseudoClassNames.OnlyType) },
            { PseudoClassNames.FirstOfType, SimpleSelector.PseudoClass(el => el.IsFirstOfType(), PseudoClassNames.FirstOfType) },
            { PseudoClassNames.LastOfType, SimpleSelector.PseudoClass(el => el.IsLastOfType(), PseudoClassNames.LastOfType) },
            { PseudoClassNames.OnlyChild, SimpleSelector.PseudoClass(el => el.IsOnlyChild(), PseudoClassNames.OnlyChild) },
            { PseudoClassNames.FirstChild, SimpleSelector.PseudoClass(el => el.IsFirstChild(), PseudoClassNames.FirstChild) },
            { PseudoClassNames.LastChild, SimpleSelector.PseudoClass(el => el.IsLastChild(), PseudoClassNames.LastChild) },
            { PseudoClassNames.Empty, SimpleSelector.PseudoClass(el => el.ChildElementCount == 0 && el.TextContent.Is(String.Empty), PseudoClassNames.Empty) },
            { PseudoClassNames.AnyLink, SimpleSelector.PseudoClass(el => el.IsLink() || el.IsVisited(), PseudoClassNames.AnyLink) },
            { PseudoClassNames.Link, SimpleSelector.PseudoClass(el => el.IsLink(), PseudoClassNames.Link) },
            { PseudoClassNames.Visited, SimpleSelector.PseudoClass(el => el.IsVisited(), PseudoClassNames.Visited) },
            { PseudoClassNames.Active, SimpleSelector.PseudoClass(el => el.IsActive(), PseudoClassNames.Active) },
            { PseudoClassNames.Hover, SimpleSelector.PseudoClass(el => el.IsHovered(), PseudoClassNames.Hover) },
            { PseudoClassNames.Focus, SimpleSelector.PseudoClass(el => el.IsFocused, PseudoClassNames.Focus) },
            { PseudoClassNames.Target, SimpleSelector.PseudoClass(el => el.IsTarget(), PseudoClassNames.Target) },
            { PseudoClassNames.Enabled, SimpleSelector.PseudoClass(el => el.IsEnabled(), PseudoClassNames.Enabled) },
            { PseudoClassNames.Disabled, SimpleSelector.PseudoClass(el => el.IsDisabled(), PseudoClassNames.Disabled) },
            { PseudoClassNames.Default, SimpleSelector.PseudoClass(el => el.IsDefault(), PseudoClassNames.Default) },
            { PseudoClassNames.Checked, SimpleSelector.PseudoClass(el => el.IsChecked(), PseudoClassNames.Checked) },
            { PseudoClassNames.Indeterminate, SimpleSelector.PseudoClass(el => el.IsIndeterminate(), PseudoClassNames.Indeterminate) },
            { PseudoClassNames.PlaceholderShown, SimpleSelector.PseudoClass(el => el.IsPlaceholderShown(), PseudoClassNames.PlaceholderShown) },
            { PseudoClassNames.Unchecked, SimpleSelector.PseudoClass(el => el.IsUnchecked(), PseudoClassNames.Unchecked) },
            { PseudoClassNames.Valid, SimpleSelector.PseudoClass(el => el.IsValid(), PseudoClassNames.Valid) },
            { PseudoClassNames.Invalid, SimpleSelector.PseudoClass(el => el.IsInvalid(), PseudoClassNames.Invalid) },
            { PseudoClassNames.Required, SimpleSelector.PseudoClass(el => el.IsRequired(), PseudoClassNames.Required) },
            { PseudoClassNames.ReadOnly, SimpleSelector.PseudoClass(el => el.IsReadOnly(), PseudoClassNames.ReadOnly) },
            { PseudoClassNames.ReadWrite, SimpleSelector.PseudoClass(el => el.IsEditable(), PseudoClassNames.ReadWrite) },
            { PseudoClassNames.InRange, SimpleSelector.PseudoClass(el => el.IsInRange(), PseudoClassNames.InRange) },
            { PseudoClassNames.OutOfRange, SimpleSelector.PseudoClass(el => el.IsOutOfRange(), PseudoClassNames.OutOfRange) },
            { PseudoClassNames.Optional, SimpleSelector.PseudoClass(el => el.IsOptional(), PseudoClassNames.Optional) },
            { PseudoClassNames.Shadow, SimpleSelector.PseudoClass(el => el.IsShadow(), PseudoClassNames.Shadow) },
            { PseudoElementNames.Before, SimpleSelector.PseudoClass(el => el.IsPseudo(PseudoElementNames.Before), PseudoElementNames.Before) },
            { PseudoElementNames.After, SimpleSelector.PseudoClass(el => el.IsPseudo(PseudoElementNames.After), PseudoElementNames.After) },
            { PseudoElementNames.FirstLine, SimpleSelector.PseudoClass(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text, PseudoElementNames.FirstLine) },
            { PseudoElementNames.FirstLetter, SimpleSelector.PseudoClass(el => el.HasChildNodes && el.ChildNodes[0].NodeType == NodeType.Text && el.ChildNodes[0].TextContent.Length > 0, PseudoElementNames.FirstLetter) },
        };

        /// <summary>
        /// Registers a new selector for the specified name.
        /// Throws an exception if another selector for the given
        /// name is already added.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <param name="selector">The selector to register.</param>
        public void Register(String name, ISelector selector)
        {
            _selectors.Add(name, selector);
        }

        /// <summary>
        /// Unregisters an existing selector for the given name.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <returns>The registered selector, if any.</returns>
        public ISelector Unregister(String name)
        {
            var selector = default(ISelector);

            if (_selectors.TryGetValue(name, out selector))
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
        protected virtual ISelector CreateDefault(String name)
        {
            return null;
        }

        /// <summary>
        /// Creates or gets the associated CSS pseudo class selector.
        /// </summary>
        /// <param name="name">The name of the CSS pseudo class.</param>
        /// <returns>The associated selector.</returns>
        public ISelector Create(String name)
        {
            var selector = default(ISelector);

            if (_selectors.TryGetValue(name, out selector))
            {
                return selector;
            }

            return CreateDefault(name);
        }
    }
}

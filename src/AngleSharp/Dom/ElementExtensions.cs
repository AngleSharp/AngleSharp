namespace AngleSharp.Dom
{
    using AngleSharp.Common;
    using AngleSharp.Css.Dom;
    using AngleSharp.Html;
    using AngleSharp.Html.Dom;
    using AngleSharp.Io;
    using AngleSharp.Io.Processors;
    using AngleSharp.Media;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful methods for element objects.
    /// </summary>
    public static class ElementExtensions
    {
        /// <summary>
        /// Locates the prefix of the given namespace.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="namespaceUri">The url of the namespace.</param>
        /// <returns>The prefix or null, if the namespace could not be found.</returns>
        public static String LocatePrefixFor(this IElement element, String namespaceUri)
        {
            if (element.NamespaceUri.Is(namespaceUri) && element.Prefix != null)
            {
                return element.Prefix;
            }

            foreach (var attr in element.Attributes)
            {
                if (attr.Prefix.Is(NamespaceNames.XmlNsPrefix) && attr.Value.Is(namespaceUri))
                {
                    return attr.LocalName;
                }
            }

            return element.ParentElement?.LocatePrefixFor(namespaceUri);
        }

        /// <summary>
        /// Locates the namespace of the given prefix.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        /// <returns>The url of the namespace or null, if the prefix could not be found.</returns>
        public static String LocateNamespaceFor(this IElement element, String prefix)
        {
            var ns = element.NamespaceUri;
            var px = element.Prefix;

            if (String.IsNullOrEmpty(ns) || !px.Is(prefix))
            {
                var success = false;

                if (prefix == null)
                {
                    success = element.TryLocateStandardNamespace(out ns);
                }
                else
                {
                    success = element.TryLocateCustomNamespace(prefix, out ns);
                }

                if (!success)
                {
                    ns = element.ParentElement?.LocateNamespaceFor(prefix);
                }
            }

            return ns;
        }

        /// <summary>
        /// Gets the non-fixed namespace of the element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The url of the namespace.</returns>
        public static String GetNamespaceUri(this IElement element)
        {
            var prefix = element.Prefix;
            var success = false;
            var ns = String.Empty;

            if (prefix == null)
            {
                success = element.TryLocateStandardNamespace(out ns);
            }
            else
            {
                success = element.TryLocateCustomNamespace(prefix, out ns);
            }

            if (!success)
            {
                ns = element.ParentElement?.LocateNamespaceFor(prefix);
            }

            return ns;
        }

        /// <summary>
        /// Tries to locate a custom namespace URI.
        /// </summary>
        /// <param name="element">The element to locate the namespace URI for.</param>
        /// <param name="prefix">The prefix of the custom namespace.</param>
        /// <param name="namespaceUri">The located namespace URI.</param>
        /// <returns>True if the namespace URI could be located, otherwise false.</returns>
        public static Boolean TryLocateCustomNamespace(this IElement element, String prefix, out String namespaceUri)
        {
            foreach (var attr in element.Attributes)
            {
                if (attr.NamespaceUri.Is(NamespaceNames.XmlNsUri) && attr.Prefix.Is(NamespaceNames.XmlNsPrefix) && attr.LocalName.Is(prefix))
                {
                    var value = attr.Value;

                    if (String.IsNullOrEmpty(value))
                    {
                        value = null;
                    }

                    namespaceUri = value;
                    return true;
                }
            }

            namespaceUri = null;
            return false;
        }

        /// <summary>
        /// Tries to locate the standard namespace URI.
        /// </summary>
        /// <param name="element">The element to locate the namespace URI for.</param>
        /// <param name="namespaceUri">The located namespace URI.</param>
        /// <returns>True if the namespace URI could be located, otherwise false.</returns>
        public static Boolean TryLocateStandardNamespace(this IElement element, out String namespaceUri)
        {
            foreach (var attr in element.Attributes)
            {
                if (attr.Prefix == null && attr.LocalName.Is(NamespaceNames.XmlNsPrefix))
                {
                    var value = attr.Value;

                    if (String.IsNullOrEmpty(value))
                    {
                        value = null;
                    }

                    namespaceUri = value;
                    return true;
                }
            }

            namespaceUri = null;
            return false;
        }

        /// <summary>
        /// Creates a new resource request from the provided element for the
        /// given url.
        /// </summary>
        /// <param name="element">The element creating a request.</param>
        /// <param name="url">The address that specifies the target.</param>
        /// <returns>The new resource request with the information.</returns>
        public static ResourceRequest CreateRequestFor(this IElement element, Url url)
        {
            return new ResourceRequest(element, url);
        }

        /// <summary>
        /// Checks if the element with the provided prefix matches the CSS
        /// namespace.
        /// </summary>
        /// <param name="el">The element to examine.</param>
        /// <param name="prefix">The namespace in question.</param>
        /// <returns>True if the namespace is matched, else false.</returns>
        public static Boolean MatchesCssNamespace(this IElement el, String prefix)
        {
            if (!prefix.Is("*"))
            {
                var nsUri = el.GetAttribute(NamespaceNames.XmlNsPrefix) ?? el.NamespaceUri;

                if (prefix.Is(String.Empty))
                {
                    return nsUri.Is(String.Empty);
                }

                return nsUri.Is(GetCssNamespace(el, prefix));
            }

            return true;
        }

        /// <summary>
        /// Gets the CSS namespace that is defined via the provided prefix.
        /// </summary>
        /// <param name="el">The element that is connected to a doc.</param>
        /// <param name="prefix">The prefix to lookup.</param>
        /// <returns>The namespace url for the prefix.</returns>
        public static String GetCssNamespace(this IElement el, String prefix)
        {
            return el.Owner?.StyleSheets.LocateNamespace(prefix) ?? el.LocateNamespaceFor(prefix);
        }

        /// <summary>
        /// Checks if the element is currently hovered.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently hovered, otherwise false.</returns>
        public static Boolean IsHovered(this IElement element)
        {
            //TODO Connection to Visual Tree ?
            return false;
        }

        /// <summary>
        /// Checks if the element is the only of its type among the parent's children.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is the only of its type among its siblings.</returns>
        public static Boolean IsOnlyOfType(this IElement element)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i].NodeName.Is(element.NodeName) && parent.ChildNodes[i] != element)
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is the first of its type among the parent's children.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is the first of its type among its siblings.</returns>
        public static Boolean IsFirstOfType(this IElement element)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                for (var i = 0; i < parent.ChildNodes.Length; i++)
                {
                    if (parent.ChildNodes[i].NodeName.Is(element.NodeName))
                    {
                        return parent.ChildNodes[i] == element;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is the last of its type among the parent's children.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is the last of its type among its siblings.</returns>
        public static Boolean IsLastOfType(this IElement element)
        {
            var parent = element.ParentElement;

            if (parent != null)
            {
                for (var i = parent.ChildNodes.Length - 1; i >= 0; i--)
                {
                    if (parent.ChildNodes[i].NodeName.Is(element.NodeName))
                    {
                        return parent.ChildNodes[i] == element;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently targeted.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element's ID is equal to the hash.</returns>
        public static Boolean IsTarget(this IElement element)
        {
            var id = element.Id;
            var hash = element.Owner?.Location.Hash;
            return id != null && hash != null && String.Compare(id, 0, hash, hash.Length > 0 ? 1 : 0, Int32.MaxValue) == 0;
        }

        /// <summary>
        /// Checks if the element is currently enabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently enabled, otherwise false.</returns>
        public static Boolean IsEnabled(this IElement element)
        {
            if (element is HtmlAnchorElement || element is HtmlAreaElement || element is HtmlLinkElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                return !String.IsNullOrEmpty(href);
            }
            else if (element is HtmlButtonElement)
            {
                return !((HtmlButtonElement)element).IsDisabled;
            }
            else if (element is HtmlInputElement)
            {
                return !((HtmlInputElement)element).IsDisabled;
            }
            else if (element is HtmlSelectElement)
            {
                return !((HtmlSelectElement)element).IsDisabled;
            }
            else if (element is HtmlTextAreaElement)
            {
                return !((HtmlTextAreaElement)element).IsDisabled;
            }
            else if (element is HtmlOptionElement)
            {
                return !((HtmlOptionElement)element).IsDisabled;
            }
            else if (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement)
            {
                var isDisabled = element.GetAttribute(null, AttributeNames.Disabled);
                return String.IsNullOrEmpty(isDisabled);
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently disabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently disabled, otherwise false.</returns>
        public static Boolean IsDisabled(this IElement element)
        {
            if (element is HtmlButtonElement)
            {
                return ((HtmlButtonElement)element).IsDisabled;
            }
            else if (element is HtmlInputElement)
            {
                return ((HtmlInputElement)element).IsDisabled;
            }
            else if (element is HtmlSelectElement)
            {
                return ((HtmlSelectElement)element).IsDisabled;
            }
            else if (element is HtmlTextAreaElement)
            {
                return ((HtmlTextAreaElement)element).IsDisabled;
            }
            else if (element is HtmlOptionElement)
            {
                return ((HtmlOptionElement)element).IsDisabled;
            }
            else if (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement)
            {
                var isDisabled = element.GetAttribute(null, AttributeNames.Disabled);
                return !String.IsNullOrEmpty(isDisabled);
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is an input element that is currently in its default state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently in its default state, otherwise false.</returns>
        public static Boolean IsDefault(this IElement element)
        {
            if (element is HtmlButtonElement)
            {
                var bt = (HtmlButtonElement)element;
                var form = bt.Form;

                //TODO Check if button is form def. button
                if (form != null)
                {
                    return true;
                }
            }
            else if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var type = input.Type;

                if (type == InputTypeNames.Submit || type == InputTypeNames.Image)
                {
                    var form = input.Form;

                    //TODO Check if input is form def. button
                    if (form != null)
                    {
                        return true;
                    }
                }
                else
                {
                    //TODO input that are checked and can be checked ...
                }
            }
            else if (element is HtmlOptionElement)
            {
                var value = element.GetAttribute(null, AttributeNames.Selected);
                return !String.IsNullOrEmpty(value);
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is a pseudo element (before or after).
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <param name="name">The pseudo element's name.</param>
        /// <returns>True if the element is a pseudo element, otherwise false.</returns>
        public static Boolean IsPseudo(this IElement element, String name)
        {
            var pseudoElement = element as IPseudoElement;
            return pseudoElement?.PseudoName.Is(name) ?? false;
        }

        /// <summary>
        /// Checks if the element is currently checked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently checked, otherwise false.</returns>
        public static Boolean IsChecked(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var type = input.Type;
                var canBeChecked = type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio);
                return canBeChecked && input.IsChecked;
            }
            else if (element is HtmlMenuItemElement)
            {
                var menuItem = (HtmlMenuItemElement)element;
                var type = menuItem.Type;
                var canBeChecked = type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio);
                return canBeChecked && menuItem.IsChecked;
            }
            else if (element is HtmlOptionElement)
            {
                var option = ((HtmlOptionElement)element);
                return option.IsSelected;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently in its indeterminate state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently indeterminate, otherwise false.</returns>
        public static Boolean IsIndeterminate(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var isCheckbox = input.Type.Is(InputTypeNames.Checkbox);
                return isCheckbox && input.IsIndeterminate;
            }
            else if (element is HtmlProgressElement)
            {
                var value = element.GetAttribute(null, AttributeNames.Value);
                return String.IsNullOrEmpty(value);
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently showing a placeholder.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently showing a placeholder, otherwise false.</returns>
        public static Boolean IsPlaceholderShown(this IElement element)
        {
            var input = element as HtmlInputElement;

            if (input != null)
            {
                var containsPlaceholder = !String.IsNullOrEmpty(input.Placeholder);
                var isEmpty = String.IsNullOrEmpty(input.Value);
                return containsPlaceholder && isEmpty;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently unchecked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently unchecked, otherwise false.</returns>
        public static Boolean IsUnchecked(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var type = input.Type;
                var canBeChecked = type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio);
                return canBeChecked && !input.IsChecked;
            }
            else if (element is HtmlMenuItemElement)
            {
                var menuItem = (HtmlMenuItemElement)element;
                var type = menuItem.Type;
                var canBeChecked = type.IsOneOf(InputTypeNames.Checkbox, InputTypeNames.Radio);
                return canBeChecked && !menuItem.IsChecked;
            }
            else if (element is HtmlOptionElement)
            {
                var option = (HtmlOptionElement)element;
                return !option.IsSelected;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is currently active.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently active, otherwise false.</returns>
        public static Boolean IsActive(this IElement element)
        {
            if (element is HtmlAnchorElement)
            {
                var anchor = (HtmlAnchorElement)element;
                var href = element.GetAttribute(null, AttributeNames.Href);
                return !String.IsNullOrEmpty(href) && anchor.IsActive;
            }
            else if (element is HtmlAreaElement)
            {
                var area = (HtmlAreaElement)element;
                var href = element.GetAttribute(null, AttributeNames.Href);
                return !String.IsNullOrEmpty(href) && area.IsActive;
            }
            else if (element is HtmlLinkElement)
            {
                var link = (HtmlLinkElement)element;
                var href = element.GetAttribute(null, AttributeNames.Href);
                return !String.IsNullOrEmpty(href) && link.IsActive;
            }
            else if (element is HtmlButtonElement)
            {
                var button = (HtmlButtonElement)element;
                return !button.IsDisabled && button.IsActive;
            }
            else if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var type = input.Type;
                var canBeSubmitted = type.IsOneOf(InputTypeNames.Submit, InputTypeNames.Image, InputTypeNames.Reset, InputTypeNames.Button);
                return canBeSubmitted && input.IsActive;
            }
            else if (element is HtmlMenuItemElement)
            {
                var menuItem = (HtmlMenuItemElement)element;
                return !menuItem.IsDisabled && menuItem.IsActive;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element has already been visited.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element has been visited, otherwise false.</returns>
        public static Boolean IsVisited(this IElement element)
        {
            if (element is HtmlAnchorElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var anchor = (HtmlAnchorElement)element;
                return !String.IsNullOrEmpty(href) && anchor.IsVisited;
            }
            else if (element is HtmlAreaElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var area = (HtmlAreaElement)element;
                return !String.IsNullOrEmpty(href) && area.IsVisited;
            }
            else if (element is HtmlLinkElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var link = (HtmlLinkElement)element;
                return !String.IsNullOrEmpty(href) && link.IsVisited;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is a link.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is a link, otherwise false.</returns>
        public static Boolean IsLink(this IElement element)
        {
            if (element is HtmlAnchorElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var anchor = (HtmlAnchorElement)element;
                return !String.IsNullOrEmpty(href) && !anchor.IsVisited;
            }
            else if (element is HtmlAreaElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var area = (HtmlAreaElement)element;
                return !String.IsNullOrEmpty(href) && !area.IsVisited;
            }
            else if (element is HtmlLinkElement)
            {
                var href = element.GetAttribute(null, AttributeNames.Href);
                var link = (HtmlLinkElement)element;
                return !String.IsNullOrEmpty(href) && !link.IsVisited;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element hosts a shadow tree.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element hosts a shadow tree.</returns>
        public static Boolean IsShadow(this IElement element)
        {
            return element?.ShadowRoot != null;
        }

        /// <summary>
        /// Checks if the element is only optional and not required.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is optional, otherwise false.</returns>
        public static Boolean IsOptional(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                return !input.IsRequired;
            }
            else if (element is HtmlSelectElement)
            {
                var select = (HtmlSelectElement)element;
                return !select.IsRequired;
            }
            else if (element is HtmlTextAreaElement)
            {
                var area = (HtmlTextAreaElement)element;
                return !area.IsRequired;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is required and must be filled out.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is required, otherwise false.</returns>
        public static Boolean IsRequired(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                return input.IsRequired;
            }
            else if (element is HtmlSelectElement)
            {
                var select = (HtmlSelectElement)element;
                return select.IsRequired;
            }
            else if (element is HtmlTextAreaElement)
            {
                var textArea = (HtmlTextAreaElement)element;
                return textArea.IsRequired;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element does not validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is invalid, otherwise false.</returns>
        public static Boolean IsInvalid(this IElement element)
        {
            if (element is IValidation)
            {
                var validator = (IValidation)element;
                return !validator.CheckValidity();
            }
            else if (element is HtmlFormElement)
            {
                var form = (HtmlFormElement)element;
                return !form.CheckValidity();
            }

            return false;
        }

        /// <summary>
        /// Checks if the element does validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is valid, otherwise false.</returns>
        public static Boolean IsValid(this IElement element)
        {
            if (element is IValidation)
            {
                var validator = (IValidation)element;
                return validator.CheckValidity();
            }
            else if (element is HtmlFormElement)
            {
                var form = (HtmlFormElement)element;
                return form.CheckValidity();
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is readonly.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is readonly, otherwise false.</returns>
        public static Boolean IsReadOnly(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                return !input.IsMutable;
            }
            else if (element is HtmlTextAreaElement)
            {
                var textArea = (HtmlTextAreaElement)element;
                return !textArea.IsMutable;
            }
            else if (element is IHtmlElement)
            {
                var general = (IHtmlElement)element;
                return !general.IsContentEditable;
            }

            return true;
        }

        /// <summary>
        /// Checks if the element is editable.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element can be edited, otherwise false.</returns>
        public static Boolean IsEditable(this IElement element)
        {
            if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                return input.IsMutable;
            }
            else if (element is HtmlTextAreaElement)
            {
                var textArea = (HtmlTextAreaElement)element;
                return textArea.IsMutable;
            }
            else if (element is IHtmlElement)
            {
                var general = (IHtmlElement)element;
                return general.IsContentEditable;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element's value is out-of-range.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is invalid, otherwise false.</returns>
        public static Boolean IsOutOfRange(this IElement element)
        {
            var validation = element as IValidation;

            if (validation != null)
            {
                var state = validation.Validity;
                return state.IsRangeOverflow || state.IsRangeUnderflow;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element's value is within the range.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is valid, otherwise false.</returns>
        public static Boolean IsInRange(this IElement element)
        {
            var validation = element as IValidation;

            if (validation != null)
            {
                var state = validation.Validity;
                return !state.IsRangeOverflow && !state.IsRangeUnderflow;
            }

            return false;
        }

        /// <summary>
        /// Checks if the element is its parent's only child.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element has no siblings, otherwise false.</returns>
        public static Boolean IsOnlyChild(this IElement element)
        {
            var parent = element.ParentElement;
            return parent != null && parent.ChildElementCount == 1 && parent.FirstElementChild == element;
        }

        /// <summary>
        /// Checks if the element is its parent's first child.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element's first child is the given one, otherwise false.</returns>
        public static Boolean IsFirstChild(this IElement element)
        {
            var parent = element.ParentElement;
            return parent?.FirstElementChild == element;
        }

        /// <summary>
        /// Checks if the element is its parent's last child.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element's last child is the given one, otherwise false.</returns>
        public static Boolean IsLastChild(this IElement element)
        {
            var parent = element.ParentElement;
            return parent?.LastElementChild == element;
        }

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <param name="attributeValue">The value of the attribute.</param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, String attributeName, String attributeValue)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (attributeName == null)
                throw new ArgumentNullException(nameof(attributeName));

            foreach (var element in elements)
            {
                element.SetAttribute(attributeName, attributeValue);
            }

            return elements;
        }

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="attributes">
        /// An enumeration of attributes in form of key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, IEnumerable<KeyValuePair<String, String>> attributes)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            foreach (var element in elements)
            {
                foreach (var attribute in attributes)
                {
                    element.SetAttribute(attribute.Key, attribute.Value);
                }
            }

            return elements;
        }

        /// <summary>
        /// Sets the specified attribute name to the specified value for all
        /// elements in the given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection of elements.</param>
        /// <param name="attributes">
        /// An enumeration of attributes in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Attr<T>(this T elements, Object attributes)
            where T : IEnumerable<IElement>
        {
            var realAttributes = attributes.ToDictionary();
            return elements.Attr(realAttributes);
        }

        /// <summary>
        /// Gets the values of the specified attribute for all elements in the
        /// given collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection of elements.</param>
        /// <param name="attributeName">The name of the attribute.</param>
        /// <returns>The attributes' values.</returns>
        public static IEnumerable<String> Attr<T>(this T elements, String attributeName)
            where T : IEnumerable<IElement>
        {
            return elements.Select(m => m.GetAttribute(attributeName));
        }

        /// <summary>
        /// Clears the attributes of the given element.
        /// </summary>
        /// <param name="element">The element to clear.</param>
        /// <returns>The element itself.</returns>
        public static IElement ClearAttr(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.Attributes.Clear();
            return element;
        }

        /// <summary>
        /// Clears the attributes of all elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection to clear.</param>
        /// <returns>The collection itself.</returns>
        public static T ClearAttr<T>(this T elements)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                element.ClearAttr();
            }

            return elements;
        }

        /// <summary>
        /// Empties the provided element.
        /// </summary>
        /// <param name="element">The element to empty.</param>
        /// <returns>The element itself.</returns>
        public static IElement Empty(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            element.InnerHtml = String.Empty;
            return element;
        }

        /// <summary>
        /// Empties all provided elements.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The collection itself.</returns>
        public static T Empty<T>(this T elements)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                element.Empty();
            }

            return elements;
        }

        /// <summary>
        /// Gets the inner HTML of the given element.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>The source code of the inner HTML.</returns>
        public static String Html(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            return element.InnerHtml;
        }

        /// <summary>
        /// Sets the inner HTML of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="html">
        /// The source code of the inner HTML to set.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Html<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                element.InnerHtml = html;
            }

            return elements;
        }

        /// <summary>
        /// Adds the specified class name(s) for all elements in the given
        /// collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T AddClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (className == null)
                throw new ArgumentNullException(nameof(className));

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                element.ClassList.Add(classes);
            }

            return elements;
        }

        /// <summary>
        /// Removes the specified class name(s) for all elements in the given
        /// collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T RemoveClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (className == null)
                throw new ArgumentNullException(nameof(className));

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                element.ClassList.Remove(classes);
            }

            return elements;
        }

        /// <summary>
        /// Toggles the specified class name(s) for all elements in the given
        /// collection.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>The collection itself.</returns>
        public static T ToggleClass<T>(this T elements, String className)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (className == null)
                throw new ArgumentNullException(nameof(className));

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                foreach (var @class in classes)
                {
                    element.ClassList.Toggle(@class);
                }
            }

            return elements;
        }

        /// <summary>
        /// Checks if any element in the given collection has the given
        /// class(es).
        /// </summary>
        /// <param name="elements">The collection of elements.</param>
        /// <param name="className">The name(s) of the class(es).</param>
        /// <returns>
        /// True if any element has the class(es), otherwise false.
        /// </returns>
        public static Boolean HasClass(this IEnumerable<IElement> elements, String className)
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (className == null)
                throw new ArgumentNullException(nameof(className));

            var classes = className.SplitSpaces();

            foreach (var element in elements)
            {
                var found = true;

                foreach (var @class in classes)
                {
                    if (!element.ClassList.Contains(@class))
                    {
                        found = false;
                        break;
                    }
                }

                if (found)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Inserts the nodes generated from the given HTML code before
        /// each element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Before<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var parent = element.ParentElement;

                if (parent != null)
                {
                    var fragment = parent.CreateFragment(html);
                    parent.InsertBefore(fragment, element);
                }
            }

            return elements;
        }

        /// <summary>
        /// Inserts the nodes generated from the given HTML code after
        /// each element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T After<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var parent = element.ParentElement;

                if (parent != null)
                {
                    var fragment = parent.CreateFragment(html);
                    parent.InsertBefore(fragment, element.NextSibling);
                }
            }

            return elements;
        }

        /// <summary>
        /// Appends the nodes generated from the given HTML code to each
        /// element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Append<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                element.Append(fragment);
            }

            return elements;
        }

        /// <summary>
        /// Prepends the nodes generated from the given HTML code to each
        /// element of the provided elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the nodes.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Prepend<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                element.InsertBefore(fragment, element.FirstChild);
            }

            return elements;
        }

        /// <summary>
        /// Wraps the given elements in the inner most element of the tree
        /// generated form the provided HTML code.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T Wrap<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();
                var parent = element.Parent;
                parent?.InsertBefore(fragment, element);
                newParent.AppendChild(element);
            }

            return elements;
        }

        /// <summary>
        /// Wraps the content of the given elements in the inner most element
        /// of the tree generated form the provided HTML code.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to iterate through.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T WrapInner<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            foreach (var element in elements)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();

                while (element.ChildNodes.Length > 0)
                {
                    var child = element.ChildNodes[0];
                    newParent.AppendChild(child);
                }

                element.AppendChild(fragment);
            }

            return elements;
        }

        /// <summary>
        /// Wraps all elements in the inner most element of the tree
        /// generated form the provided HTML code. The tree is appended before
        /// the first element of the given list.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="elements">The elements to wrap.</param>
        /// <param name="html">The HTML code that generates the tree.</param>
        /// <returns>The unchanged collection.</returns>
        public static T WrapAll<T>(this T elements, String html)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            var element = elements.FirstOrDefault();

            if (element != null)
            {
                var fragment = element.CreateFragment(html);
                var newParent = fragment.GetInnerMostElement();
                var parent = element.Parent;
                parent?.InsertBefore(fragment, element);

                foreach (var child in elements)
                {
                    newParent.AppendChild(child);
                }
            }

            return elements;
        }

        /// <summary>
        /// Creates a new HTML Collection from the provided elements.
        /// </summary>
        /// <typeparam name="TElement">The base type of the elements.</typeparam>
        /// <param name="elements">The elements to include.</param>
        /// <returns>The created collection.</returns>
        public static IHtmlCollection<TElement> ToCollection<TElement>(this IEnumerable<TElement> elements)
            where TElement : class, IElement
        {
            return new HtmlCollection<TElement>(elements);
        }

        /// <summary>
        /// Navigates to the hyper reference given by the provided element
        /// without any possibility for cancellation.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="element">The element of navigation.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> NavigateAsync<TElement>(this TElement element)
            where TElement : IUrlUtilities, IElement
        {
            return element.NavigateAsync(CancellationToken.None);
        }

        /// <summary>
        /// Navigates to the hyper reference given by the provided element.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="element">The element of navigation.</param>
        /// <param name="cancel">The token for cancellation.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> NavigateAsync<TElement>(this TElement element, CancellationToken cancel)
            where TElement : IUrlUtilities, IElement
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var address = element.Href;
            var url = Url.Create(address);
            return element.Owner.Context.OpenAsync(url, cancel);
        }

        /// <summary>
        /// Creates a task to use the processor for loading and processing the
        /// resource from the provided url.
        /// </summary>
        /// <param name="element">The element to use.</param>
        /// <param name="processor">The processor to use.</param>
        /// <param name="url">The url of the resource.</param>
        internal static void Process(this Element element, IRequestProcessor processor, Url url)
        {
            var request = element.CreateRequestFor(url);
            var task = processor?.ProcessAsync(request);

            if (task != null)
            {
                element.Owner?.DelayLoad(task);
            }
        }

        /// <summary>
        /// Gets a suitable image candidate for the provided image element.
        /// </summary>
        /// <param name="img">The element to use.</param>
        /// <returns>The possibly valid URL to the right candidate.</returns>
        internal static Url GetImageCandidate(this HtmlImageElement img)
        {
            var srcset = new SourceSet();
            var context = img.Context;
            var sources = img.GetSources();

            while (sources.Count > 0)
            {
                var source = sources.Pop();
                var type = source.Type;

                if (String.IsNullOrEmpty(type) || context.GetResourceService<IImageInfo>(type) != null)
                {
                    foreach (var candidate in srcset.GetCandidates(source.SourceSet, source.Sizes))
                    {
                        return new Url(img.BaseUrl, candidate);
                    }
                }
            }

            foreach (var candidate in srcset.GetCandidates(img.SourceSet, img.Sizes))
            {
                return new Url(img.BaseUrl, candidate);
            }

            return string.IsNullOrEmpty(img.Source) ? null : Url.Create(img.Source);
        }

        /// <summary>
        /// Plan to navigate to an action using the specified method with the given
        /// entity body of the mime type.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#plan-to-navigate
        /// </summary>
        /// <param name="element">The element to navigate from.</param>
        /// <param name="request">The request to issue.</param>
        /// <returns>A task that will eventually result in a new document.</returns>
        internal static async Task<IDocument> NavigateToAsync(this Element element, DocumentRequest request)
        {
            var download = element.Context.GetService<IDocumentLoader>()?.FetchAsync(request);

            if (download != null)
            {
                var response = await download.Task.ConfigureAwait(false);
                var cancel = CancellationToken.None;
                return await element.Owner.Context.OpenAsync(response, cancel).ConfigureAwait(false);
            }

            return null;
        }

        /// <summary>
        /// Faster way of getting the (known) attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's value, if any.</returns>
        internal static String GetOwnAttribute(this Element element, String name)
        {
            return element.Attributes.GetNamedItem(null, name)?.Value;
        }

        /// <summary>
        /// Faster way of checking for a (known) attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>True if the attribute exists, otherwise false.</returns>
        internal static Boolean HasOwnAttribute(this Element element, String name)
        {
            return element.Attributes.GetNamedItem(null, name) != null;
        }

        /// <summary>
        /// Easy way of getting the current absolute url from attributes.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's absolute url value.</returns>
        internal static String GetUrlAttribute(this Element element, String name)
        {
            var value = element.GetOwnAttribute(name);
            var url = value != null ? new Url(element.BaseUrl, value) : null;
            return url != null && !url.IsInvalid ? url.Href : String.Empty;
        }

        /// <summary>
        /// Easy way of getting the current boolean value from attributes.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's boolean value.</returns>
        internal static Boolean GetBoolAttribute(this Element element, String name)
        {
            var value = element.GetOwnAttribute(name);
            return value != null;
        }

        /// <summary>
        /// Easy way of setting the current boolean value of an attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The attribute's value.</param>
        internal static void SetBoolAttribute(this Element element, String name, Boolean value)
        {
            if (value)
            {
                element.SetOwnAttribute(name, String.Empty);
            }
            else
            {
                element.Attributes.RemoveNamedItemOrDefault(name, true);
            }
        }

        /// <summary>
        /// Faster way of setting the (known) attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The attribute's value.</param>
        /// <param name="suppressCallbacks">Flag to suppress callbacks.</param>
        internal static void SetOwnAttribute(this Element element, String name, String value, Boolean suppressCallbacks = false)
        {
            element.Attributes.SetNamedItemWithNamespaceUri(new Attr(name, value), suppressCallbacks);
        }

        private static IDocumentFragment CreateFragment(this IElement context, String html)
        {
            var contextElement = context as Element;
            var content = html ?? String.Empty;
            return new DocumentFragment(contextElement, content);
        }

        private static IElement GetInnerMostElement(this IDocumentFragment fragment)
        {
            if (fragment.ChildElementCount != 1)
                throw new InvalidOperationException("The provided HTML code did not result in any element.");

            var element = default(IElement);
            var child = fragment.FirstElementChild;

            do
            {
                element = child;
                child = element.FirstElementChild;
            }
            while (child != null);

            return element;
        }
    }
}

namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Diagnostics;

    /// <summary>
    /// Useful methods for element objects.
    /// </summary>
    [DebuggerStepThrough]
    static class ElementExtensions
    {
        /// <summary>
        /// Locates the prefix of the given namespace.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="namespaceUri">The url of the namespace.</param>
        /// <returns>The prefix or null, if the namespace could not be found.</returns>
        public static String LocatePrefix(this IElement element, String namespaceUri)
        {
            if (element == null)
                return null;

            if (element.NamespaceUri == namespaceUri && element.Prefix != null)
                return element.Prefix;

            foreach (var attr in element.Attributes)
            {
                if (attr.Prefix == Namespaces.XmlNsPrefix && attr.Value == namespaceUri)
                    return attr.LocalName;
            }

            return element.ParentElement.LocatePrefix(namespaceUri);
        }

        /// <summary>
        /// Locates the namespace of the given prefix.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        /// <returns>The url of the namespace or null, if the prefix could not be found.</returns>
        public static String LocateNamespace(this IElement element, String prefix)
        {
            if (element == null)
                return null;

            var ns = element.NamespaceUri;
            var px = element.Prefix;

            if (!String.IsNullOrEmpty(ns) && px == prefix)
                return ns;

            var predicate = prefix == null ? (Predicate<IAttr>)
                (attr => (attr.NamespaceUri == Namespaces.XmlNsUri && attr.Prefix == null && attr.LocalName == Namespaces.XmlNsPrefix)) :
                (attr => (attr.NamespaceUri == Namespaces.XmlNsUri && attr.Prefix == Namespaces.XmlNsPrefix && attr.LocalName == prefix));

            foreach (var attr in element.Attributes)
            {
                if (predicate(attr))
                {
                    var value = attr.Value;

                    if (String.IsNullOrEmpty(value))
                        value = null;

                    return value;
                }
            }

            return element.ParentElement.LocateNamespace(prefix);
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
            if (prefix == "*")
                return true;

            var nsUri = el.GetAttribute(Namespaces.XmlNsPrefix) ?? el.NamespaceUri;

            if (prefix == String.Empty)
                return nsUri == String.Empty;

            return nsUri == GetCssNamespace(el, prefix);
        }

        /// <summary>
        /// Gets the CSS namespace that is defined via the provided prefix.
        /// </summary>
        /// <param name="el">The element that is connected to a doc.</param>
        /// <param name="prefix">The prefix to lookup.</param>
        /// <returns>The namespace url for the prefix.</returns>
        public static String GetCssNamespace(this IElement el, String prefix)
        {
            return el.Owner.StyleSheets.LocateNamespace(prefix) ?? el.LocateNamespace(prefix);
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

            if (parent == null)
                return false;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i].NodeName == element.NodeName && parent.ChildNodes[i] != element)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if the element is the first of its type among the parent's children.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is the first of its type among its siblings.</returns>
        public static Boolean IsFirstOfType(this IElement element)
        {
            var parent = element.ParentElement;

            if (parent == null)
                return false;

            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i].NodeName == element.NodeName)
                    return parent.ChildNodes[i] == element;
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

            if (parent == null)
                return false;

            for (int i = parent.ChildNodes.Length - 1; i >= 0; i--)
            {
                if (parent.ChildNodes[i].NodeName == element.NodeName)
                    return parent.ChildNodes[i] == element;
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
            var owner = element.Owner;
            var id = element.Id;

            if (owner == null || id == null)
                return false;

            var hash = owner.Location.Hash;
            return String.Compare(id, 0, hash, hash.Length > 0 ? 1 : 0, Int32.MaxValue) == 0;
        }

        /// <summary>
        /// Checks if the element is currently enabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is currently enabled, otherwise false.</returns>
        public static Boolean IsEnabled(this IElement element)
        {
            if (element is HtmlAnchorElement || element is HtmlAreaElement || element is HtmlLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href));
            else if (element is HtmlButtonElement)
                return !((HtmlButtonElement)element).IsDisabled;
            else if (element is HtmlInputElement)
                return !((HtmlInputElement)element).IsDisabled;
            else if (element is HtmlSelectElement)
                return !((HtmlSelectElement)element).IsDisabled;
            else if (element is HtmlTextAreaElement)
                return !((HtmlTextAreaElement)element).IsDisabled;
            else if (element is HtmlOptionElement)
                return !((HtmlOptionElement)element).IsDisabled;
            else if (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement)
                return String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Disabled));

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
                return ((HtmlButtonElement)element).IsDisabled;
            else if (element is HtmlInputElement)
                return ((HtmlInputElement)element).IsDisabled;
            else if (element is HtmlSelectElement)
                return ((HtmlSelectElement)element).IsDisabled;
            else if (element is HtmlTextAreaElement)
                return ((HtmlTextAreaElement)element).IsDisabled;
            else if (element is HtmlOptionElement)
                return ((HtmlOptionElement)element).IsDisabled;
            else if (element is HtmlOptionsGroupElement || element is HtmlMenuItemElement || element is HtmlFieldSetElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Disabled));

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

                if (form != null)//TODO Check if button is form def. button
                    return true;
            }
            else if (element is HtmlInputElement)
            {
                var input = (HtmlInputElement)element;
                var type = input.Type;

                if (type == InputTypeNames.Submit || type == InputTypeNames.Image)
                {
                    var form = input.Form;

                    if (form != null)//TODO Check if input is form def. button
                        return true;
                }
                else
                {
                    //TODO input that are checked and can be checked ...
                }
            }
            else if (element is HtmlOptionElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Selected));

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
            var pseudoElement = element as PseudoElement;
            return pseudoElement != null && pseudoElement.PseudoName == name;
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
                var inp = (HtmlInputElement)element;
                var type = inp.Type;

                return (type == InputTypeNames.Checkbox || type == InputTypeNames.Radio) && inp.IsChecked;
            }
            else if (element is HtmlMenuItemElement)
            {
                var mi = (HtmlMenuItemElement)element;
                var type = mi.Type;

                return (type == InputTypeNames.Checkbox || type == InputTypeNames.Radio) && mi.IsChecked;
            }
            else if (element is HtmlOptionElement)
                return ((HtmlOptionElement)element).IsSelected;

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
                var inp = (HtmlInputElement)element;
                return inp.Type == InputTypeNames.Checkbox && inp.IsIndeterminate;
            }
            else if (element is HtmlProgressElement)
                return String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Value));

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
                return !String.IsNullOrEmpty(input.Placeholder) && String.IsNullOrEmpty(input.Value);

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
                var inp = (HtmlInputElement)element;
                var type = inp.Type;
                return (type == InputTypeNames.Checkbox || type == InputTypeNames.Radio) && !inp.IsChecked;
            }
            else if (element is HtmlMenuItemElement)
            {
                var mi = (HtmlMenuItemElement)element;
                var type = mi.Type;
                return (type == InputTypeNames.Checkbox || type == InputTypeNames.Radio) && !mi.IsChecked;
            }
            else if (element is HtmlOptionElement)
                return !((HtmlOptionElement)element).IsSelected;

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
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && ((HtmlAnchorElement)element).IsActive;
            else if (element is HtmlAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && ((HtmlAreaElement)element).IsActive;
            else if (element is HtmlLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && ((HtmlLinkElement)element).IsActive;
            else if (element is HtmlButtonElement)
                return !((HtmlButtonElement)element).IsDisabled && ((HtmlButtonElement)element).IsActive;
            else if (element is HtmlInputElement)
            {
                var inp = (HtmlInputElement)element;
                var type = inp.Type;
                return (type == InputTypeNames.Submit || type == InputTypeNames.Image || type == InputTypeNames.Reset || type == InputTypeNames.Button) && inp.IsActive;
            }
            else if (element is HtmlMenuItemElement)
                return string.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Disabled)) && ((HtmlMenuItemElement)element).IsActive;

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
                return !String.IsNullOrEmpty(element.GetAttribute( null, AttributeNames.Href)) && ((HtmlAnchorElement)element).IsVisited;
            else if (element is HtmlAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && ((HtmlAreaElement)element).IsVisited;
            else if (element is HtmlLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && ((HtmlLinkElement)element).IsVisited;

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
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && !((HtmlAnchorElement)element).IsVisited;
            else if (element is HtmlAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && !((HtmlAreaElement)element).IsVisited;
            else if (element is HtmlLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(null, AttributeNames.Href)) && !((HtmlLinkElement)element).IsVisited;

            return false;
        }

        /// <summary>
        /// Checks if the element is only optional and not required.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element is optional, otherwise false.</returns>
        public static Boolean IsOptional(this IElement element)
        {
            if (element is HtmlInputElement)
                return !((HtmlInputElement)element).IsRequired;
            else if (element is HtmlSelectElement)
                return !((HtmlSelectElement)element).IsRequired;
            else if (element is HtmlTextAreaElement)
                return !((HtmlTextAreaElement)element).IsRequired;

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
                return ((HtmlInputElement)element).IsRequired;
            else if (element is HtmlSelectElement)
                return ((HtmlSelectElement)element).IsRequired;
            else if (element is HtmlTextAreaElement)
                return ((HtmlTextAreaElement)element).IsRequired;

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
                return !((IValidation)element).CheckValidity();
            else if (element is HtmlFormElement)
                return !((HtmlFormElement)element).CheckValidity();

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
                return ((IValidation)element).CheckValidity();
            else if (element is HtmlFormElement)
                return ((HtmlFormElement)element).CheckValidity();

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
                return !((HtmlInputElement)element).IsMutable;
            else if (element is HtmlTextAreaElement)
                return !((HtmlTextAreaElement)element).IsMutable;
            else if (element is IHtmlElement)
                return !((IHtmlElement)element).IsContentEditable;

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
                return ((HtmlInputElement)element).IsMutable;
            else if (element is HtmlTextAreaElement)
                return ((HtmlTextAreaElement)element).IsMutable;
            else if (element is IHtmlElement)
                return ((IHtmlElement)element).IsContentEditable;

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
            return parent != null && parent.FirstElementChild == element;
        }

        /// <summary>
        /// Checks if the element is its parent's last child.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the element's last child is the given one, otherwise false.</returns>
        public static Boolean IsLastChild(this IElement element)
        {
            var parent = element.ParentElement;
            return parent != null && parent.LastElementChild == element;
        }
    }
}

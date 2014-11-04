namespace AngleSharp.Extensions
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
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
            if (element.NamespaceUri == namespaceUri && element.Prefix != null)
                return element.Prefix;

            foreach (var attr in element.Attributes)
            {
                if (attr.Prefix == Namespaces.XmlNsPrefix && attr.Value == namespaceUri)
                    return attr.LocalName;
            }

            var parent = element.ParentElement;

            if (parent != null)
                return parent.LocatePrefix(namespaceUri);

            return null;
        }

        /// <summary>
        /// Locates the namespace of the given prefix.
        /// </summary>
        /// <param name="element">The element that might contain the namespace information.</param>
        /// <param name="prefix">The prefix of the namespace to find.</param>
        /// <returns>The url of the namespace or null, if the prefix could not be found.</returns>
        public static String LocateNamespace(this IElement element, String prefix)
        {
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

            var parent = element.ParentElement;

            if (parent != null)
                return parent.LocateNamespace(prefix);

            return null;
        }

        /// <summary>
        /// Checks if the element is currently hovered.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently hovered, otherwise false.</returns>
        public static Boolean IsHovered(this IElement element)
        {
            //TODO Connection to Visual Tree ?
            return false;
        }

        /// <summary>
        /// Checks if the element is currently focused.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently focused, otherwise false.</returns>
        public static Boolean IsFocused(this IElement element)
        {
            //TODO Connection to Visual Tree ?
            return false;
        }

        /// <summary>
        /// Checks if the element is currently enabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently enabled, otherwise false.</returns>
        public static Boolean IsEnabled(this IElement el)
        {
            if (el is HTMLAnchorElement || el is HTMLAreaElement || el is HTMLLinkElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href));
            else if (el is HTMLButtonElement)
                return !((HTMLButtonElement)el).IsDisabled;
            else if (el is HTMLInputElement)
                return !((HTMLInputElement)el).IsDisabled;
            else if (el is HTMLSelectElement)
                return !((HTMLSelectElement)el).IsDisabled;
            else if (el is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)el).IsDisabled;
            else if (el is HTMLOptionElement)
                return !((HTMLOptionElement)el).IsDisabled;
            else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
                return String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently disabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently disabled, otherwise false.</returns>
        public static Boolean IsDisabled(this IElement el)
        {
            if (el is HTMLButtonElement)
                return ((HTMLButtonElement)el).IsDisabled;
            else if (el is HTMLInputElement)
                return ((HTMLInputElement)el).IsDisabled;
            else if (el is HTMLSelectElement)
                return ((HTMLSelectElement)el).IsDisabled;
            else if (el is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)el).IsDisabled;
            else if (el is HTMLOptionElement)
                return ((HTMLOptionElement)el).IsDisabled;
            else if (el is HTMLOptGroupElement || el is HTMLMenuItemElement || el is HTMLFieldSetElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled));

            return false;
        }

        /// <summary>
        /// Checks if the element is an input element that is currently in its default state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently in its default state, otherwise false.</returns>
        public static Boolean IsDefault(this IElement el)
        {
            if (el is HTMLButtonElement)
            {
                var bt = (HTMLButtonElement)el;
                var form = bt.Form;

                if (form != null)//TODO Check if button is form def. button
                    return true;
            }
            else if (el is HTMLInputElement)
            {
                var input = (HTMLInputElement)el;
                var type = input.Type.ToEnum(HTMLInputElement.InputType.Text);

                if (type == HTMLInputElement.InputType.Submit || type == HTMLInputElement.InputType.Image)
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
            else if (el is HTMLOptionElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Selected));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently checked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently checked, otherwise false.</returns>
        public static Boolean IsChecked(this IElement el)
        {
            if (el is HTMLInputElement)
            {
                var inp = (HTMLInputElement)el;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);

                return (type == HTMLInputElement.InputType.Checkbox || type == HTMLInputElement.InputType.Radio) && inp.IsChecked;
            }
            else if (el is HTMLMenuItemElement)
            {
                var mi = (HTMLMenuItemElement)el;
                var type = mi.Type.ToEnum(HTMLMenuItemElement.ItemType.Command);

                return (type == HTMLMenuItemElement.ItemType.Checkbox || type == HTMLMenuItemElement.ItemType.Radio) && mi.IsChecked;
            }
            else if (el is HTMLOptionElement)
                return ((HTMLOptionElement)el).IsSelected;

            return false;
        }

        /// <summary>
        /// Checks if the element is currently in its indeterminate state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently indeterminate, otherwise false.</returns>
        public static Boolean IsIndeterminate(this IElement el)
        {
            if (el is HTMLInputElement)
            {
                var inp = (HTMLInputElement)el;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return type == HTMLInputElement.InputType.Checkbox && inp.IsIndeterminate;
            }
            else if (el is HTMLProgressElement)
                return String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Value));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently unchecked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently unchecked, otherwise false.</returns>
        public static Boolean IsUnchecked(this IElement el)
        {
            if (el is HTMLInputElement)
            {
                var inp = (HTMLInputElement)el;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return (type == HTMLInputElement.InputType.Checkbox || type == HTMLInputElement.InputType.Radio) && !inp.IsChecked;
            }
            else if (el is HTMLMenuItemElement)
            {
                var mi = (HTMLMenuItemElement)el;
                var type = mi.Type.ToEnum(HTMLMenuItemElement.ItemType.Command);

                return (type == HTMLMenuItemElement.ItemType.Checkbox || type == HTMLMenuItemElement.ItemType.Radio)
                    && !mi.IsChecked;
            }
            else if (el is HTMLOptionElement)
                return !((HTMLOptionElement)el).IsSelected;

            return false;
        }

        /// <summary>
        /// Checks if the element is currently active.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently active, otherwise false.</returns>
        public static Boolean IsActive(this IElement el)
        {
            if (el is HTMLAnchorElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsActive;
            else if (el is HTMLAreaElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsActive;
            else if (el is HTMLLinkElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsActive;
            else if (el is HTMLButtonElement)
                return !((HTMLButtonElement)el).IsDisabled && ((HTMLButtonElement)el).IsActive;
            else if (el is HTMLInputElement)
            {
                var inp = (HTMLInputElement)el;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return (type == HTMLInputElement.InputType.Submit || type == HTMLInputElement.InputType.Image ||
                    type == HTMLInputElement.InputType.Reset || type == HTMLInputElement.InputType.Button) && inp.IsActive;
            }
            else if (el is HTMLMenuItemElement)
                return string.IsNullOrEmpty(el.GetAttribute(AttributeNames.Disabled)) && ((HTMLMenuItemElement)el).IsActive;

            return false;
        }

        /// <summary>
        /// Checks if the element has already been visited.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent has been visited, otherwise false.</returns>
        public static Boolean IsVisited(this IElement el)
        {
            if (el is HTMLAnchorElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)el).IsVisited;
            else if (el is HTMLAreaElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)el).IsVisited;
            else if (el is HTMLLinkElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)el).IsVisited;

            return false;
        }

        /// <summary>
        /// Checks if the element is a link.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is a link, otherwise false.</returns>
        public static Boolean IsLink(this IElement el)
        {
            if (el is HTMLAnchorElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAnchorElement)el).IsVisited;
            else if (el is HTMLAreaElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLAreaElement)el).IsVisited;
            else if (el is HTMLLinkElement)
                return !String.IsNullOrEmpty(el.GetAttribute(AttributeNames.Href)) && !((HTMLLinkElement)el).IsVisited;

            return false;
        }

        /// <summary>
        /// Checks if the element is only optional and not required.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is optional, otherwise false.</returns>
        public static Boolean IsOptional(this IElement el)
        {
            if (el is HTMLInputElement)
                return !((HTMLInputElement)el).IsRequired;
            else if (el is HTMLSelectElement)
                return !((HTMLSelectElement)el).IsRequired;
            else if (el is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)el).IsRequired;

            return false;
        }

        /// <summary>
        /// Checks if the element is required and must be filled out.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is required, otherwise false.</returns>
        public static Boolean IsRequired(this IElement el)
        {
            if (el is HTMLInputElement)
                return ((HTMLInputElement)el).IsRequired;
            else if (el is HTMLSelectElement)
                return ((HTMLSelectElement)el).IsRequired;
            else if (el is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)el).IsRequired;

            return false;
        }

        /// <summary>
        /// Checks if the element does not validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is invalid, otherwise false.</returns>
        public static Boolean IsInvalid(this IElement el)
        {
            if (el is IValidation)
                return !((IValidation)el).CheckValidity();
            else if (el is HTMLFormElement)
                return !((HTMLFormElement)el).CheckValidity();

            return false;
        }

        /// <summary>
        /// Checks if the element does validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is valid, otherwise false.</returns>
        public static Boolean IsValid(this IElement el)
        {
            if (el is IValidation)
                return ((IValidation)el).CheckValidity();
            else if (el is HTMLFormElement)
                return ((HTMLFormElement)el).CheckValidity();

            return false;
        }

        /// <summary>
        /// Checks if the element is readonly.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is readonly, otherwise false.</returns>
        public static Boolean IsReadOnly(this IElement el)
        {
            if (el is HTMLInputElement)
                return !((HTMLInputElement)el).IsMutable;
            else if (el is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)el).IsMutable;
            else if (el is IHtmlElement)
                return !((IHtmlElement)el).IsContentEditable;

            return true;
        }

        /// <summary>
        /// Checks if the element is editable.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent can be edited, otherwise false.</returns>
        public static Boolean IsEditable(this IElement el)
        {
            if (el is HTMLInputElement)
                return ((HTMLInputElement)el).IsMutable;
            else if (el is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)el).IsMutable;
            else if (el is IHtmlElement)
                return ((IHtmlElement)el).IsContentEditable;

            return false;
        }

        /// <summary>
        /// Checks if the element's value is out-of-range.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is invalid, otherwise false.</returns>
        public static Boolean IsOutOfRange(this IElement el)
        {
            var validation = el as IValidation;

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
        /// <returns>True if the elemnent is valid, otherwise false.</returns>
        public static Boolean IsInRange(this IElement el)
        {
            var validation = el as IValidation;

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
        /// <returns>True if the elemnent has no siblings, otherwise false.</returns>
        public static Boolean IsOnlyChild(this IElement el)
        {
            var parent = el.ParentElement;

            if (parent == null)
                return false;

            return parent.ChildElementCount == 1;
        }
    }
}

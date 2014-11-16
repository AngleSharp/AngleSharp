namespace AngleSharp.Extensions
{
    using AngleSharp.DOM;
    using AngleSharp.DOM.Html;
    using AngleSharp.Html;
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
        /// Adds a new attribute if the attribute is not yet created.
        /// Does not fire the changed event.
        /// </summary>
        /// <param name="element">The element that gets a new attribute.</param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        public static void AddAttribute(this Element element, String name, String value)
        {
            var attributes = element.Attributes;

            if (!attributes.Has(name))
                attributes.Add(new Attr(element, name, value));
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
        public static Boolean IsEnabled(this IElement element)
        {
            if (element is HTMLAnchorElement || element is HTMLAreaElement || element is HTMLLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href));
            else if (element is HTMLButtonElement)
                return !((HTMLButtonElement)element).IsDisabled;
            else if (element is HTMLInputElement)
                return !((HTMLInputElement)element).IsDisabled;
            else if (element is HTMLSelectElement)
                return !((HTMLSelectElement)element).IsDisabled;
            else if (element is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)element).IsDisabled;
            else if (element is HTMLOptionElement)
                return !((HTMLOptionElement)element).IsDisabled;
            else if (element is HTMLOptGroupElement || element is HTMLMenuItemElement || element is HTMLFieldSetElement)
                return String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Disabled));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently disabled.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently disabled, otherwise false.</returns>
        public static Boolean IsDisabled(this IElement element)
        {
            if (element is HTMLButtonElement)
                return ((HTMLButtonElement)element).IsDisabled;
            else if (element is HTMLInputElement)
                return ((HTMLInputElement)element).IsDisabled;
            else if (element is HTMLSelectElement)
                return ((HTMLSelectElement)element).IsDisabled;
            else if (element is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)element).IsDisabled;
            else if (element is HTMLOptionElement)
                return ((HTMLOptionElement)element).IsDisabled;
            else if (element is HTMLOptGroupElement || element is HTMLMenuItemElement || element is HTMLFieldSetElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Disabled));

            return false;
        }

        /// <summary>
        /// Checks if the element is an input element that is currently in its default state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently in its default state, otherwise false.</returns>
        public static Boolean IsDefault(this IElement element)
        {
            if (element is HTMLButtonElement)
            {
                var bt = (HTMLButtonElement)element;
                var form = bt.Form;

                if (form != null)//TODO Check if button is form def. button
                    return true;
            }
            else if (element is HTMLInputElement)
            {
                var input = (HTMLInputElement)element;
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
            else if (element is HTMLOptionElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Selected));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently checked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently checked, otherwise false.</returns>
        public static Boolean IsChecked(this IElement element)
        {
            if (element is HTMLInputElement)
            {
                var inp = (HTMLInputElement)element;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);

                return (type == HTMLInputElement.InputType.Checkbox || type == HTMLInputElement.InputType.Radio) && inp.IsChecked;
            }
            else if (element is HTMLMenuItemElement)
            {
                var mi = (HTMLMenuItemElement)element;
                var type = mi.Type.ToEnum(HTMLMenuItemElement.ItemType.Command);

                return (type == HTMLMenuItemElement.ItemType.Checkbox || type == HTMLMenuItemElement.ItemType.Radio) && mi.IsChecked;
            }
            else if (element is HTMLOptionElement)
                return ((HTMLOptionElement)element).IsSelected;

            return false;
        }

        /// <summary>
        /// Checks if the element is currently in its indeterminate state.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently indeterminate, otherwise false.</returns>
        public static Boolean IsIndeterminate(this IElement element)
        {
            if (element is HTMLInputElement)
            {
                var inp = (HTMLInputElement)element;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return type == HTMLInputElement.InputType.Checkbox && inp.IsIndeterminate;
            }
            else if (element is HTMLProgressElement)
                return String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Value));

            return false;
        }

        /// <summary>
        /// Checks if the element is currently unchecked.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently unchecked, otherwise false.</returns>
        public static Boolean IsUnchecked(this IElement element)
        {
            if (element is HTMLInputElement)
            {
                var inp = (HTMLInputElement)element;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return (type == HTMLInputElement.InputType.Checkbox || type == HTMLInputElement.InputType.Radio) && !inp.IsChecked;
            }
            else if (element is HTMLMenuItemElement)
            {
                var mi = (HTMLMenuItemElement)element;
                var type = mi.Type.ToEnum(HTMLMenuItemElement.ItemType.Command);

                return (type == HTMLMenuItemElement.ItemType.Checkbox || type == HTMLMenuItemElement.ItemType.Radio)
                    && !mi.IsChecked;
            }
            else if (element is HTMLOptionElement)
                return !((HTMLOptionElement)element).IsSelected;

            return false;
        }

        /// <summary>
        /// Checks if the element is currently active.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is currently active, otherwise false.</returns>
        public static Boolean IsActive(this IElement element)
        {
            if (element is HTMLAnchorElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)element).IsActive;
            else if (element is HTMLAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)element).IsActive;
            else if (element is HTMLLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)element).IsActive;
            else if (element is HTMLButtonElement)
                return !((HTMLButtonElement)element).IsDisabled && ((HTMLButtonElement)element).IsActive;
            else if (element is HTMLInputElement)
            {
                var inp = (HTMLInputElement)element;
                var type = inp.Type.ToEnum(HTMLInputElement.InputType.Text);
                return (type == HTMLInputElement.InputType.Submit || type == HTMLInputElement.InputType.Image ||
                    type == HTMLInputElement.InputType.Reset || type == HTMLInputElement.InputType.Button) && inp.IsActive;
            }
            else if (element is HTMLMenuItemElement)
                return string.IsNullOrEmpty(element.GetAttribute(AttributeNames.Disabled)) && ((HTMLMenuItemElement)element).IsActive;

            return false;
        }

        /// <summary>
        /// Checks if the element has already been visited.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent has been visited, otherwise false.</returns>
        public static Boolean IsVisited(this IElement element)
        {
            if (element is HTMLAnchorElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLAnchorElement)element).IsVisited;
            else if (element is HTMLAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLAreaElement)element).IsVisited;
            else if (element is HTMLLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && ((HTMLLinkElement)element).IsVisited;

            return false;
        }

        /// <summary>
        /// Checks if the element is a link.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is a link, otherwise false.</returns>
        public static Boolean IsLink(this IElement element)
        {
            if (element is HTMLAnchorElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && !((HTMLAnchorElement)element).IsVisited;
            else if (element is HTMLAreaElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && !((HTMLAreaElement)element).IsVisited;
            else if (element is HTMLLinkElement)
                return !String.IsNullOrEmpty(element.GetAttribute(AttributeNames.Href)) && !((HTMLLinkElement)element).IsVisited;

            return false;
        }

        /// <summary>
        /// Checks if the element is only optional and not required.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is optional, otherwise false.</returns>
        public static Boolean IsOptional(this IElement element)
        {
            if (element is HTMLInputElement)
                return !((HTMLInputElement)element).IsRequired;
            else if (element is HTMLSelectElement)
                return !((HTMLSelectElement)element).IsRequired;
            else if (element is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)element).IsRequired;

            return false;
        }

        /// <summary>
        /// Checks if the element is required and must be filled out.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is required, otherwise false.</returns>
        public static Boolean IsRequired(this IElement element)
        {
            if (element is HTMLInputElement)
                return ((HTMLInputElement)element).IsRequired;
            else if (element is HTMLSelectElement)
                return ((HTMLSelectElement)element).IsRequired;
            else if (element is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)element).IsRequired;

            return false;
        }

        /// <summary>
        /// Checks if the element does not validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is invalid, otherwise false.</returns>
        public static Boolean IsInvalid(this IElement element)
        {
            if (element is IValidation)
                return !((IValidation)element).CheckValidity();
            else if (element is HTMLFormElement)
                return !((HTMLFormElement)element).CheckValidity();

            return false;
        }

        /// <summary>
        /// Checks if the element does validate.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is valid, otherwise false.</returns>
        public static Boolean IsValid(this IElement element)
        {
            if (element is IValidation)
                return ((IValidation)element).CheckValidity();
            else if (element is HTMLFormElement)
                return ((HTMLFormElement)element).CheckValidity();

            return false;
        }

        /// <summary>
        /// Checks if the element is readonly.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is readonly, otherwise false.</returns>
        public static Boolean IsReadOnly(this IElement element)
        {
            if (element is HTMLInputElement)
                return !((HTMLInputElement)element).IsMutable;
            else if (element is HTMLTextAreaElement)
                return !((HTMLTextAreaElement)element).IsMutable;
            else if (element is IHtmlElement)
                return !((IHtmlElement)element).IsContentEditable;

            return true;
        }

        /// <summary>
        /// Checks if the element is editable.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent can be edited, otherwise false.</returns>
        public static Boolean IsEditable(this IElement element)
        {
            if (element is HTMLInputElement)
                return ((HTMLInputElement)element).IsMutable;
            else if (element is HTMLTextAreaElement)
                return ((HTMLTextAreaElement)element).IsMutable;
            else if (element is IHtmlElement)
                return ((IHtmlElement)element).IsContentEditable;

            return false;
        }

        /// <summary>
        /// Checks if the element's value is out-of-range.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the elemnent is invalid, otherwise false.</returns>
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
        /// <returns>True if the elemnent is valid, otherwise false.</returns>
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
        /// <returns>True if the elemnent has no siblings, otherwise false.</returns>
        public static Boolean IsOnlyChild(this IElement element)
        {
            var parent = element.ParentElement;

            if (parent == null)
                return false;

            return parent.ChildElementCount == 1;
        }
    }
}

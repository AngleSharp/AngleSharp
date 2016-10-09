namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using AngleSharp.Network.RequestProcessors;
    using AngleSharp.Services.Media;
    using System;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Useful methods for element objects.
    /// </summary>
    static class ElementExtensions
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

            if (!String.IsNullOrEmpty(ns) && px.Is(prefix))
            {
                return ns;
            }

            var predicate = prefix == null ? (Predicate<IAttr>)
                (attr => (attr.NamespaceUri.Is(NamespaceNames.XmlNsUri) && attr.Prefix == null && attr.LocalName.Is(NamespaceNames.XmlNsPrefix))) :
                (attr => (attr.NamespaceUri.Is(NamespaceNames.XmlNsUri) && attr.Prefix.Is(NamespaceNames.XmlNsPrefix) && attr.LocalName.Is(prefix)));

            foreach (var attr in element.Attributes)
            {
                if (predicate(attr))
                {
                    var value = attr.Value;

                    if (String.IsNullOrEmpty(value))
                    {
                        value = null;
                    }

                    return value;
                }
            }

            return element.ParentElement?.LocateNamespaceFor(prefix);
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
            if (prefix.Is(Keywords.Asterisk))
            {
                return true;
            }

            var nsUri = el.GetAttribute(NamespaceNames.XmlNsPrefix) ?? el.NamespaceUri;

            if (prefix.Is(String.Empty))
            {
                return nsUri.Is(String.Empty);
            }

            return nsUri.Is(GetCssNamespace(el, prefix));
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
            var pseudoElement = element as PseudoElement;
            return pseudoElement != null && pseudoElement.PseudoName.Is(name);
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
        /// Creates a task to use the processor for loading and processing the
        /// resource from the provided url.
        /// </summary>
        /// <param name="element">The element to use.</param>
        /// <param name="processor">The processor to use.</param>
        /// <param name="url">The url of the resource.</param>
        public static void Process(this Element element, IRequestProcessor processor, Url url)
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
        public static Url GetImageCandidate(this HtmlImageElement img)
        {
            var owner = img.Owner;
            var srcset = new SourceSet(owner);
            var options = owner.Options;
            var sources = img.GetSources();

            while (sources.Count > 0)
            {
                var source = sources.Pop();
                var type = source.Type;

                if (String.IsNullOrEmpty(type) || options.GetResourceService<IImageInfo>(type) != null)
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

            return Url.Create(img.Source);
        }

        /// <summary>
        /// Plan to navigate to an action using the specified method with the given
        /// entity body of the mime type.
        /// http://www.w3.org/html/wg/drafts/html/master/forms.html#plan-to-navigate
        /// </summary>
        /// <param name="element">The element to navigate from.</param>
        /// <param name="request">The request to issue.</param>
        /// <returns>A task that will eventually result in a new document.</returns>
        public static async Task<IDocument> NavigateToAsync(this Element element, DocumentRequest request)
        {
            var download = element.Owner.Context.Loader.DownloadAsync(request);
            var response = await download.Task.ConfigureAwait(false);
            var cancel = CancellationToken.None;
            return await element.Owner.Context.OpenAsync(response, cancel).ConfigureAwait(false);
        }

        /// <summary>
        /// Faster way of getting the (known) attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's value, if any.</returns>
        public static String GetOwnAttribute(this Element element, String name)
        {
            return element.Attributes.GetNamedItem(null, name)?.Value;
        }

        /// <summary>
        /// Faster way of checking for a (known) attribute.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>True if the attribute exists, otherwise false.</returns>
        public static Boolean HasOwnAttribute(this Element element, String name)
        {
            return element.Attributes.GetNamedItem(null, name) != null;
        }

        /// <summary>
        /// Easy way of getting the current absolute url from attributes.
        /// </summary>
        /// <param name="element">The element to host the attribute.</param>
        /// <param name="name">The name of the attribute.</param>
        /// <returns>The attribute's absolute url value.</returns>
        public static String GetUrlAttribute(this Element element, String name)
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
        public static Boolean GetBoolAttribute(this Element element, String name)
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
        public static void SetBoolAttribute(this Element element, String name, Boolean value)
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
        public static void SetOwnAttribute(this Element element, String name, String value, Boolean suppressCallbacks = false)
        {
            element.Attributes.SetNamedItemWithNamespaceUri(new Attr(name, value), suppressCallbacks);
        }

        /// <summary>
        /// Gathers the source elements for the provided image element.
        /// </summary>
        /// <param name="img">The image to extend.</param>
        /// <returns>The stack of source elements.</returns>
        private static Stack<IHtmlSourceElement> GetSources(this IHtmlImageElement img)
        {
            var parent = img.ParentElement;
            var sources = new Stack<IHtmlSourceElement>();

            if (parent != null && parent.LocalName.Is(TagNames.Picture))
            {
                var element = img.PreviousElementSibling as IHtmlSourceElement;

                while (element != null)
                {
                    sources.Push(element);
                    element = element.PreviousElementSibling as IHtmlSourceElement;
                }
            }

            return sources;
        }
    }
}

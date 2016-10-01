namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Dom.Collections;
    using AngleSharp.Dom.Css;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// A set of useful extension methods when dealing with the DOM.
    /// </summary>
    public static class ApiExtensions
    {
        #region Generic extensions

        /// <summary>
        /// Creates an element of the given type or throws an exception, if
        /// there is no such type.
        /// </summary>
        /// <typeparam name="TElement">The type of the element.</typeparam>
        /// <param name="document">The responsible document.</param>
        /// <returns>The created element.</returns>
        public static TElement CreateElement<TElement>(this IDocument document)
            where TElement : IElement
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var type = typeof(ApiExtensions).GetAssembly().GetTypes()
                .Where(m => m.Implements<TElement>())
                .FirstOrDefault(m => !m.IsAbstractClass());

            if (type != null)
            {
                var ctors = type.GetConstructors()
                    .OrderBy(m => m.GetParameters().Length);

                foreach (var ctor in ctors)
                {
                    var parameters = ctor.GetParameters();
                    var arguments = new Object[parameters.Length];

                    for (var i = 0; i < parameters.Length; i++)
                    {
                        var isDocument = parameters[i].ParameterType == typeof(Document);
                        arguments[i] = isDocument ? document : parameters[i].DefaultValue;
                    }

                    var obj = ctor.Invoke(arguments);

                    if (obj != null)
                    {
                        var element = (TElement)obj;
                        var baseElement = element as Element;
                        baseElement?.SetupElement();
                        document.Adopt(element);
                        return element;
                    }
                }
            }

            throw new ArgumentException("No element could be created for the provided interface.");
        }

        /// <summary>
        /// Returns a task that is completed once the event is fired.
        /// </summary>
        /// <typeparam name="TEventTarget">The event target type.</typeparam>
        /// <param name="node">The node that fires the event.</param>
        /// <param name="eventName">The name of the event to be awaited.</param>
        /// <returns>The awaitable task returning the event arguments.</returns>
        public static async Task<Event> AwaitEventAsync<TEventTarget>(this TEventTarget node, String eventName)
            where TEventTarget : IEventTarget
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (eventName == null)
                throw new ArgumentNullException(nameof(eventName));

            var completion = new TaskCompletionSource<Event>();
            DomEventHandler handler = (s, ev) => completion.TrySetResult(ev);
            node.AddEventListener(eventName, handler);

            try
            {
                return await completion.Task.ConfigureAwait(false);
            }
            finally
            {
                node.RemoveEventListener(eventName, handler);
            }
        }

        /// <summary>
        /// Inserts a node as the last child node of this element.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="element">The element to be appended.</param>
        /// <returns>The appended element.</returns>
        public static TElement AppendElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.AppendChild(element) as TElement;
        }

        /// <summary>
        /// Inserts the newElement immediately before the referenceElement.
        /// </summary>
        /// <typeparam name="TElement">The type of element to add.</typeparam>
        /// <param name="parent">The parent of the node to add.</param>
        /// <param name="newElement">The node to be inserted.</param>
        /// <param name="referenceElement">
        /// The existing child element that will succeed the new element.
        /// </param>
        /// <returns>The inserted element.</returns>
        public static TElement InsertElement<TElement>(this INode parent, TElement newElement, INode referenceElement)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.InsertBefore(newElement, referenceElement) as TElement;
        }

        /// <summary>
        /// Removes a child node from the current element, which must be a
        /// child of the current node.
        /// </summary>
        /// <typeparam name="TElement">The type of element.</typeparam>
        /// <param name="parent">The parent of the node to remove.</param>
        /// <param name="element">The element to be removed.</param>
        /// <returns>The removed element.</returns>
        public static TElement RemoveElement<TElement>(this INode parent, TElement element)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.RemoveChild(element) as TElement;
        }

        /// <summary>
        /// Returns the first element matching the selectors with the provided
        /// type, or null.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>The element, if there is any.</returns>
        public static TElement QuerySelector<TElement>(this IParentNode parent, String selectors)
            where TElement : class, IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (selectors == null)
                throw new ArgumentNullException(nameof(selectors));

            return parent.QuerySelector(selectors) as TElement;
        }

        /// <summary>
        /// Returns a list of elements matching the selectors with the
        /// provided type.
        /// </summary>
        /// <typeparam name="TElement">The type to look for.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <param name="selectors">The group of selectors to use.</param>
        /// <returns>An enumeration with the elements.</returns>
        public static IEnumerable<TElement> QuerySelectorAll<TElement>(this IParentNode parent, String selectors)
            where TElement : IElement
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (selectors == null)
                throw new ArgumentNullException(nameof(selectors));

            return parent.QuerySelectorAll(selectors).OfType<TElement>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<TNode> Descendents<TNode>(this INode parent)
        {
            return parent.Descendents().OfType<TNode>();
        }

        /// <summary>
        /// Gets the descendent nodes of the given parent.
        /// </summary>
        /// <param name="parent">The parent of the nodes to gather.</param>
        /// <returns>The descendent nodes.</returns>
        public static IEnumerable<INode> Descendents(this INode parent)
        {
            if (parent == null)
                throw new ArgumentNullException(nameof(parent));

            return parent.GetDescendants();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <typeparam name="TNode">The type of nodes to obtain.</typeparam>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<TNode> Ancestors<TNode>(this INode child)
        {
            return child.Ancestors().OfType<TNode>();
        }

        /// <summary>
        /// Gets the ancestor nodes of the given child.
        /// </summary>
        /// <param name="child">The child of the nodes to gather.</param>
        /// <returns>The ancestor nodes.</returns>
        public static IEnumerable<INode> Ancestors(this INode child)
        {
            if (child == null)
                throw new ArgumentNullException(nameof(child));

            return child.GetAncestors();
        }

        #endregion

        #region Set Form Values extensions

        /// <summary>
        /// Set the field values of given form by using the dictionary which
        /// contains name value pairs of input fields.
        /// </summary>
        /// <param name="form">The form to set.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The given form for chaining.</returns>
        public static IHtmlFormElement SetValues(this IHtmlFormElement form, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            if (form == null)
                throw new ArgumentNullException(nameof(form));

            if (fields == null)
                throw new ArgumentNullException(nameof(fields));

            var inputs = form.Elements.OfType<HtmlFormControlElement>();

            foreach (var field in fields)
            {
                var targetInput = inputs.FirstOrDefault(e => e.Name.Is(field.Key));

                if (targetInput != null)
                {
                    if (targetInput is IHtmlInputElement)
                    {
                        var input = (IHtmlInputElement)targetInput;

                        if (input.Type.Is(InputTypeNames.Radio))
                        {
                            var radios = inputs.OfType<IHtmlInputElement>().Where(i => i.Name.Is(targetInput.Name));

                            foreach (var radio in radios)
                            {
                                radio.IsChecked = radio.Value.Is(field.Value);
                            }
                        }
                        else
                        {
                            input.Value = field.Value;
                        }
                    }
                    else if (targetInput is IHtmlTextAreaElement)
                    {
                        var textarea = (IHtmlTextAreaElement)targetInput;
                        textarea.Value = field.Value;
                    }
                    else if (targetInput is IHtmlSelectElement)
                    {
                        var select = (IHtmlSelectElement)targetInput;
                        select.Value = field.Value;
                    }
                    else
                    {
                        //Silently ignore unsupported input type, e.g.,
                        //no idea if modifying keygen fields is really
                        //useful or how it is regulated.
                    }
                }
                else if (createMissing)
                {
                    var newInput = form.Owner.CreateElement<IHtmlInputElement>();
                    newInput.Type = InputTypeNames.Hidden;
                    newInput.Name = field.Key;
                    newInput.Value = field.Value;
                    form.AppendChild(newInput);
                }
                else
                {
                    var message = $"Field {field.Key} not found.";
                    throw new KeyNotFoundException(message);
                }
            }

            return form;
        }

        #endregion

        #region Navigation extensions

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
        /// Submits the given form by decomposing the object into a dictionary
        /// that contains its properties as name value pairs.
        /// </summary>
        /// <param name="form">The form to submit.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, Object fields)
        {
            return form.SubmitAsync(fields.ToDictionary());
        }

        /// <summary>
        /// Submits the given form by using the dictionary which contains name
        /// value pairs of input fields to submit.
        /// </summary>
        /// <param name="form">The form to submit.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlFormElement form, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            form.SetValues(fields, createMissing);
            return form.SubmitAsync();
        }

        /// <summary>
        /// Submits the form of the element by decomposing the object into a dictionary
        /// that contains its properties as name value pairs.
        /// </summary>
        /// <param name="element">The element to submit its form.</param>
        /// <param name="fields">The optional fields to use as values.</param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlElement element, Object fields = null)
        {
            return element.SubmitAsync(fields.ToDictionary());
        }

        /// <summary>
        /// Submits the form of the element by using the dictionary which contains name
        /// value pairs of input fields to submit.
        /// </summary>
        /// <param name="element">The element to submit its form.</param>
        /// <param name="fields">The fields to use as values.</param>
        /// <param name="createMissing">
        /// What to do if some field(s) have not been found in the form. If
        /// true, then new input will be created. Otherwise, an exception will
        /// be thrown.
        /// </param>
        /// <returns>The task eventually resulting in the response.</returns>
        public static Task<IDocument> SubmitAsync(this IHtmlElement element, IDictionary<String, String> fields, Boolean createMissing = false)
        {
            var button = element as HtmlFormControlElement;

            if (button == null)
                throw new ArgumentException(nameof(element));

            var form = button.Form;

            if (form != null)
            {
                form.SetValues(fields, createMissing);
                return form.SubmitAsync(button);
            }

            return null;
        }

        #endregion

        #region Query extensions

        /// <summary>
        /// Reduces the elements to the one at the given index, if any.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The index of the element.</param>
        /// <returns>The element, or its default value.</returns>
        public static T Eq<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Skip(index).FirstOrDefault();
        }

        /// <summary>
        /// Reduces the elements to the ones above the given index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The minimum exclusive index.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Gt<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Skip(index + 1);
        }

        /// <summary>
        /// Reduces the elements to the ones below the given index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="index">The maximum exclusive index.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Lt<T>(this IEnumerable<T> elements, Int32 index)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            return elements.Take(index);
        }

        /// <summary>
        /// Reduces the elements to the ones with even index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Even<T>(this IEnumerable<T> elements)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            var even = true;

            foreach (var element in elements)
            {
                if (even)
                {
                    yield return element;
                }

                even = !even;
            }
        }

        /// <summary>
        /// Reduces the elements to the ones with odd index.
        /// </summary>
        /// <typeparam name="T">The type of element.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <returns>The remaining elements.</returns>
        public static IEnumerable<T> Odd<T>(this IEnumerable<T> elements)
            where T : IElement
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            var odd = false;

            foreach (var element in elements)
            {
                if (odd)
                {
                    yield return element;
                }

                odd = !odd;
            }
        }

        #endregion

        #region Element extensions

        /// <summary>
        /// Gets the computed style of the given element from the current view.
        /// </summary>
        /// <param name="element">The element to compute the style for.</param>
        /// <returns>The computed style declaration if available.</returns>
        public static ICssStyleDeclaration ComputeCurrentStyle(this IElement element)
        {
            if (element == null)
                throw new ArgumentNullException(nameof(element));

            var document = element.Owner;
            var window = document?.DefaultView;
            return window?.GetComputedStyle(element);
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
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="propertyName">The name of the property to set.</param>
        /// <param name="propertyValue">
        /// The value of the property to set.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, String propertyName, String propertyValue)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (propertyName == null)
                throw new ArgumentNullException(nameof(propertyName));

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                element.Style.SetProperty(propertyName, propertyValue);
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified 
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, IEnumerable<KeyValuePair<String, String>> properties)
            where T : IEnumerable<IElement>
        {
            if (elements == null)
                throw new ArgumentNullException(nameof(elements));

            if (properties == null)
                throw new ArgumentNullException(nameof(properties));

            foreach (var element in elements.OfType<IHtmlElement>())
            {
                foreach (var property in properties)
                {
                    element.Style.SetProperty(property.Key, property.Value);
                }
            }

            return elements;
        }

        /// <summary>
        /// Extends the CSS of the given elements with the specified
        /// declarations.
        /// </summary>
        /// <typeparam name="T">The type of element collection.</typeparam>
        /// <param name="elements">The collection.</param>
        /// <param name="properties">
        /// An enumeration of properties in form of an anonymous object, that
        /// carries key-value pairs.
        /// </param>
        /// <returns>The collection itself.</returns>
        public static T Css<T>(this T elements, Object properties)
            where T : IEnumerable<IElement>
        {
            var realProperties = properties.ToDictionary();
            return elements.Css(realProperties);
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

        #endregion

        #region NamedNodeMap extensions

        /// <summary>
        /// Clears the given attribute collection.
        /// </summary>
        /// <param name="attributes">The collection to clear.</param>
        /// <returns>The collection itself.</returns>
        public static INamedNodeMap Clear(this INamedNodeMap attributes)
        {
            if (attributes == null)
                throw new ArgumentNullException(nameof(attributes));

            while (attributes.Length > 0)
            {
                var name = attributes[attributes.Length - 1].Name;
                attributes.RemoveNamedItem(name);
            }

            return attributes;
        }

        #endregion

        #region Document extensions

        /// <summary>
        /// Gets all downloads associated with resources of the document.
        /// </summary>
        /// <param name="document">The document hosting the downloads.</param>
        /// <returns>The collection of elements hosting resources.</returns>
        public static IEnumerable<IDownload> GetDownloads(this IDocument document)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            return document.All.OfType<ILoadableElement>().Select(m => m.CurrentDownload).Where(m => m != null);
        }

        /// <summary>
        /// Creates a new DocumentFragment from the given HTML code. The
        /// fragment is parsed with the Body element as context.
        /// </summary>
        /// <param name="document">The responsible document.</param>
        /// <param name="html">The HTML to transform into a fragment.</param>
        /// <returns>The fragment containing the new nodes.</returns>
        static IDocumentFragment CreateFromHtml(this IDocument document, String html)
        {
            if (document == null)
                throw new ArgumentNullException(nameof(document));

            var body = document.Body as Element;

            if (body == null)
                throw new ArgumentException("The provided document does not have a valid body element.");

            return new DocumentFragment(body, html ?? String.Empty);
        }

        #endregion

        #region Node extensions

        /// <summary>
        /// Gets the content text of the given DOM node.
        /// </summary>
        /// <param name="node">The node to stringify.</param>
        /// <returns>The text of the node and its children.</returns>
        public static String Text(this INode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            return node.TextContent;
        }

        /// <summary>
        /// Sets the text content of the given elements.
        /// </summary>
        /// <typeparam name="T">The type of collection.</typeparam>
        /// <param name="nodes">The collection.</param>
        /// <param name="text">The text that should be set.</param>
        /// <returns>The collection itself.</returns>
        public static T Text<T>(this T nodes, String text)
            where T : IEnumerable<INode>
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));

            foreach (var element in nodes)
            {
                element.TextContent = text;
            }

            return nodes;
        }

        /// <summary>
        /// Gets the index of the given item in the list of nodes.
        /// </summary>
        /// <param name="nodes">The source list of nodes.</param>
        /// <param name="item">The item to search for.</param>
        /// <returns>The index of the item or -1 if not found.</returns>
        public static Int32 Index(this IEnumerable<INode> nodes, INode item)
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));

            if (item != null)
            {
                var i = 0;

                foreach (var node in nodes)
                {
                    if (Object.ReferenceEquals(node, item))
                    {
                        return i;
                    }

                    i++;
                }
            }

            return -1;
        }

        #endregion

        #region Helpers

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

        #endregion
    }
}

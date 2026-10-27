namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// A bunch of methods for getting DOM elements on some internal collections.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        /// Gets the descendants from the provided parent that fulfill the
        /// specified predicate, if any.
        /// </summary>
        /// <typeparam name="T">The type of elements to obtain.</typeparam>
        /// <param name="parent">The parent of the descendants.</param>
        /// <param name="deep">
        /// True if all descendants, false if only direct descendants should be
        /// considered.
        /// </param>
        /// <param name="predicate">The filter function, if any.</param>
        /// <returns>The collection with the corresponding elements.</returns>
        public static IEnumerable<T> GetNodes<T>(this INode parent, Boolean deep = true, Func<T, Boolean>? predicate = null)
            where T : class, INode
        {
            predicate ??= (_ => true);
            return deep ? parent.GetAllNodes(predicate) : parent.GetDescendendElements(predicate);
        }

        /// <summary>
        /// Gets an element by its ID.
        /// </summary>
        /// <param name="children">The nodelist to investigate.</param>
        /// <param name="id">The id to find.</param>
        /// <returns>The element or null.</returns>
        public static IElement? GetElementById(this INodeList children, String id)
        {
            for (var i = 0; i < children.Length; i++)
            {
                if (children[i] is IElement element)
                {
                    if (element.Id.Is(id))
                    {
                        return element;
                    }

                    // The children of the library's own elements take the concrete walk below,
                    // which needs no interface calls and cannot overflow the call stack
                    element = (element is Element known ? GetElementById(known.ChildNodes, id, 1) : element.ChildNodes.GetElementById(id))!;

                    if (element != null)
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        // Recurses for the first levels and continues with an explicit stack below them, so deeply
        // nested documents cannot overflow the call stack
        private static Element? GetElementById(NodeList children, String id, Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                return GetElementByIdIteratively(children, id);
            }

            for (var i = 0; i < children.Length; i++)
            {
                if (children[i] is Element element)
                {
                    if (element.Id.Is(id))
                    {
                        return element;
                    }

                    element = GetElementById(element.ChildNodes, id, depth + 1)!;

                    if (element != null)
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        private static Element? GetElementByIdIteratively(NodeList children, String id)
        {
            // An explicit stack, so deeply nested documents cannot overflow the call stack
            var visitor = new IdVisitor(id);
            return ElementWalker.Walk(children, ref visitor);
        }

        /// <summary>
        /// Gets a list of HTML elements given by their name attribute.
        /// </summary>
        /// <param name="children">The list to investigate.</param>
        /// <param name="name">The name attribute's value.</param>
        /// <param name="result">The result collection.</param>
        public static void GetElementsByName(this INodeList children, String name, List<IElement> result)
        {
            for (var i = 0; i < children.Length; i++)
            {
                if (children[i] is IElement element)
                {
                    if (element.GetAttribute(null, AttributeNames.Name).Is(name))
                    {
                        result.Add(element);
                    }

                    // See GetElementById above
                    if (element is Element known)
                    {
                        GetElementsByName(known.ChildNodes, name, result, 1);
                    }
                    else
                    {
                        element.ChildNodes.GetElementsByName(name, result);
                    }
                }
            }
        }

        // Recurses for the first levels and continues with an explicit stack below them, so deeply
        // nested documents cannot overflow the call stack
        private static void GetElementsByName(NodeList children, String name, List<IElement> result, Int32 depth)
        {
            if (depth == ChildIndexStack.RecursionLimit)
            {
                GetElementsByNameIteratively(children, name, result);
                return;
            }

            for (var i = 0; i < children.Length; i++)
            {
                if (children[i] is Element element)
                {
                    if (element.GetAttribute(null, AttributeNames.Name).Is(name))
                    {
                        result.Add(element);
                    }

                    GetElementsByName(element.ChildNodes, name, result, depth + 1);
                }
            }
        }

        private static void GetElementsByNameIteratively(NodeList children, String name, List<IElement> result)
        {
            var visitor = new NameVisitor(name, result);
            ElementWalker.Walk(children, ref visitor);
        }

        private readonly struct IdVisitor(String id) : IElementVisitor
        {
            public Boolean Visit(Element element) => element.Id.Is(id);
        }

        private readonly struct NameVisitor(String name, List<IElement> result) : IElementVisitor
        {
            public Boolean Visit(Element element)
            {
                if (element.GetAttribute(null, AttributeNames.Name).Is(name))
                {
                    result.Add(element);
                }

                return false;
            }
        }

        /// <summary>
        /// Determines if the current filter settings include the provided
        /// node.
        /// </summary>
        /// <param name="filter">The filter settings to use.</param>
        /// <param name="node">The node to check against.</param>
        /// <returns>True if the node is accepted, otherwise false.</returns>
        public static Boolean Accepts(this FilterSettings filter, INode node)
        {
            return node.NodeType switch
            {
                NodeType.Attribute              => (filter & FilterSettings.Attribute) == FilterSettings.Attribute,
                NodeType.CharacterData          => (filter & FilterSettings.CharacterData) == FilterSettings.CharacterData,
                NodeType.Comment                => (filter & FilterSettings.Comment) == FilterSettings.Comment,
                NodeType.Document               => (filter & FilterSettings.Document) == FilterSettings.Document,
                NodeType.DocumentFragment       => (filter & FilterSettings.DocumentFragment) == FilterSettings.DocumentFragment,
                NodeType.DocumentType           => (filter & FilterSettings.DocumentType) == FilterSettings.DocumentType,
                NodeType.Element                => (filter & FilterSettings.Element) == FilterSettings.Element,
                NodeType.Entity                 => (filter & FilterSettings.Entity) == FilterSettings.Entity,
                NodeType.EntityReference        => (filter & FilterSettings.EntityReference) == FilterSettings.EntityReference,
                NodeType.ProcessingInstruction  => (filter & FilterSettings.ProcessingInstruction) == FilterSettings.ProcessingInstruction,
                NodeType.Notation               => (filter & FilterSettings.Notation) == FilterSettings.Notation,
                NodeType.Text                   => (filter & FilterSettings.Text) == FilterSettings.Text,
                _                               => filter == FilterSettings.All
            };
        }

        /// <summary>
        /// Gets the element with the provided id, if any. Otherwise the
        /// element with the same name is searched.
        /// </summary>
        /// <typeparam name="T">The type of node to obtain.</typeparam>
        /// <param name="elements">The list of elements to filter.</param>
        /// <param name="id">The id of the element to find.</param>
        /// <returns>The element with the given id, or null.</returns>
        public static T? GetElementById<T>(this IEnumerable<T> elements, String id)
            where T : class, IElement
        {
            // Single pass: an id match always wins, even one found after a name match, so the
            // first name match found is only returned once the whole sequence is exhausted
            // without an id match. This preserves the two-pass ordering (any id beats every
            // name) while enumerating a potentially lazily-built sequence only once.
            T? nameMatch = null;

            foreach (var element in elements)
            {
                if (element.Id.Is(id))
                {
                    return element;
                }

                if (nameMatch is null && element.GetAttribute(null, AttributeNames.Name).Is(id))
                {
                    nameMatch = element;
                }
            }

            return nameMatch;
        }

        private static IEnumerable<T> GetAllNodes<T>(this INode parent, Func<T, Boolean> predicate)
            where T : class, INode
            => new NodeEnumerable(parent).OfType<T>().Where(predicate);

        private static IEnumerable<T> GetDescendendElements<T>(this INode parent, Func<T, Boolean> predicate)
            where T : class, INode
        {
            for (var i = 0; i < parent.ChildNodes.Length; i++)
            {
                if (parent.ChildNodes[i] is T child && predicate(child))
                {
                    yield return child;
                }
            }
        }
    }
}

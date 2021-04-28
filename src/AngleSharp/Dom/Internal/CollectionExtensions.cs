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
        /// Gets the descendents from the provided parent that fulfill the
        /// specified predicate, if any.
        /// </summary>
        /// <typeparam name="T">The type of elements to obtain.</typeparam>
        /// <param name="parent">The parent of the descendents.</param>
        /// <param name="deep">
        /// True if all descendents, false if only direct descendents should be
        /// considered.
        /// </param>
        /// <param name="predicate">The filter function, if any.</param>
        /// <returns>The collection with the corresponding elements.</returns>
        public static IEnumerable<T> GetNodes<T>(this INode parent, Boolean deep = true, Func<T, Boolean>? predicate = null)
            where T : class, INode
        {
            predicate ??= (m => true);
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

                    element = element.ChildNodes.GetElementById(id)!;

                    if (element != null)
                    {
                        return element;
                    }
                }
            }

            return null;
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

                    element.ChildNodes.GetElementsByName(name, result);
                }
            }
        }

        /// <summary>
        /// Determines if the current filter settings includes the provided
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
            foreach (var element in elements)
            {
                if (element.Id.Is(id))
                {
                    return element;
                }
            }

            foreach (var element in elements)
            {
                if (element.GetAttribute(null, AttributeNames.Name).Is(id))
                {
                    return element;
                }
            }

            return null;
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

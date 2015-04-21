namespace AngleSharp.Extensions
{
    using AngleSharp.Dom;
    using AngleSharp.Html;
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A bunch of methods for getting DOM elements.
    /// </summary>
    [DebuggerStepThrough]
    static class CollectionExtensions
    {
        #region Methods

        /// <summary>
        /// Returns the concatenation of the provided enumerable with the
        /// specified element.
        /// </summary>
        /// <typeparam name="T">The type of the enumeration.</typeparam>
        /// <param name="items">The items to lead.</param>
        /// <param name="element">The item to concat.</param>
        /// <returns>The new enumerable.</returns>
        public static IEnumerable<T> Concat<T>(this IEnumerable<T> items, T element)
        {
            foreach (var item in items)
                yield return item;

            yield return element;
        }

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
        public static IEnumerable<T> GetElements<T>(this INode parent, Boolean deep = true, Predicate<T> predicate = null)
            where T : class, INode
        {
            predicate = predicate ?? (m => true);
            return deep ? parent.GetAllElements(predicate) : parent.GetDescendendElements(predicate);
        }

        /// <summary>
        /// Gets an element by its ID.
        /// </summary>
        /// <param name="children">The nodelist to investigate.</param>
        /// <param name="id">The id to find.</param>
        /// <returns>The element or null.</returns>
        public static IElement GetElementById(this INodeList children, String id)
        {
            for (int i = 0; i < children.Length; i++)
            {
                var element = children[i] as IElement;

                if (element != null)
                {
                    if (element.Id == id)
                        return element;

                    element = element.ChildNodes.GetElementById(id);

                    if (element != null)
                        return element;
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
            for (int i = 0; i < children.Length; i++)
            {
                var element = children[i] as IElement;

                if (element != null)
                {
                    if (element.GetAttribute(null, AttributeNames.Name) == name)
                        result.Add(element);

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
            switch (node.NodeType)
            {
                case NodeType.Attribute:             return filter.HasFlag(FilterSettings.Attribute);
                case NodeType.CharacterData:         return filter.HasFlag(FilterSettings.CharacterData);
                case NodeType.Comment:               return filter.HasFlag(FilterSettings.Comment);
                case NodeType.Document:              return filter.HasFlag(FilterSettings.Document);
                case NodeType.DocumentFragment:      return filter.HasFlag(FilterSettings.DocumentFragment);
                case NodeType.DocumentType:          return filter.HasFlag(FilterSettings.DocumentType);
                case NodeType.Element:               return filter.HasFlag(FilterSettings.Element);
                case NodeType.Entity:                return filter.HasFlag(FilterSettings.Entity);
                case NodeType.EntityReference:       return filter.HasFlag(FilterSettings.EntityReference);
                case NodeType.ProcessingInstruction: return filter.HasFlag(FilterSettings.ProcessingInstruction);
                case NodeType.Notation:              return filter.HasFlag(FilterSettings.Notation);
                case NodeType.Text:                  return filter.HasFlag(FilterSettings.Text);
            }

            return filter == FilterSettings.All;
        }

        /// <summary>
        /// Gets the elements that satisfy the provided filter settings.
        /// </summary>
        /// <typeparam name="T">The type of nodes to obtain.</typeparam>
        /// <param name="parent">The parent of the nodes to find.</param>
        /// <param name="filter">The filter settings to apply.</param>
        /// <returns>
        /// The filtered list of all descendents from the provided node.
        /// </returns>
        public static IEnumerable<T> GetElements<T>(this INode parent, FilterSettings filter)
            where T : class, INode
        {
            return parent.GetElements<T>(predicate: (node => filter.Accepts(node)));
        }

        /// <summary>
        /// Gets the element with the provided id, if any. Otherwise the
        /// element with the same name is searched.
        /// </summary>
        /// <typeparam name="T">The type of node to obtain.</typeparam>
        /// <param name="elements">The list of elements to filter.</param>
        /// <param name="id">The id of the element to find.</param>
        /// <returns>The element with the given id, or null.</returns>
        public static T GetElementById<T>(this IEnumerable<T> elements, String id)
            where T : class, IElement
        {
            foreach (var element in elements)
            {
                if (element.Id == id)
                    return element;
            }

            foreach (var element in elements)
            {
                if (element.GetAttribute(null, AttributeNames.Name) == id)
                    return element;
            }

            return null;
        }

        #endregion

        #region Helpers

        static IEnumerable<T> GetAllElements<T>(this INode parent, Predicate<T> predicate)
            where T : class, INode
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null && predicate(child))
                    yield return child;

                foreach (var element in parent.ChildNodes[i].GetAllElements<T>(predicate))
                    yield return element;
            }
        }

        static IEnumerable<T> GetDescendendElements<T>(this INode parent, Predicate<T> predicate)
            where T : class, INode
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null && predicate(child))
                    yield return child;
            }
        }

        #endregion
    }
}

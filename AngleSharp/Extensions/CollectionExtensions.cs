namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;

    /// <summary>
    /// A bunch of methods for getting DOM elements.
    /// </summary>
    [DebuggerStepThrough]
    static class CollectionExtensions
    {
        public static IEnumerable<T> GetElements<T>(this INode parent, Boolean deep = true, Predicate<T> predicate = null)
            where T : class, INode
        {
            predicate = predicate ?? (m => true);

            if (deep)
                return GetAllElementsOf<T>(parent, predicate);
            else
                return GetOnlyElementsOf<T>(parent, predicate);
        }

        /// <summary>
        /// Gets an element by its ID.
        /// </summary>
        /// <param name="children">The nodelist to investigate.</param>
        /// <param name="id">The id to find.</param>
        /// <returns>The element or NULL.</returns>
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
                    if (element.GetAttribute(AttributeNames.Name) == name)
                        result.Add(element);

                    element.ChildNodes.GetElementsByName(name, result);
                }
            }
        }

        public static Boolean Accepts(this FilterSettings filter, INode node)
        {
            switch (node.NodeType)
            {
                case NodeType.Attribute:
                    return filter.HasFlag(FilterSettings.Attribute);
                case NodeType.CharacterData:
                    return filter.HasFlag(FilterSettings.CharacterData);
                case NodeType.Comment:
                    return filter.HasFlag(FilterSettings.Comment);
                case NodeType.Document:
                    return filter.HasFlag(FilterSettings.Document);
                case NodeType.DocumentFragment:
                    return filter.HasFlag(FilterSettings.DocumentFragment);
                case NodeType.DocumentType:
                    return filter.HasFlag(FilterSettings.DocumentType);
                case NodeType.Element:
                    return filter.HasFlag(FilterSettings.Element);
                case NodeType.Entity:
                    return filter.HasFlag(FilterSettings.Entity);
                case NodeType.EntityReference:
                    return filter.HasFlag(FilterSettings.EntityReference);
                case NodeType.ProcessingInstruction:
                    return filter.HasFlag(FilterSettings.ProcessingInstruction);
                case NodeType.Notation:
                    return filter.HasFlag(FilterSettings.Notation);
                case NodeType.Text:
                    return filter.HasFlag(FilterSettings.Text);
            }

            return filter == FilterSettings.All;
        }

        public static IEnumerable<T> GetElements<T>(this INode parent, FilterSettings filter)
            where T : class, INode
        {
            return parent.GetElements<T>(predicate: (node => filter.Accepts(node)));
        }

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
                if (element.GetAttribute(AttributeNames.Name) == id)
                    return element;
            }

            return null;
        }

        static IEnumerable<T> GetAllElementsOf<T>(INode parent, Predicate<T> predicate)
            where T : class, INode
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null && predicate(child))
                    yield return child;

                foreach (var element in GetAllElementsOf<T>(parent.ChildNodes[i], predicate))
                    yield return element;
            }
        }

        static IEnumerable<T> GetOnlyElementsOf<T>(INode parent, Predicate<T> predicate)
            where T : class, INode
        {
            for (int i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null && predicate(child))
                    yield return child;
            }
        }
    }
}

namespace AngleSharp.Dom
{
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    /// <summary>
    /// A bunch of methods for getting DOM elements.
    /// </summary>
    static class CollectionExtensions
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
            for (var i = 0; i < children.Length; i++)
            {
                var element = children[i] as IElement;

                if (element != null)
                {
                    if (element.Id.Is(id))
                    {
                        return element;
                    }

                    element = element.ChildNodes.GetElementById(id);

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
                var element = children[i] as IElement;

                if (element != null)
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
            switch (node.NodeType)
            {
                case NodeType.Attribute:             return (filter & FilterSettings.Attribute) == FilterSettings.Attribute;
                case NodeType.CharacterData:         return (filter & FilterSettings.CharacterData) == FilterSettings.CharacterData;
                case NodeType.Comment:               return (filter & FilterSettings.Comment) == FilterSettings.Comment;
                case NodeType.Document:              return (filter & FilterSettings.Document) == FilterSettings.Document;
                case NodeType.DocumentFragment:      return (filter & FilterSettings.DocumentFragment) == FilterSettings.DocumentFragment;
                case NodeType.DocumentType:          return (filter & FilterSettings.DocumentType) == FilterSettings.DocumentType;
                case NodeType.Element:               return (filter & FilterSettings.Element) == FilterSettings.Element;
                case NodeType.Entity:                return (filter & FilterSettings.Entity) == FilterSettings.Entity;
                case NodeType.EntityReference:       return (filter & FilterSettings.EntityReference) == FilterSettings.EntityReference;
                case NodeType.ProcessingInstruction: return (filter & FilterSettings.ProcessingInstruction) == FilterSettings.ProcessingInstruction;
                case NodeType.Notation:              return (filter & FilterSettings.Notation) == FilterSettings.Notation;
                case NodeType.Text:                  return (filter & FilterSettings.Text) == FilterSettings.Text;
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

        private static IEnumerable<T> GetAllElements<T>(this INode parent, Predicate<T> predicate)
            where T : class, INode
            => new NodeEnumerable(parent)
            .OfType<T>()
            .Where(node => predicate(node));

        private static IEnumerable<T> GetDescendendElements<T>(this INode parent, Predicate<T> predicate)
            where T : class, INode
        {
            for (var i = 0; i < parent.ChildNodes.Length; i++)
            {
                var child = parent.ChildNodes[i] as T;

                if (child != null && predicate(child))
                {
                    yield return child;
                }
            }
        }

        private class NodeEnumerable : IEnumerable<INode>
        {
            private readonly INode _startingNode;

            public NodeEnumerable(INode startingNode)
            {
                _startingNode = startingNode;
            }

            public IEnumerator<INode> GetEnumerator() => new NodeEnumerator(_startingNode);
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        private class NodeEnumerator : IEnumerator<INode>
        {
            private readonly Stack<EnumerationFrame> _frameStack = new Stack<EnumerationFrame>();

            public NodeEnumerator(INode startingNode)
            {
                TryPushFrame(startingNode, 0);
            }

            public INode Current { get; private set; }
            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (_frameStack.Count == 0)
                    return false;

                var currentFrame = _frameStack.Pop();
                Current = currentFrame.Parent.ChildNodes[currentFrame.ChildIndex];

                TryPushFrame(currentFrame.Parent, currentFrame.ChildIndex + 1);
                TryPushFrame(Current, 0);

                return true;
            }

            private void TryPushFrame(INode parent, int childIndex)
            {
                if (childIndex < parent.ChildNodes.Length)
                    _frameStack.Push(new EnumerationFrame(parent, childIndex));
            }

            public void Dispose() { }
            public void Reset() { throw new NotSupportedException(); }
        }

        private struct EnumerationFrame
        {
            public EnumerationFrame(INode parent, int childIndex)
            {
                Parent = parent;
                ChildIndex = childIndex;
            }

            public INode Parent { get; }
            public int ChildIndex { get; }
        }
    }
}

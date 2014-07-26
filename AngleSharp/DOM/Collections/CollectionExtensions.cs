namespace AngleSharp.DOM.Collections
{
    using System;
    using System.Collections.Generic;

    static class CollectionExtensions
    {
        public static IEnumerable<T> GetElements<T>(this INode parent, Boolean deep = true, Predicate<T> predicate = null)
            where T : class, IElement
        {
            predicate = predicate ?? (m => true);

            if (deep)
                return GetAllElementsOf<T>(parent, predicate);
            else
                return GetOnlyElementsOf<T>(parent, predicate);
        }

        public static IEnumerable<T> GetElements<T>(this INode parent, FilterSettings filter)
            where T : class, IElement
        {
            return parent.GetElements<T>(predicate: (node =>
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
                        return filter.HasFlag(FilterSettings.ShowNotation);
                    case NodeType.Text:
                        return filter.HasFlag(FilterSettings.Text);
                }

                return filter == FilterSettings.All;
            }));
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
            where T : class, IElement
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
            where T : class, IElement
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

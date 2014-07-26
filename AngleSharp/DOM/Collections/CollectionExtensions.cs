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

using System;
using System.Collections.Generic;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// An HMTL Live collection that also searches for attribute matches.
    /// </summary>
    /// <typeparam name="T">The basic type of this live collection.</typeparam>
    sealed class HTMLLiveCollectionWithAttr<T> : HTMLLiveCollection
        where T : Element
    {
        #region Members

        String _attribute;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML live collection with attribute searching.
        /// This is always a deep search.
        /// </summary>
        /// <param name="parent">The parent for this live collection.</param>
        /// <param name="attribute">The attribute name to search for.</param>
        public HTMLLiveCollectionWithAttr(Node parent, String attribute)
            : base(parent, true)
        {
            _attribute = attribute;
        }

        #endregion

        #region Helpers

        protected override IEnumerable<Element> GetElements()
        {
            return GetElementsOf(Parent);
        }

        IEnumerable<Element> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if (child is T && child.Attributes[_attribute] != null)
                    yield return (Element)child;

                foreach (var element in GetElementsOf(child))
                    yield return element;
            }
        }

        #endregion
    }

    /// <summary>
    /// An HMTL Live collection that also searches for attribute matches.
    /// </summary>
    /// <typeparam name="T1">The first type to search for.</typeparam>
    /// <typeparam name="T2">The second type to search for.</typeparam>
    sealed class HTMLLiveCollectionWithAttr<T1, T2> : HTMLLiveCollection
        where T1 : Element
        where T2 : Element
    {
        #region Members

        String _attribute;

        #endregion

        #region ctor

        /// <summary>
        /// Creates a new HTML live collection with attribute searching.
        /// This is always a deep search.
        /// </summary>
        /// <param name="parent">The parent for this live collection.</param>
        /// <param name="attribute">The attribute name to search for.</param>
        public HTMLLiveCollectionWithAttr(Node parent, String attribute)
            : base(parent, true)
        {
            _attribute = attribute;
        }

        #endregion

        #region Helpers

        protected override IEnumerable<Element> GetElements()
        {
            return GetElementsOf(Parent);
        }

        IEnumerable<Element> GetElementsOf(Node parent)
        {
            foreach (var child in parent.ChildNodes)
            {
                if ((child is T1 || child is T2) && child.Attributes[_attribute] != null)
                    yield return (Element)child;

                foreach (var element in GetElementsOf(child))
                    yield return element;
            }
        }

        #endregion
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Runtime.CompilerServices;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// A collection of HTML nodes.
    /// </summary>
    [DOM("HTMLCollection")]
    public abstract class HTMLCollection : IEnumerable<Element>
    {
        #region ctor

        /// <summary>
        /// Creates a new list of HTML elements.
        /// </summary>
        internal HTMLCollection()
        {
        }

        #endregion

        #region Index

        /// <summary>
        /// Gets the specific node whose id matches the string specified by the attribute.
        /// Matching by name is only done as a last resort, only in HTML, and only if the referenced
        /// element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="index">The 0-based index of the element.</param>
        /// <returns>The element at the specified index.</returns>
        [DOM("item")]
        public Element this[Int32 index]
        {
            get { return GetItem(index); }
        }

        /// <summary>
        /// Gets the specific node whose id matches the string specified by the attribute.
        /// Matching by name is only done as a last resort, only in HTML, and only if the referenced
        /// element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        /// <returns>The element(s) with the specified identifier.</returns>
        [DOM("namedItem")]
        public Object this[String id]
        {
            get { return GetItem(id); }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the number of nodes in the list.
        /// </summary>
        [DOM("length")]
        public Int32 Length
        {
            get { return GetLength(); }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Gets the index of the given element.
        /// </summary>
        /// <param name="element">The element to find.</param>
        /// <returns>The index of the element.</returns>
        internal abstract Int32 IndexOf(Element element);

        /// <summary>
        /// Gets the item at the specified index.
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The item at the specified index.</returns>
        protected abstract Element GetItem(Int32 index);

        /// <summary>
        /// Gets the item with the specified id.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        /// <returns>The item with with the id or null.</returns>
        protected virtual Object GetItem(String id)
        {
            var result = new List<Element>();

            for (int i = 0; i < Length; i++)
            {
                if (this[i].Id == id)
                    result.Add(this[i]);
            }

            if (result.Count == 0)
            {
                for (int i = 0; i < Length; i++)
                {
                    if (this[i].GetAttribute("name") == id)
                        result.Add(this[i]);
                }
            }

            if (result.Count == 1)
                return result[0];
            else if (result.Count != 0)
                return new HTMLStaticCollection(result);

            return null;
        }

        /// <summary>
        /// Gets the number of items in the collection.
        /// </summary>
        /// <returns>The number.</returns>
        protected abstract Int32 GetLength();

        #endregion

        #region IEnumerable Implementation

        /// <summary>
        /// Gets an enumerator over the contained elements.
        /// </summary>
        /// <returns>The enumerator.</returns>
        public abstract IEnumerator<Element> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion
    }
}

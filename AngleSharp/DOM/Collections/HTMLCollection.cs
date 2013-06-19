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
    public class HTMLCollection : BaseCollection<Element>
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
        /// Gets a node within the list of nodes.
        /// </summary>
        /// <param name="id">The id of the node.</param>
        /// <returns>The node with the specified identifier.</returns>
        public Object this[String id]
        {
            get { return NamedItem(id); }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns the specific node whose id matches the string specified by the attribute.
        /// Matching by name is only done as a last resort, only in HTML, and only if the referenced
        /// element supports the name attribute. Returns null if no node exists by the given name.
        /// </summary>
        /// <param name="id">The id of the element.</param>
        /// <returns>The found element.</returns>
        [DOM("namedItem")]
        public virtual Object NamedItem(String id)
        {
            var result = new HTMLCollection();

            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Id == id)
                    result.Add(_entries[i]);
            }

            if (result.Length == 1)
                return result[0];
            else if (result.Length != 0)
                return result;

            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].GetAttribute("name") == id)
                    result.Add(_entries[i]);
            }

            if (result.Length == 1)
                return result[0];
            else if (result.Length != 0)
                return result;

            return null;
        }

        #endregion
    }
}

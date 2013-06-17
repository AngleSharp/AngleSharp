using System;

namespace AngleSharp.DOM.Collections
{
    /// <summary>
    /// A collection of HTML form controls.
    /// </summary>
    public class HTMLFormControlsCollection : HTMLCollection
    {
        /// <summary>
        /// Gets the node or list of nodes in the collection whose name or id match the specified name,
        /// or null if no nodes match.
        /// </summary>
        /// <param name="name">The name or id of the element(s).</param>
        /// <returns>The found element(s).</returns>
        public override Object NamedItem(String name)
        {
            var result = new HTMLCollection();

            for (int i = 0; i < _entries.Count; i++)
            {
                if (_entries[i].Id == name || _entries[i].GetAttribute("name") == name)
                    result.Add(_entries[i]);
            }

            if (result.Length == 0)
                return null;
            else if (result.Length == 1)
                return result[0];

            return result;
        }
    }
}

using System;

namespace AngleSharp.DOM.Xml
{
    /// <summary>
    /// The object representation of an XMLElement.
    /// </summary>
    public class XMLElement : Element
    {        
        #region ctor

        /// <summary>
        /// Creates a new XML element.
        /// </summary>
        public XMLElement()
        {
            NamespaceURI = Namespaces.Xml;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Returns a duplicate of the node on which this method was called.
        /// </summary>
        /// <param name="deep">Optional value: true if the children of the node should also be cloned, or false to clone only the specified node.</param>
        /// <returns>The duplicate node.</returns>
        public override Node CloneNode(bool deep = true)
        {
            var node = Factory(_name);
            CopyProperties(this, node, deep);
            return node;
        }

        /// <summary>
        /// Returns a specialized SVGElement instance for the given tag name.
        /// </summary>
        /// <param name="tagName">The given tag name.</param>
        /// <returns>The specialized SVGElement instance.</returns>
        public static XMLElement Factory(string tagName)
        {
            return new XMLElement { _name = tagName };
        }

        #endregion
    }
}

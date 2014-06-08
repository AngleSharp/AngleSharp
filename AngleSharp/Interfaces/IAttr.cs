namespace AngleSharp.DOM
{
    using System;

    /// <summary>
    /// This type represents a DOM element's attribute as an object. 
    /// </summary>
    [DOM("Attr")]
    public interface IAttr
    {
        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        [DOM("localName")]
        String LocalName { get; }

        /// <summary>
        /// Gets the attribute's name.
        /// </summary>
        [DOM("name")]
        String Name { get; }

        /// <summary>
        /// Gets the attribute's value.
        /// </summary>
        [DOM("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets the namespace URL of the attribute.
        /// </summary>
        [DOM("namespaceURI")]
        String NamespaceUri { get; }

        /// <summary>
        /// Gets the prefix used by the namespace.
        /// </summary>
        [DOM("prefix")]
        String Prefix { get; }
    }
}

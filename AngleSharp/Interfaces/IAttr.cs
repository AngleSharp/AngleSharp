namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// This type represents a DOM element's attribute as an object. 
    /// </summary>
    [DomName("Attr")]
    public interface IAttr
    {
        /// <summary>
        /// Gets the local name of the attribute.
        /// </summary>
        [DomName("localName")]
        String LocalName { get; }

        /// <summary>
        /// Gets the attribute's name.
        /// </summary>
        [DomName("name")]
        String Name { get; }

        /// <summary>
        /// Gets the attribute's value.
        /// </summary>
        [DomName("value")]
        String Value { get; set; }

        /// <summary>
        /// Gets the namespace URL of the attribute.
        /// </summary>
        [DomName("namespaceURI")]
        String NamespaceUri { get; }

        /// <summary>
        /// Gets the prefix used by the namespace.
        /// </summary>
        [DomName("prefix")]
        String Prefix { get; }
    }
}

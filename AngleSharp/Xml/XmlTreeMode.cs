using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Possible insertation mode values.
    /// </summary>
    enum XmlTreeMode
    {
        /// <summary>
        /// The initial mode - waiting for the XML declaration.
        /// </summary>
        Initial,
        /// <summary>
        /// The prolog (before the doctype or any element).
        /// </summary>
        Prolog,
        /// <summary>
        /// The body (after the doctype or first element).
        /// </summary>
        Body
    }
}

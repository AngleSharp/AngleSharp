namespace AngleSharp.Dom.Css
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents a namespace declaration CSS rule.
    /// </summary>
    [DomName("CSSNamespaceRule")]
    public interface ICssNamespaceRule : ICssRule
    {
        /// <summary>
        /// Gets the URI of the given namespace.
        /// </summary>
        [DomName("namespaceURI")]
        String NamespaceUri { get; set; }

        /// <summary>
        /// Gets the prefix associated to this namespace.
        /// If there is no such prefix, returns null.
        /// </summary>
        [DomName("prefix")]
        String Prefix { get; set; }
    }
}

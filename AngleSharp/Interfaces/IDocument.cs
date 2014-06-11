namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// The Document interface serves as an entry point to the web page's content.
    /// </summary>
    [DomName("Document")]
    public interface IDocument : INode, IParentNode
    {
        /// <summary>
        /// Gets the DOM implementation associated with the current document.
        /// </summary>
        [DomName("implementation")]
        IImplementation Implementation { get; }

        /// <summary>
        /// Gets the URI of the current document.
        /// </summary>
        [DomName("documentURI")]
        String DocumentUri { get; }

        /// <summary>
        /// Gets the character encoding of the current document.
        /// </summary>
        [DomName("characterSet")]
        String CharacterSet { get; }

        /// <summary>
        /// Gets a value to indicate whether the document is rendered in Quirks mode (BackComp) 
        /// or Strict mode (CSS1Compat).
        /// </summary>
        [DomName("compatMode")]
        String CompatMode { get; }

        /// <summary>
        /// Gets a string containing the URL of the current document.
        /// </summary>
        [DomName("URL")]
        String Url { get; }

        /// <summary>
        /// Gets the Content-Type from the MIME Header of the current document.
        /// </summary>
        [DomName("contentType")]
        String ContentType { get; }

        /// <summary>
        /// Gets the document type.
        /// </summary>
        [DomName("doctype")]
        IDocumentType Doctype { get; }

        /// <summary>
        /// Gets the root element of the document.
        /// </summary>
        [DomName("documentElement")]
        IElement DocumentElement { get; }

        /// <summary>
        /// Creates an event of the type specified. 
        /// </summary>
        /// <param name="type">Represents the type of event to be created.</param>
        [DomName("createEvent")]
        IEvent CreateEvent(String type);

        /// <summary>
        /// Creates a new Range object.
        /// </summary>
        [DomName("createRange")]
        IRange CreateRange();

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">A string representing the list of class names to match; class names are separated by whitespace.</param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        IHtmlCollection GetElementsByClassName(String classNames);

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">A string representing the name of the elements. The special string "*" represents all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        [DomName("getElementsByTagName")]
        IHtmlCollection GetElementsByTagName(String tagName);

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the given namespace.
        /// The complete document is searched, including the root node.
        /// </summary>
        /// <param name="namespaceUri">The namespace URI of elements to look for.</param>
        /// <param name="tagName">Either the local name of elements to look for or the special value "*", which matches all elements.</param>
        /// <returns>A collection of elements in the order they appear in the tree.</returns>
        [DomName("getElementsByTagNameNS")]
        IHtmlCollection GetElementsByTagNameNS(String namespaceUri, String tagName);

        /// <summary>
        /// Returns the Element whose ID is given by elementId. If no such element exists, returns null.
        /// The behavior is not defined if more than one element have this ID.
        /// </summary>
        /// <param name="elementId">A case-sensitive string representing the unique ID of the element being sought.</param>
        /// <returns>The matching element.</returns>
        [DomName("getElementById")]
        Element GetElementById(String elementId);

        IElement ActiveElement { get; }
        Attr CreateAttribute(String name);
        Attr CreateAttributeNS(String namespaceURI, String name);
        IComment CreateComment(String data);
        IDocumentFragment CreateDocumentFragment();
        Element CreateElement(String tagName);
        Element CreateElementNS(String namespaceURI, String tagName);
        EntityReference CreateEntityReference(String name);
        IProcessingInstruction CreateProcessingInstruction(String target, String data);
        IText CreateTextNode(String data);
        IWindow DefaultView { get; }
        IWindow ParentWindow { get; }
        String InputEncoding { get; }
        DateTime LastModified { get; }
        Location Location { get; set; }
        Readiness ReadyState { get; set; }
        event EventHandler OnReadyStateChange;
        String Referrer { get; }
        DOMStringList StyleSheetSets { get; }
    }
}

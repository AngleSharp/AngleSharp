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
        IElement GetElementById(String elementId);

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
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">A string containing the data to be added to the Comment.</param>
        /// <returns>The new comment.</returns>
        [DomName("createComment")]
        IComment CreateComment(String data);

        /// <summary>
        /// Creates an empty DocumentFragment object.
        /// </summary>
        /// <returns>The new document fragment.</returns>
        [DomName("createDocumentFragment")]
        IDocumentFragment CreateDocumentFragment();

        /// <summary>
        /// Creates a new element with the given tag name.
        /// </summary>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element object.</returns>
        [DomName("createElement")]
        IElement CreateElement(String tagName);

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceUri">Specifies the namespace URI to associate with the element.</param>
        /// <param name="tagName">A string that specifies the type of element to be created.</param>
        /// <returns>The created element.</returns>
        [DomName("createElementNS")]
        IElement CreateElementNS(String namespaceUri, String tagName);

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and data strings.
        /// </summary>
        /// <param name="target">The target part of the processing instruction.</param>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new processing instruction.</returns>
        [DomName("createProcessingInstruction")]
        IProcessingInstruction CreateProcessingInstruction(String target, String data);

        /// <summary>
        /// Creates a new text node and returns it..
        /// </summary>
        /// <param name="data">A string containing the data to be put in the text node.</param>
        /// <returns>The created text node.</returns>
        [DomName("createTextNode")]
        IText CreateTextNode(String data);

        /// <summary>
        /// Creates a new NodeIterator object.
        /// </summary>
        /// <param name="root">The root node at which to begin the NodeIterator's traversal.</param>
        /// <param name="settings">Indicates which nodes to iterate over.</param>
        /// <param name="filter">An optional callback function for filtering.</param>
        /// <returns>The created node NodeIterator.</returns>
        [DomName("createNodeIterator")]
        INodeIterator CreateNodeIterator(INode root, FilterSetting settings = FilterSetting.All, NodeFilter filter = null);

        IWindow DefaultView { get; }
        IWindow ParentWindow { get; }
        String InputEncoding { get; }
        DateTime LastModified { get; }
        Location Location { get; set; }
        Readiness ReadyState { get; set; }
        event EventHandler OnReadyStateChange;
        String Referrer { get; }
        DOMStringList StyleSheetSets { get; }
        IElement ActiveElement { get; }
    }
}

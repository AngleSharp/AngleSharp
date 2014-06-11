namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    /// <summary>
    /// The Document interface serves as an entry point to the web page's content.
    /// </summary>
    [DomName("Document")]
    public interface IDocument : INode, IQueryElements
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
        /// Appends nodes to current document.
        /// </summary>
        /// <param name="nodes">The nodes to append.</param>
        [DomName("append")]
        void Append(params INode[] nodes);

        /// <summary>
        /// Prepends nodes to the current document.
        /// </summary>
        /// <param name="nodes">The nodes to prepend.</param>
        [DomName("prepend")]
        void Prepend(params INode[] nodes);

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
        Element GetElementById(String elementId);
        String InputEncoding { get; }
        DateTime LastModified { get; }
        Location Location { get; set; }
        Readiness ReadyState { get; set; }
        event EventHandler OnReadyStateChange;
        String Referrer { get; }
        DOMStringList StyleSheetSets { get; }
    }
}

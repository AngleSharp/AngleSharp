namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Events;
    using AngleSharp.Dom.Html;
    using System;

    /// <summary>
    /// The Document interface serves as an entry point to the web page's
    /// content.
    /// </summary>
    [DomName("Document")]
    public interface IDocument : INode, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
    {
        /// <summary>
        /// Gets a list of all elements in the document.
        /// </summary>
        [DomName("all")]
        IHtmlAllCollection All { get; }

        /// <summary>
        /// Gets a list of all of the anchors in the document.
        /// </summary>
        [DomName("anchors")]
        IHtmlCollection<IHtmlAnchorElement> Anchors { get; }

        /// <summary>
        /// Gets the DOM implementation associated with the current document.
        /// </summary>
        [DomName("implementation")]
        IImplementation Implementation { get; }

        /// <summary>
        /// Gets or sets whether the entire document is editable.
        /// </summary>
        [DomName("designMode")]
        String DesignMode { get; set; }

        /// <summary>
        /// Gets or sets the value of the dir attribute.
        /// </summary>
        [DomName("dir")]
        String Direction { get; set; }

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
        /// Gets a value to indicate whether the document is rendered in Quirks
        /// mode (BackComp) or Strict mode (CSS1Compat).
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
        /// Causes the Document to be replaced in-place, as if it was a new
        /// Document object, but reusing the previous object, which is then
        /// returned.
        /// </summary>
        /// <param name="type">The new content type.</param>
        /// <param name="replace">
        /// Special annotation to replace the history.
        /// </param>
        [DomName("open")]
        IDocument Open(String type = "text/html", String replace = null);

        /// <summary>
        /// Opens another window, i.e. works like the window.open method.
        /// </summary>
        /// <param name="url">The URL to open.</param>
        /// <param name="name">The name of the new window.</param>
        /// <param name="features">
        /// The feature string of the new window.
        /// </param>
        /// <param name="replace">
        /// Special annotation to replace the history.
        /// </param>
        [DomName("open")]
        IWindow OpenNew(String url, String name, String features, String replace = null);

        /// <summary>
        /// Finishes writing to a document.
        /// </summary>
        [DomName("close")]
        void Close();

        /// <summary>
        /// Writes text to a document.
        /// </summary>
        /// <param name="content">
        /// The text to be written on the document.
        /// </param>
        [DomName("write")]
        void Write(String content);

        /// <summary>
        /// Writes a line of text to a document.
        /// </summary>
        /// <param name="content">
        /// The text to be written on the document.
        /// </param>
        [DomName("writeln")]
        void WriteLine(String content);

        /// <summary>
        /// Loads the document content from the given url.
        /// </summary>
        /// <param name="url">The url that hosts the content.</param>
        [DomName("load")]
        void Load(String url);

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
        /// Returns a list of elements with a given name in the HTML document.
        /// </summary>
        /// <param name="name">
        /// The value of the name attribute of the element.
        /// </param>
        /// <returns>A collection of HTML elements.</returns>
        [DomName("getElementsByName")]
        IHtmlCollection<IElement> GetElementsByName(String name);

        /// <summary>
        /// Returns a set of elements which have all the given class names.
        /// </summary>
        /// <param name="classNames">
        /// A string representing the list of class names to match; class names
        /// are separated by whitespace.
        /// </param>
        /// <returns>A collection of elements.</returns>
        [DomName("getElementsByClassName")]
        IHtmlCollection<IElement> GetElementsByClassName(String classNames);

        /// <summary>
        /// Returns a NodeList of elements with the given tag name. The
        /// complete document is searched, including the root node.
        /// </summary>
        /// <param name="tagName">
        /// A string representing the name of the elements. The special string
        /// "*" represents all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagName")]
        IHtmlCollection<IElement> GetElementsByTagName(String tagName);

        /// <summary>
        /// Returns a list of elements with the given tag name belonging to the
        /// given namespace. The complete document is searched, including the
        /// root node.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace URI of elements to look for.
        /// </param>
        /// <param name="tagName">
        /// Either the local name of elements to look for or the special value
        /// "*", which matches all elements.
        /// </param>
        /// <returns>
        /// A collection of elements in the order they appear in the tree.
        /// </returns>
        [DomName("getElementsByTagNameNS")]
        IHtmlCollection<IElement> GetElementsByTagName(String namespaceUri, String tagName);

        /// <summary>
        /// Creates an event of the type specified. 
        /// </summary>
        /// <param name="type">
        /// Represents the type of event (e.g., uievent, event, customevent,
        /// ...) to be created.
        /// </param>
        /// <returns>The event.</returns>
        [DomName("createEvent")]
        Event CreateEvent(String type);

        /// <summary>
        /// Creates a new Range object.
        /// </summary>
        /// <returns>The range.</returns>
        [DomName("createRange")]
        IRange CreateRange();

        /// <summary>
        /// Creates a new comment node, and returns it.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be added to the Comment.
        /// </param>
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
        /// <param name="name">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element object.</returns>
        [DomName("createElement")]
        IElement CreateElement(String name);

        /// <summary>
        /// Creates a new element with the given tag name and namespace URI.
        /// </summary>
        /// <param name="namespaceUri">
        /// Specifies the namespace URI to associate with the element.
        /// </param>
        /// <param name="name">
        /// A string that specifies the type of element to be created.
        /// </param>
        /// <returns>The created element.</returns>
        [DomName("createElementNS")]
        IElement CreateElement(String namespaceUri, String name);

        /// <summary>
        /// Creates a ProcessingInstruction node given the specified name and
        /// data strings.
        /// </summary>
        /// <param name="target">
        /// The target part of the processing instruction.
        /// </param>
        /// <param name="data">The data for the node.</param>
        /// <returns>The new processing instruction.</returns>
        [DomName("createProcessingInstruction")]
        IProcessingInstruction CreateProcessingInstruction(String target, String data);

        /// <summary>
        /// Creates a new text node and returns it.
        /// </summary>
        /// <param name="data">
        /// A string containing the data to be put in the text node.
        /// </param>
        /// <returns>The created text node.</returns>
        [DomName("createTextNode")]
        IText CreateTextNode(String data);

        /// <summary>
        /// Creates a new NodeIterator object.
        /// </summary>
        /// <param name="root">
        /// The root node at which to begin the NodeIterator's traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
        /// <returns>The created node NodeIterator.</returns>
        [DomName("createNodeIterator")]
        INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null);

        /// <summary>
        /// Creates a new TreeWalker object.
        /// </summary>
        /// <param name="root">
        /// Is the root Node of this TreeWalker traversal.
        /// </param>
        /// <param name="settings">
        /// Indicates which nodes to iterate over.
        /// </param>
        /// <param name="filter">
        /// An optional callback function for filtering.
        /// </param>
        /// <returns>The created node TreeWalker.</returns>
        [DomName("createTreeWalker")]
        ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null);

        /// <summary>
        /// Creates a copy of a node from an external document that can be
        /// inserted into the current document.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be imported.
        /// </param>
        /// <param name="deep">
        /// Optional argument, indicating whether the descendants of the
        /// imported node need to be imported.
        /// </param>
        /// <returns>
        /// The new node that is imported into the document. The new node's
        /// parentNode is null, since it has not yet been inserted into the
        /// document tree.
        /// </returns>
        [DomName("importNode")]
        INode Import(INode externalNode, Boolean deep = true);

        /// <summary>
        /// Adopts a node from an external document. The node and its subtree
        /// is removed from the document it's in (if any), and its
        /// ownerDocument is changed to the current document. The node can then
        /// be inserted into the current document. The new node's parentNode is
        /// null, since it has not yet been inserted into the document tree.
        /// </summary>
        /// <param name="externalNode">
        /// The node from another document to be adopted.
        /// </param>
        /// <returns>
        /// The adopted node that can be used in the current document.
        /// </returns>
        [DomName("adoptNode")]
        INode Adopt(INode externalNode);

        /// <summary>
        /// Gets the date of the last modification.
        /// </summary>
        [DomName("lastModified")]
        String LastModified { get; }

        /// <summary>
        /// Gets the current ready state of the document.
        /// </summary>
        [DomLenientThis]
        [DomName("readyState")]
        DocumentReadyState ReadyState { get; }

        /// <summary>
        /// Gets the current location of the document.
        /// </summary>
        [DomName("location")]
        [DomPutForwards("href")]
        ILocation Location { get; }

        /// <summary>
        /// Gets the forms in the document.
        /// </summary>
        [DomName("forms")]
        IHtmlCollection<IHtmlFormElement> Forms { get; }

        /// <summary>
        /// Gets the images in the document.
        /// </summary>
        [DomName("images")]
        IHtmlCollection<IHtmlImageElement> Images { get; }

        /// <summary>
        /// Gets the scripts in the document.
        /// </summary>
        [DomName("scripts")]
        IHtmlCollection<IHtmlScriptElement> Scripts { get; }

        /// <summary>
        /// Gets a list of the embed elements within the current document.
        /// </summary>
        [DomName("embeds")]
        [DomName("plugins")]
        IHtmlCollection<IHtmlEmbedElement> Plugins { get; }

        /// <summary>
        /// Gets a list of the commands (menu item, button, and link elements)
        /// within the current document.
        /// </summary>
        [DomName("commands")]
        IHtmlCollection<IElement> Commands { get; }

        /// <summary>
        /// Gets a collection of all area and anchor elements in a document
        /// with a value for the href attribute.
        /// </summary>
        [DomName("links")]
        IHtmlCollection<IElement> Links { get; }

        /// <summary>
        /// Gets or sets the title of the document.
        /// </summary>
        [DomName("title")]
        String Title { get; set; }

        /// <summary>
        /// Gets or sets the head element.
        /// </summary>
        [DomName("head")]
        IHtmlHeadElement Head { get; }

        /// <summary>
        /// Gets the body element.
        /// </summary>
        [DomName("body")]
        IHtmlElement Body { get; set; }

        /// <summary>
        /// Gets or sets the document cookie.
        /// </summary>
        [DomName("cookie")]
        String Cookie { get; set; }

        /// <summary>
        /// Gets the Unicode serialization of document's origin.
        /// </summary>
        [DomName("origin")]
        String Origin { get; }

        /// <summary>
        /// Gets or sets the domain portion of the origin of the current
        /// document.
        /// </summary>
        [DomName("domain")]
        String Domain { get; set; }

        /// <summary>
        /// Gets the referer to that pointed to the current document.
        /// </summary>
        [DomName("referrer")]
        String Referrer { get; }

        /// <summary>
        /// Event triggered after the ready state changed.
        /// </summary>
        [DomName("onreadystatechange")]
        event DomEventHandler ReadyStateChanged;

        /// <summary>
        /// Gets the currently focused element, that is, the element that will
        /// get keystroke events if the user types any.
        /// </summary>
        [DomName("activeElement")]
        IElement ActiveElement { get; }

        /// <summary>
        /// Gets the script element which is currently being processed.
        /// </summary>
        [DomName("currentScript")]
        IHtmlScriptElement CurrentScript { get; }

        /// <summary>
        /// Gets the window object associated with the document or null if none
        /// available.
        /// </summary>
        [DomName("defaultView")]
        IWindow DefaultView { get; }

        /// <summary>
        /// Checks if the document is currently focused.
        /// </summary>
        /// <returns>True if the document is active and in the focus.</returns>
        [DomName("hasFocus")]
        Boolean HasFocus();

        /// <summary>
        /// Executes a command with the provided id and the optional arguments.
        /// </summary>
        /// <param name="commandId">The id of the command to issue.</param>
        /// <param name="showUserInterface">Shall the UI be shown?</param>
        /// <param name="value">
        /// The argument value of the command, if any.
        /// </param>
        /// <returns>
        /// True if the command has been successfully executed, otherwise
        /// false.
        /// </returns>
        [DomName("execCommand")]
        Boolean ExecuteCommand(String commandId, Boolean showUserInterface = false, String value = "");

        /// <summary>
        /// Checks if the command with the provided id is enabled.
        /// </summary>
        /// <param name="commandId">The id of the command to check.</param>
        /// <returns>
        /// True if the command exists and is enabled, otherwise false.
        /// </returns>
        [DomName("queryCommandEnabled")]
        Boolean IsCommandEnabled(String commandId);

        /// <summary>
        /// Checks if the command with the provided id is currently in an
        /// indeterminate state.
        /// </summary>
        /// <param name="commandId">The id of the command to check.</param>
        /// <returns>
        /// True if the command exists and is neither enabled nor disabled,
        /// otherwise false.
        /// </returns>
        [DomName("queryCommandIndeterm")]
        Boolean IsCommandIndeterminate(String commandId);

        /// <summary>
        /// Checks if the command with the provided id has already been
        /// executed for the current value.
        /// </summary>
        /// <param name="commandId">The id of the command to check.</param>
        /// <returns>
        /// True if the command has been executed, otherwise false.
        /// </returns>
        [DomName("queryCommandState")]
        Boolean IsCommandExecuted(String commandId);

        /// <summary>
        /// Checks if a command with the provided id exists and is supported
        /// in the current context.
        /// </summary>
        /// <param name="commandId">The id of the command to check.</param>
        /// <returns>True if the command exists, otherwise false.</returns>
        [DomName("queryCommandSupported")]
        Boolean IsCommandSupported(String commandId);

        /// <summary>
        /// Gets the value of the document, range, or current selection, for
        /// the provided command.
        /// </summary>
        /// <param name="commandId">The id of the command to issue.</param>
        /// <returns>The modified value.</returns>
        [DomName("queryCommandValue")]
        String GetCommandValue(String commandId);

        /// <summary>
        /// Gets the browsing context to use.
        /// </summary>
        IBrowsingContext Context { get; }
    }
}

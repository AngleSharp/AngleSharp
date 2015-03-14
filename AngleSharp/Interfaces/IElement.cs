namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The Element interface represents an object within a DOM document. 
    /// </summary>
    [DomName("Element")]
    public interface IElement : INode, IParentNode, IChildNode, INonDocumentTypeChildNode
    {        
        /// <summary>
        /// Gets the namespace prefix of this element.
        /// </summary>
        [DomName("prefix")]
        String Prefix { get; }

        /// <summary>
        /// Gets the local part of the qualified name of this element.
        /// </summary>
        [DomName("localName")]
        String LocalName { get; }

        /// <summary>
        /// Gets the namespace URI of this element.
        /// </summary>
        [DomName("namespaceURI")]
        String NamespaceUri { get; }

        /// <summary>
        /// Gets the sequence of associated attributes.
        /// </summary>
        [DomName("attributes")]
        IEnumerable<IAttr> Attributes { get; }

        /// <summary>
        /// Gets the list of class names.
        /// </summary>
        [DomName("classList")]
        ITokenList ClassList { get; }

        /// <summary>
        /// Gets or sets the value of the class attribute.
        /// </summary>
        [DomName("className")]
        String ClassName { get; set; }

        /// <summary>
        /// Gets or sets the id value of the element.
        /// </summary>
        [DomName("id")]
        String Id { get; set; }

        /// <summary>
        /// Inserts new HTML elements specified by the given HTML string at
        /// a position relative to the current element specified by the
        /// position.
        /// </summary>
        /// <param name="position">The relation to the current element.</param>
        /// <param name="html">The HTML code to generate elements for.</param>
        [DomName("insertAdjacentHTML")]
        void Insert(AdjacentPosition position, String html);

        /// <summary>
        /// Returns a boolean value indicating whether the specified element
        /// has the specified attribute or not.
        /// </summary>
        /// <param name="name">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DomName("hasAttribute")]
        Boolean HasAttribute(String name);

        /// <summary>
        /// Returns a boolean value indicating whether the specified element
        /// has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DomName("hasAttributeNS")]
        Boolean HasAttribute(String namespaceUri, String localName);

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="name">
        /// The name of the attribute whose value you want to get.
        /// </param>
        /// <returns>
        /// If the named attribute does not exist, the value returned will be
        /// null, otherwise the attribute's value.
        /// </returns>
        [DomName("getAttribute")]
        String GetAttribute(String name);

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">
        /// The name of the attribute whose value you want to get.
        /// </param>
        /// <returns>
        /// If the named attribute does not exist, the value returned will be
        /// null, otherwise the attribute's value.
        /// </returns>
        [DomName("getAttributeNS")]
        String GetAttribute(String namespaceUri, String localName);

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute
        /// on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        /// <returns>The current element.</returns>
        [DomName("setAttribute")]
        void SetAttribute(String name, String value);

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute
        /// on the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        [DomName("setAttributeNS")]
        void SetAttribute(String namespaceUri, String name, String value);

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="name">
        /// Is a string that names the attribute to be removed.
        /// </param>
        /// <returns>The current element.</returns>
        [DomName("removeAttribute")]
        void RemoveAttribute(String name);

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceUri">
        /// A string specifying the namespace of the attribute.
        /// </param>
        /// <param name="localName">
        /// Is a string that names the attribute to be removed.
        /// </param>
        /// <returns>The current element.</returns>
        [DomName("removeAttributeNS")]
        void RemoveAttribute(String namespaceUri, String localName);

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
        IHtmlCollection<IElement> GetElementsByTagNameNS(String namespaceUri, String tagName);

        /// <summary>
        /// Checks if the element is matched by the given selector.
        /// </summary>
        /// <param name="selectors">Represents the selector to test.</param>
        /// <returns>
        /// True if the element would be selected by the specified selector,
        /// otherwise false.
        /// </returns>
        [DomName("matches")]
        Boolean Matches(String selectors);

        /// <summary>
        /// Gets or sets the inner HTML (excluding the current element) of the
        /// element.
        /// </summary>
        [DomName("innerHTML")]
        String InnerHtml { get; set; }

        /// <summary>
        /// Gets or sets the outer HTML (including the current element) of the
        /// element.
        /// </summary>
        [DomName("outerHTML")]
        String OuterHtml { get; set; }

        /// <summary>
        /// Gets the name of the tag that represents the current element.
        /// </summary>
        [DomName("tagName")]
        String TagName { get; }

        /// <summary>
        /// Creates a pseudo element for the current element.
        /// </summary>
        /// <param name="pseudoElement">
        /// The element to create (e.g. ::after).
        /// </param>
        /// <returns>The created element or null, if not possible.</returns>
        [DomName("pseudo")]
        IPseudoElement Pseudo(String pseudoElement);

        /// <summary>
        /// Gets if the element is currently focused.
        /// </summary>
        Boolean IsFocused { get; }
    }
}

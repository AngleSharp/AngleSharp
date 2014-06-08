namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Collections.Generic;

    [DOM("Element")]
    interface IElement : INode, IQueryElements
    {
        Int32 ChildElementCount { get; }

        /// <summary>
        /// Gets the sequence of associated attributes.
        /// </summary>
        [DOM("attributes")]
        AttrContainer Attributes { get; }

        HTMLCollection Children { get; }

        ITokenList ClassList { get; }

        String ClassName { get; set; }

        ContentEditableMode ContentEditable { get; set; }

        IStringMap Dataset { get; }

        DirectionMode Dir { get; set; }

        Element FirstElementChild { get; }

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="name">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttribute")]
        Boolean HasAttribute(String name);

        /// <summary>
        /// Returns a boolean value indicating whether the specified element has the specified attribute or not.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The attributes name.</param>
        /// <returns>The return value of true or false.</returns>
        [DOM("hasAttributeNS")]
        Boolean HasAttribute(String namespaceUri, String localName);

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttribute")]
        String GetAttribute(String name);

        /// <summary>
        /// Returns the value of the named attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localName">The name of the attribute whose value you want to get.</param>
        /// <returns>If the named attribute does not exist, the value returned will be null, otherwise the attribute's value.</returns>
        [DOM("getAttributeNS")]
        String GetAttribute(String namespaceUri, String localName);

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        /// <returns>The current element.</returns>
        [DOM("setAttribute")]
        void SetAttribute(String name, String value);

        /// <summary>
        /// Adds a new attribute or changes the value of an existing attribute on the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="name">The name of the attribute as a string.</param>
        /// <param name="value">The desired new value of the attribute.</param>
        [DOM("setAttributeNS")]
        void SetAttribute(String namespaceUri, String name, String value);

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="name">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttribute")]
        void RemoveAttribute(String name);

        /// <summary>
        /// Removes an attribute from the specified element.
        /// </summary>
        /// <param name="namespaceUri">A string specifying the namespace of the attribute.</param>
        /// <param name="localAttrName">Is a string that names the attribute to be removed.</param>
        /// <returns>The current element.</returns>
        [DOM("removeAttributeNS")]
        void RemoveAttribute(String namespaceUri, String localName);

        String Id { get; set; }

        String InnerHTML { get; set; }

        Boolean IsContentEditable { get; }

        String Lang { get; set; }

        Element LastElementChild { get; }

        Element NextElementSibling { get; }

        String OuterHTML { get; set; }

        Element PreviousElementSibling { get; }

        Boolean Spellcheck { get; set; }

        CSSStyleDeclaration Style { get; }

        Int32 TabIndex { get; set; }

        String TagName { get; }

        String Title { get; set; }
    }
}

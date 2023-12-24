namespace AngleSharp.Html.Construction;

using System;
using AngleSharp.Dom;
using Common;
using Parser.Tokens.Struct;

/// <summary>
/// Represents a constructable DOM element.
/// </summary>
public interface IConstructableElement : IConstructableNode
{
    /// <summary>
    /// Gets the namespace URI of this element.
    /// </summary>
    StringOrMemory NamespaceUri { get; }

    /// <summary>
    /// Gets the local part of the qualified name of this element.
    /// </summary>
    StringOrMemory LocalName { get; }

    /// <summary>
    /// Gets the namespace prefix of this element.
    /// </summary>
    StringOrMemory Prefix { get; }

    /// <summary>
    /// Gets the sequence of associated attributes.
    /// </summary>
    IConstructableNamedNodeMap Attributes { get; }

    /// <summary>
    /// Gets or sets the source reference of this element.
    /// </summary>
    ISourceReference? SourceReference { get; set; }

    /// <summary>
    /// Adds a new attribute or changes the value of an existing attribute
    /// on the specified element.
    /// </summary>
    /// <param name="namespaceUri">
    /// A string specifying the namespace of the attribute.
    /// </param>
    /// <param name="name">The name of the attribute as a string.</param>
    /// <param name="value">The desired new value of the attribute.</param>
    void SetAttribute(String? namespaceUri, StringOrMemory name, StringOrMemory value);

    /// <summary>
    /// Faster way of setting the (known) attribute.
    /// </summary>
    /// <param name="name">The name of the attribute.</param>
    /// <param name="value">The attribute's value.</param>
    void SetOwnAttribute(StringOrMemory name, StringOrMemory value);

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
    StringOrMemory GetAttribute(StringOrMemory namespaceUri, StringOrMemory localName);

    /// <summary>
    /// Sets the node attributes from the struct representation. Usually from html token representation.
    /// </summary>
    /// <param name="tagAttributes"></param>
    void SetAttributes(StructAttributes tagAttributes);

    /// <summary>
    /// Returns a boolean value indicating whether the specified element
    /// has the specified attribute or not.
    /// </summary>
    /// <param name="name">The attributes name.</param>
    /// <returns>The return value of true or false.</returns>
    Boolean HasAttribute(StringOrMemory name);

    /// <summary>
    /// Performs an element setup. Usually called after the element has been created and added to the parent.
    /// </summary>
    void SetupElement();

    /// <summary>
    /// Ads a new dom representation of a comment and adds it to the document.
    /// </summary>
    /// <param name="token">The token to use.</param>
    void AddComment(ref StructHtmlToken token);

    /// <summary>
    /// Creates a shallow copy of the element. Usually used for fixing issues with the DOM during parsing.
    /// </summary>
    IConstructableNode ShallowCopy();
}
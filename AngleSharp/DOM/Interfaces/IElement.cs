using System;
using AngleSharp.DOM.Collections;

namespace AngleSharp.DOM
{
    interface IElement : INode, IQueryElements
    {
        int ChildElementCount { get; }
        HTMLCollection Children { get; }
        DOMTokenList ClassList { get; }
        string ClassName { get; set; }
        ContentEditableMode ContentEditable { get; set; }
        DOMStringMap Dataset { get; }
        DirectionMode Dir { get; set; }
        Element FirstElementChild { get; }
        string GetAttribute(string attrName);
        Attr GetAttributeNode(string attrName);
        Attr GetAttributeNodeNS(string namespaceURI, string attrName);
        string GetAttributeNS(string namespaceURI, string localAttrName);
        bool HasAttribute(string attrName);
        bool HasAttributeNS(string namespaceURI, string attrName);
        string Id { get; set; }
        string InnerHTML { get; set; }
        bool IsContentEditable { get; }
        string Lang { get; set; }
        Element LastElementChild { get; }
        Element NextElementSibling { get; }
        string OuterHTML { get; set; }
        Element PreviousElementSibling { get; }
        Element RemoveAttribute(string attrName);
        Attr RemoveAttributeNode(Attr attr);
        Element RemoveAttributeNS(string namespaceURI, string localAttrName);
        Element SetAttribute(string name, string value);
        Attr SetAttributeNode(Attr attr);
        Attr SetAttributeNodeNS(string namespaceURI, Attr attr);
        Element SetAttributeNS(string namespaceURI, string name, string value);
        bool Spellcheck { get; set; }
        CSSStyleDeclaration Style { get; }
        int TabIndex { get; set; }
        string TagName { get; }
        string Title { get; set; }
    }
}

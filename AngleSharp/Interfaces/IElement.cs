namespace AngleSharp.DOM
{
    using System;
    using AngleSharp.DOM.Collections;

    interface IElement : INode, IQueryElements
    {
        Int32 ChildElementCount { get; }
        HTMLCollection Children { get; }
        ITokenList ClassList { get; }
        String ClassName { get; set; }
        ContentEditableMode ContentEditable { get; set; }
        IStringMap Dataset { get; }
        DirectionMode Dir { get; set; }
        Element FirstElementChild { get; }
        String GetAttribute(String attrName);
        Attr GetAttributeNode(String attrName);
        Attr GetAttributeNodeNS(String namespaceURI, String attrName);
        String GetAttributeNS(String namespaceURI, String localAttrName);
        Boolean HasAttribute(String attrName);
        Boolean HasAttributeNS(String namespaceURI, String attrName);
        String Id { get; set; }
        String InnerHTML { get; set; }
        Boolean IsContentEditable { get; }
        String Lang { get; set; }
        Element LastElementChild { get; }
        Element NextElementSibling { get; }
        String OuterHTML { get; set; }
        Element PreviousElementSibling { get; }
        Element RemoveAttribute(String attrName);
        Attr RemoveAttributeNode(Attr attr);
        Element RemoveAttributeNS(String namespaceURI, String localAttrName);
        Element SetAttribute(String name, String value);
        Attr SetAttributeNode(Attr attr);
        Attr SetAttributeNodeNS(String namespaceURI, Attr attr);
        Element SetAttributeNS(String namespaceURI, String name, String value);
        Boolean Spellcheck { get; set; }
        CSSStyleDeclaration Style { get; }
        Int32 TabIndex { get; set; }
        String TagName { get; }
        String Title { get; set; }
    }
}

namespace AngleSharp.DOM
{
    using System;

    interface IAttr
    {
        [DOM("localName")]
        String LocalName { get; }

        [DOM("name")]
        String Name { get; }

        [DOM("value")]
        String Value { get; set; }

        [DOM("namespaceURI")]
        String NamespaceUri { get; }

        [DOM("prefix")]
        String Prefix { get; }
    }
}

namespace AngleSharp.Dom
{
    using System;

    sealed class AnyElement : Element
    {
        public AnyElement(Document owner, String localName, String prefix, String namespaceUri, NodeFlags flags = NodeFlags.None)
            : base(owner, localName, prefix, namespaceUri, flags)
        {
        }
    }
}

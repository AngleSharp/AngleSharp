namespace AngleSharp.DOM
{
    using System;

    [DOM("DocumentType")]
    interface IDocumentType : INode
    {
        [DOM("name")]
        String Name { get; }
        
        [DOM("publicId")]
        String PublicIdentifier { get; }

        [DOM("systemId")]
        String SystemIdentifier { get; }
    }
}

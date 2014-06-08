namespace AngleSharp.DOM
{
    using System;

    [DomName("DocumentType")]
    interface IDocumentType : INode
    {
        [DomName("name")]
        String Name { get; }
        
        [DomName("publicId")]
        String PublicIdentifier { get; }

        [DomName("systemId")]
        String SystemIdentifier { get; }
    }
}

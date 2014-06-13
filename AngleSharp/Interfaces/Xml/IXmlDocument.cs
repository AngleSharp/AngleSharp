namespace AngleSharp.DOM.Xml
{
    /// <summary>
    /// The XMLDocument interface represent an XML document. It inherits from
    /// the generic Document and do not add any specific methods or properties
    /// to it: nevertheless, several algorithms behave differently with the two
    /// types of documents.
    /// </summary>
    [DomName("XMLDocument")]
    public interface IXmlDocument : IDocument
    {
    }
}

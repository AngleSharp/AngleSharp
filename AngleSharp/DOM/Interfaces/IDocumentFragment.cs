using System;

namespace AngleSharp.DOM
{
    interface IDocumentFragment : INode, IQueryElements
    {
        DocumentFragment Append(params Node[] nodes);
        DocumentFragment Prepend(params Node[] nodes);
    }
}

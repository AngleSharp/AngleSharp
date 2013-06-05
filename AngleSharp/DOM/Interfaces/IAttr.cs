using System;

namespace AngleSharp.DOM
{
    interface IAttr : INode
    {
        bool IsId { get; }
        string Name { get; }
        bool Specified { get; }
        string Value { get; set; }
    }
}

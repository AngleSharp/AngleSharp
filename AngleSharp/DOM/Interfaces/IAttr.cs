namespace AngleSharp.DOM
{
    using System;

    interface IAttr : INode
    {
        Boolean IsId { get; }
        String Name { get; }
        Boolean Specified { get; }
        String Value { get; set; }
    }
}

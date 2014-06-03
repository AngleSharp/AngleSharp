namespace AngleSharp.DOM
{
    using System;

    [DOM("NodeList")]
    interface INodeList
    {
        [DOM("item")]
        INode this[Int32 index] { get; }

        [DOM("length")]
        Int32 Length { get; }
    }
}

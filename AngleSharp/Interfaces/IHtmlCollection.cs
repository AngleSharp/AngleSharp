namespace AngleSharp.DOM
{
    using System;

    [DOM("HTMLCollection")]
    interface IHtmlCollection
    {
        [DOM("length")]
        Int32 Length { get; }
  
        [DOM("item")]
        IElement this[Int32 index] { get; }
  
        [DOM("namedItem")]
        IElement this[String name] { get; }
    }
}

using System;
namespace AngleSharp.DOM
{
    [DOM("ProcessingInstruction")]
    interface IProcessingInstruction : ICharacterData
    {
        [DOM("target")]
        String Target { get; }
    }
}

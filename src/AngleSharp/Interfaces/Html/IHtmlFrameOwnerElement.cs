namespace AngleSharp.Dom.Html
{
    using System;

    /// <summary>
    /// Represents the base class for frame owned elements.
    /// </summary>
    public interface IHtmlFrameOwnerElement : IHtmlElement
    {

        Boolean CanContainRangeEndpoint { get; }

        Int32 DisplayWidth { get; set; }

        Int32 DisplayHeight { get; set; }

        Int32 MarginWidth { get; set; }

        Int32 MarginHeight { get; set; }

    }
}

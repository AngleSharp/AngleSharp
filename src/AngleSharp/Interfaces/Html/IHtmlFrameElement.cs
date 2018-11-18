namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the HTML frame element.
    /// </summary>
    [DomHistorical]
    public interface IHtmlFrameElement : IHtmlFrameElementBase
    {

        Boolean NoResize { get; set; }

    }
}

namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Attributes;

    /// <summary>
    /// Represents the HTML frameset element.
    /// Obsolete since HTML 4.01.
    /// </summary>
    [DomHistorical]
    public interface IHtmlFrameSetElement : IHtmlElement
    {

        Int32 Columns { get; set; }

        Int32 Rows { get; set; }

    }
}

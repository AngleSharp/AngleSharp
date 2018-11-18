namespace AngleSharp.Dom.Html
{
    using System;
    using AngleSharp.Network;

    /// <summary>
    /// Represents the base class for frame elements.
    /// </summary>
    public interface IHtmlFrameElementBase : IHtmlFrameOwnerElement
    {

        IDownload CurrentDownload { get; }

        String Name { get; set; }

        String Source { get; set; }

        String Scrolling { get; set; }

        IDocument ContentDocument { get; }

        String LongDesc { get; set; }

        String FrameBorder { get; set; }

        IBrowsingContext NestedContext { get; }

    }
}

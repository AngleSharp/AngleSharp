namespace AngleSharp.DOM.Html
{
    using AngleSharp.DOM.Collections;
    using System;
    using System.Net;

    /// <summary>
    /// The HTMLDocument interface represent an HTML document.
    /// </summary>
    [DomName("HTMLDocument")]
    public interface IHtmlDocument : IDocument
    {
        IHtmlCollection Anchors { get; }
        IHtmlBodyElement Body { get; }
        Cookie Cookie { get; set; }
        String Domain { get; }
        IHtmlCollection Embeds { get; }
        IHtmlCollection Forms { get; }
        IHtmlHeadElement Head { get; }
        IHtmlCollection Images { get; }
        IHtmlCollection Links { get; }
        void Load(String url);
        IHtmlCollection Scripts { get; }
        String Title { get; set; }
        void Open();
        void Close();
        void Write(String content);
        void WriteLn(String content);
    }
}

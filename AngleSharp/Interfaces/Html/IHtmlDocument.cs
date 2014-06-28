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
        HTMLCollection<IHtmlAnchorElement> Anchors { get; }
        IHtmlBodyElement Body { get; }
        Cookie Cookie { get; set; }
        String Domain { get; }
        HTMLCollection Embeds { get; }
        HTMLCollection<HTMLFormElement> Forms { get; }
        IHtmlHeadElement Head { get; }
        HTMLCollection<IHtmlImageElement> Images { get; }
        HTMLCollection Links { get; }
        void Load(String url);
        HTMLCollection<HTMLScriptElement> Scripts { get; }
        String Title { get; set; }
        void Open();
        void Close();
        void Write(String content);
        void WriteLn(String content);
    }
}

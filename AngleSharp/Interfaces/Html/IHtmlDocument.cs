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
        void Load(String url);
        void Open();
        void Close();
        void Write(String content);
        void WriteLn(String content);
    }
}

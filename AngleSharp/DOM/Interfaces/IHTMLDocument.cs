namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;
    using System.Net;

    interface IHTMLDocument : IDocument
    {
        HTMLCollection<HTMLAnchorElement> Anchors { get; }
        HTMLBodyElement Body { get; }
        HTMLDocument Close();
        Cookie Cookie { get; set; }
        String Domain { get; }
        HTMLCollection Embeds { get; }
        HTMLCollection<HTMLFormElement> Forms { get; }
        HTMLCollection GetElementsByName(String name);
        HTMLHeadElement Head { get; }
        HTMLCollection<HTMLImageElement> Images { get; }
        HTMLCollection Links { get; }
        HTMLDocument Open();
        void Load(String url);
        HTMLCollection<HTMLScriptElement> Scripts { get; }
        String Title { get; set; }
        String Url { get; }
        HTMLDocument Write(String content);
        HTMLDocument WriteLn(String content);
    }
}

namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using AngleSharp.DOM.Html;
    using System;

    interface IHTMLDocument : IDocument
    {
        HTMLCollection Anchors { get; }
        HTMLBodyElement Body { get; }
        HTMLDocument Close();
        Cookie Cookie { get; set; }
        String Domain { get; }
        HTMLCollection Embeds { get; }
        HTMLCollection Forms { get; }
        HTMLCollection GetElementsByName(String name);
        HTMLHeadElement Head { get; }
        HTMLCollection Images { get; }
        HTMLCollection Links { get; }
        HTMLDocument Open();
        HTMLDocument Load(String url);
        HTMLCollection Scripts { get; }
        String Title { get; set; }
        String URL { get; }
        HTMLDocument Write(String content);
        HTMLDocument WriteLn(String content);
    }
}

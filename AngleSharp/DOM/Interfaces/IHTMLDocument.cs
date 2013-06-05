using AngleSharp.DOM.Collections;
using AngleSharp.DOM.Html;
using System;

namespace AngleSharp.DOM
{
    interface IHTMLDocument : IDocument
    {
        HTMLCollection Anchors { get; }
        HTMLBodyElement Body { get; }
        HTMLDocument Close();
        Cookie Cookie { get; set; }
        string Domain { get; }
        HTMLCollection Embeds { get; }
        HTMLCollection Forms { get; }
        HTMLCollection GetElementsByName(string name);
        HTMLHeadElement Head { get; }
        HTMLCollection Images { get; }
        HTMLCollection Links { get; }
        HTMLDocument Open();
        HTMLDocument Load(string url);
        HTMLCollection Scripts { get; }
        string Title { get; set; }
        string URL { get; }
        HTMLDocument Write(string content);
        HTMLDocument WriteLn(string content);
    }
}

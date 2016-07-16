namespace AngleSharp.Services
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html.LinkRels;
    using System;

    interface ILinkRelationFactory
    {
        BaseLinkRelation Create(HtmlLinkElement link, String rel);
    }
}

namespace AngleSharp.Services
{
    using AngleSharp.Dom.Html;
    using AngleSharp.Html.LinkRels;
    using System;

    interface ILinkRelationFactory : IService
    {
        BaseLinkRelation Create(HtmlLinkElement link, String rel);
    }
}

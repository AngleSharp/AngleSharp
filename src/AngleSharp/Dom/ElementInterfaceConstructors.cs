namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Svg.Dom;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The element each public element interface creates in
    /// <see cref="DocumentExtensions.CreateElement{TElement}(IDocument)"/>.
    /// </summary>
    /// <remarks>
    /// A fixed table instead of scanning the assembly for a type implementing the interface, so
    /// the method stays intact under trimming and Native AOT. The entries reproduce what that scan
    /// picked: the first implementing type in metadata order. The general interfaces
    /// (<see cref="IElement"/>, <see cref="IHtmlElement"/>, <see cref="ISvgElement"/>) are absent,
    /// since no single element stands behind them - the scan returned an arbitrary one, such as an
    /// SVG circle for <see cref="IElement"/>. <see cref="IHtmlUnknownElement"/> is absent because
    /// its constructor needs a local name, which the scan could not supply either.
    /// </remarks>
    internal static class ElementInterfaceConstructors
    {
        private static readonly Dictionary<Type, Func<Document, Element>> Constructors = new()
        {
            [typeof(IHtmlAnchorElement)] = owner => new HtmlAnchorElement(owner),
            [typeof(IHtmlAreaElement)] = owner => new HtmlAreaElement(owner),
            [typeof(IHtmlAudioElement)] = owner => new HtmlAudioElement(owner),
            [typeof(IHtmlBaseElement)] = owner => new HtmlBaseElement(owner),
            [typeof(IHtmlBodyElement)] = owner => new HtmlBodyElement(owner),
            [typeof(IHtmlBreakRowElement)] = owner => new HtmlBreakRowElement(owner),
            [typeof(IHtmlButtonElement)] = owner => new HtmlButtonElement(owner),
            [typeof(IHtmlCanvasElement)] = owner => new HtmlCanvasElement(owner),
            [typeof(IHtmlDataElement)] = owner => new HtmlDataElement(owner),
            [typeof(IHtmlDataListElement)] = owner => new HtmlDataListElement(owner),
            [typeof(IHtmlDetailsElement)] = owner => new HtmlDetailsElement(owner),
            [typeof(IHtmlDialogElement)] = owner => new HtmlDialogElement(owner),
            [typeof(IHtmlDivElement)] = owner => new HtmlDivElement(owner),
            [typeof(IHtmlEmbedElement)] = owner => new HtmlEmbedElement(owner),
            [typeof(IHtmlFieldSetElement)] = owner => new HtmlFieldSetElement(owner),
            [typeof(IHtmlFormElement)] = owner => new HtmlFormElement(owner),
            [typeof(IHtmlHeadElement)] = owner => new HtmlHeadElement(owner),
            [typeof(IHtmlHeadingElement)] = owner => new HtmlHeadingElement(owner),
            [typeof(IHtmlHrElement)] = owner => new HtmlHrElement(owner),
            [typeof(IHtmlHtmlElement)] = owner => new HtmlHtmlElement(owner),
            [typeof(IHtmlImageElement)] = owner => new HtmlImageElement(owner),
            [typeof(IHtmlInlineFrameElement)] = owner => new HtmlIFrameElement(owner),
            [typeof(IHtmlInputElement)] = owner => new HtmlInputElement(owner),
            [typeof(IHtmlKeygenElement)] = owner => new HtmlKeygenElement(owner),
            [typeof(IHtmlLabelElement)] = owner => new HtmlLabelElement(owner),
            [typeof(IHtmlLegendElement)] = owner => new HtmlLegendElement(owner),
            [typeof(IHtmlLinkElement)] = owner => new HtmlLinkElement(owner),
            [typeof(IHtmlListItemElement)] = owner => new HtmlListItemElement(owner),
            [typeof(IHtmlMapElement)] = owner => new HtmlMapElement(owner),
            [typeof(IHtmlMarqueeElement)] = owner => new HtmlMarqueeElement(owner),
            [typeof(IHtmlMediaElement)] = owner => new HtmlAudioElement(owner),
            [typeof(IHtmlMenuElement)] = owner => new HtmlMenuElement(owner),
            [typeof(IHtmlMenuItemElement)] = owner => new HtmlMenuItemElement(owner),
            [typeof(IHtmlMetaElement)] = owner => new HtmlMetaElement(owner),
            [typeof(IHtmlMeterElement)] = owner => new HtmlMeterElement(owner),
            [typeof(IHtmlModElement)] = owner => new HtmlModElement(owner),
            [typeof(IHtmlObjectElement)] = owner => new HtmlObjectElement(owner),
            [typeof(IHtmlOptionElement)] = owner => new HtmlOptionElement(owner),
            [typeof(IHtmlOptionsGroupElement)] = owner => new HtmlOptionsGroupElement(owner),
            [typeof(IHtmlOrderedListElement)] = owner => new HtmlOrderedListElement(owner),
            [typeof(IHtmlOutputElement)] = owner => new HtmlOutputElement(owner),
            [typeof(IHtmlParagraphElement)] = owner => new HtmlParagraphElement(owner),
            [typeof(IHtmlParamElement)] = owner => new HtmlParamElement(owner),
            [typeof(IHtmlPictureElement)] = owner => new HtmlPictureElement(owner),
            [typeof(IHtmlPreElement)] = owner => new HtmlPreElement(owner),
            [typeof(IHtmlProgressElement)] = owner => new HtmlProgressElement(owner),
            [typeof(IHtmlQuoteElement)] = owner => new HtmlQuoteElement(owner),
            [typeof(IHtmlScriptElement)] = owner => new HtmlScriptElement(owner),
            [typeof(IHtmlSelectElement)] = owner => new HtmlSelectElement(owner),
            [typeof(IHtmlSlotElement)] = owner => new HtmlSlotElement(owner),
            [typeof(IHtmlSourceElement)] = owner => new HtmlSourceElement(owner),
            [typeof(IHtmlSpanElement)] = owner => new HtmlSpanElement(owner),
            [typeof(IHtmlStyleElement)] = owner => new HtmlStyleElement(owner),
            [typeof(IHtmlTableCaptionElement)] = owner => new HtmlTableCaptionElement(owner),
            [typeof(IHtmlTableCellElement)] = owner => new HtmlTableDataCellElement(owner),
            [typeof(IHtmlTableColumnElement)] = owner => new HtmlTableColElement(owner),
            [typeof(IHtmlTableDataCellElement)] = owner => new HtmlTableDataCellElement(owner),
            [typeof(IHtmlTableElement)] = owner => new HtmlTableElement(owner),
            [typeof(IHtmlTableHeaderCellElement)] = owner => new HtmlTableHeaderCellElement(owner),
            [typeof(IHtmlTableRowElement)] = owner => new HtmlTableRowElement(owner),
            [typeof(IHtmlTableSectionElement)] = owner => new HtmlTableSectionElement(owner),
            [typeof(IHtmlTemplateElement)] = owner => new HtmlTemplateElement(owner),
            [typeof(IHtmlTextAreaElement)] = owner => new HtmlTextAreaElement(owner),
            [typeof(IHtmlTimeElement)] = owner => new HtmlTimeElement(owner),
            [typeof(IHtmlTitleElement)] = owner => new HtmlTitleElement(owner),
            [typeof(IHtmlTrackElement)] = owner => new HtmlTrackElement(owner),
            [typeof(IHtmlUnorderedListElement)] = owner => new HtmlUnorderedListElement(owner),
            [typeof(IHtmlVideoElement)] = owner => new HtmlVideoElement(owner),
            [typeof(ISvgCircleElement)] = owner => new SvgCircleElement(owner),
            [typeof(ISvgDescriptionElement)] = owner => new SvgDescElement(owner),
            [typeof(ISvgForeignObjectElement)] = owner => new SvgForeignObjectElement(owner),
            [typeof(ISvgStyleElement)] = owner => new SvgStyleElement(owner),
            [typeof(ISvgSvgElement)] = owner => new SvgSvgElement(owner),
            [typeof(ISvgTitleElement)] = owner => new SvgTitleElement(owner),
        };

        public static Element? Create(Type elementInterface, Document owner) =>
            Constructors.TryGetValue(elementInterface, out var create) ? create(owner) : null;
    }
}

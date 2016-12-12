namespace AngleSharp.Dom
{
    using System;
    using AngleSharp.Dom.Html;
    using AngleSharp.Extensions;
    using AngleSharp.Html;

    sealed class DefaultAttributeObserver : IAttributeObserver
    {
        private readonly Action<IElement, String, String>[] _actions;

        public DefaultAttributeObserver()
        {
            _actions = new[]
            {
                RegisterCallback<HtmlElement>(AttributeNames.DropZone, (element, value) => element.UpdateDropZone(value)),
                RegisterCallback<HtmlBaseElement>(AttributeNames.Href, (element, value) => element.UpdateUrl(value)),
                RegisterCallback<HtmlEmbedElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value)),
                RegisterCallback<HtmlLinkElement>(AttributeNames.Sizes, (element, value) => element.UpdateSizes(value)),
                RegisterCallback<HtmlLinkElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value)),
                RegisterCallback<HtmlLinkElement>(AttributeNames.Disabled, (element, value) => element.UpdateDisabled(value)),
                RegisterCallback<HtmlLinkElement>(AttributeNames.Href, (element, value) => element.UpdateSource(value)),
                RegisterCallback<HtmlLinkElement>(AttributeNames.Rel, (element, value) => element.UpdateRelation(value)),
                RegisterCallback<HtmlUrlBaseElement>(AttributeNames.Rel, (element, value) => element.UpdateRel(value)),
                RegisterCallback<HtmlUrlBaseElement>(AttributeNames.Ping, (element, value) => element.UpdatePing(value)),
                RegisterCallback<HtmlTableCellElement>(AttributeNames.Headers, (element, value) => element.UpdateHeaders(value)),
                RegisterCallback<HtmlStyleElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value)),
                RegisterCallback<HtmlSelectElement>(AttributeNames.Value, (element, value) => element.UpdateValue(value)),
                RegisterCallback<HtmlOutputElement>(AttributeNames.For, (element, value) => element.UpdateFor(value)),
                RegisterCallback<HtmlObjectElement>(AttributeNames.Data, (element, value) => element.UpdateSource(value)),
                RegisterCallback<HtmlAudioElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value)),
                RegisterCallback<HtmlVideoElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value)),
                RegisterCallback<HtmlImageElement>(AttributeNames.Src, (element, value) => element.UpdateSource()),
                RegisterCallback<HtmlImageElement>(AttributeNames.SrcSet, (element, value) => element.UpdateSource()),
                RegisterCallback<HtmlImageElement>(AttributeNames.Sizes, (element, value) => element.UpdateSource()),
                RegisterCallback<HtmlImageElement>(AttributeNames.CrossOrigin, (element, value) => element.UpdateSource()),
                RegisterCallback<HtmlIFrameElement>(AttributeNames.Sandbox, (element, value) => element.UpdateSandbox(value)),
                RegisterCallback<HtmlIFrameElement>(AttributeNames.SrcDoc, (element, value) => element.UpdateSource()),
                RegisterCallback<Element>(AttributeNames.Class, (element, value) => element.UpdateClassList(value)),
                RegisterCallback<HtmlFrameElementBase>(AttributeNames.Src, (element, value) => element.UpdateSource()),
                RegisterCallback<HtmlInputElement>(AttributeNames.Type, (element, value) => element.UpdateType(value))
            };
        }

        private Action<IElement, String, String> RegisterCallback<TElement>(String expectedName, Action<TElement, String> callback)
            where TElement : IElement
        {
            return (element, actualName, value) =>
            {
                if (element is TElement && actualName.Is(expectedName))
                {
                    callback.Invoke((TElement)element, value);
                }
            };
        }

        public void NotifyChange(IElement host, String name, String value)
        {
            for (var i = 0; i < _actions.Length; i++)
            {
                _actions[i].Invoke(host, name, value);
            }
        }
    }
}

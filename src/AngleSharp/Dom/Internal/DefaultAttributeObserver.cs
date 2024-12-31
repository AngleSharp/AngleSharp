namespace AngleSharp.Dom
{
    using AngleSharp.Html.Dom;
    using AngleSharp.Text;
    using System;
    using System.Collections.Generic;
    using AttributeObserver = System.Action<IElement, System.String, System.String>;

    /// <summary>
    /// Represents the default attribute observer.
    /// </summary>
    public class DefaultAttributeObserver : IAttributeObserver
    {
        private readonly List<AttributeObserver> _actions;

        /// <summary>
        /// Creates a new instance.
        /// </summary>
        public DefaultAttributeObserver()
        {
            _actions = [];
            RegisterStandardObservers();
        }

        /// <summary>
        /// Registers the standard attribute observers, e.g., for class, style, ... attributes.
        /// </summary>
        protected virtual void RegisterStandardObservers()
        {
            RegisterObserver<Element>(AttributeNames.Class, (element, value) => element.UpdateClassList(value));
            RegisterObserver<HtmlElement>(AttributeNames.DropZone, (element, value) => element.UpdateDropZone(value));
            RegisterObserver<HtmlBaseElement>(AttributeNames.Href, (element, value) => element.UpdateUrl(value));
            RegisterObserver<HtmlEmbedElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value));
            RegisterObserver<HtmlLinkElement>(AttributeNames.Rel, (element, value) => element.UpdateRel(value));
            RegisterObserver<HtmlLinkElement>(AttributeNames.Sizes, (element, value) => element.UpdateSizes(value));
            RegisterObserver<HtmlLinkElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value));
            RegisterObserver<HtmlLinkElement>(AttributeNames.Disabled, (element, value) => element.UpdateDisabled(value));
            RegisterObserver<HtmlLinkElement>(AttributeNames.Href, (element, value) => element.UpdateSource(value));
            RegisterObserver<HtmlUrlBaseElement>(AttributeNames.Rel, (element, value) => element.UpdateRel(value));
            RegisterObserver<HtmlUrlBaseElement>(AttributeNames.Ping, (element, value) => element.UpdatePing(value));
            RegisterObserver<HtmlTableCellElement>(AttributeNames.Headers, (element, value) => element.UpdateHeaders(value));
            RegisterObserver<HtmlStyleElement>(AttributeNames.Media, (element, value) => element.UpdateMedia(value));
            RegisterObserver<HtmlSelectElement>(AttributeNames.Value, (element, value) => element.UpdateValue(value));
            RegisterObserver<HtmlOutputElement>(AttributeNames.For, (element, value) => element.UpdateFor(value));
            RegisterObserver<HtmlObjectElement>(AttributeNames.Data, (element, value) => element.UpdateSource(value));
            RegisterObserver<HtmlAudioElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value));
            RegisterObserver<HtmlVideoElement>(AttributeNames.Src, (element, value) => element.UpdateSource(value));
            RegisterObserver<HtmlImageElement>(AttributeNames.Src, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlImageElement>(AttributeNames.SrcSet, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlImageElement>(AttributeNames.Sizes, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlImageElement>(AttributeNames.CrossOrigin, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlIFrameElement>(AttributeNames.Sandbox, (element, value) => element.UpdateSandbox(value));
            RegisterObserver<HtmlIFrameElement>(AttributeNames.SrcDoc, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlFrameElementBase>(AttributeNames.Src, (element, _) => element.UpdateSource());
            RegisterObserver<HtmlInputElement>(AttributeNames.Type, (element, value) => element.UpdateType(value));
        }

        /// <summary>
        /// Registers a new attribute observer.
        /// </summary>
        /// <typeparam name="TElement">The associated element type.</typeparam>
        /// <param name="expectedName">The name of the attribute.</param>
        /// <param name="callback">The callback to invoke when the condition is met.</param>
        public void RegisterObserver<TElement>(String expectedName, Action<TElement, String> callback)
            where TElement : IElement
        {
            _actions.Add((element, actualName, value) =>
            {
                if (element is TElement tEl && actualName.Is(expectedName))
                {
                    callback.Invoke(tEl, value);
                }
            });
        }

        void IAttributeObserver.NotifyChange(IElement host, String name, String? value)
        {
            foreach (var action in _actions)
            {
                action.Invoke(host, name, value!);
            }
        }
    }
}

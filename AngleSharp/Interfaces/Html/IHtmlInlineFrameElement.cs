namespace AngleSharp.Dom.Html
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// Represents the iframe HTML element.
    /// </summary>
    [DomName("HTMLIFrameElement")]
    interface IHtmlInlineFrameElement : IHtmlElement
    {
        /// <summary>
        /// Gets or sets the frame source.
        /// </summary>
        [DomName("src")]
        String Source { get; set; }

        /// <summary>
        /// Gets the content of the page that the nested browsing context is to contain.
        /// </summary>
        [DomName("srcdoc")]
        String ContentHtml { get; set; }

        /// <summary>
        /// Gets or sets the name of the frame.
        /// </summary>
        [DomName("name")]
        String Name { get; set; }

        /// <summary>
        /// Gets the tokens of the sandbox attribute.
        /// </summary>
        [DomName("sandbox")]
        ISettableTokenList Sandbox { get; }

        /// <summary>
        /// Gets or sets if the seamless attribute has been set.
        /// </summary>
        [DomName("seamless")]
        Boolean IsSeamless { get; set; }

        /// <summary>
        /// Gets or sets if the frame's content can trigger the fullscreen mode.
        /// </summary>
        [DomName("allowFullscreen")]
        Boolean IsFullscreenAllowed { get; set; }

        /// <summary>
        /// Gets or sets the display width of the frame.
        /// </summary>
        [DomName("width")]
        Int32 DisplayWidth { get; set; }

        /// <summary>
        /// Gets or sets the display height of the frame.
        /// </summary>
        [DomName("height")]
        Int32 DisplayHeight { get; set; }

        /// <summary>
        /// Gets the document this frame contains, if there is any.
        /// </summary>
        [DomName("contentDocument")]
        IDocument ContentDocument { get; }

        /// <summary>
        /// Gets the frame's parent's window context.
        /// </summary>
        [DomName("contentWindow")]
        IWindow ContentWindow { get; }
    }
}
